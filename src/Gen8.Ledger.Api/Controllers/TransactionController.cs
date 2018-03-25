using System;
using System.Linq;
using System.Collections.Generic;
using Gen8.Ledger.Core.ReadModel;
using Gen8.Ledger.Core.ReadModel.Dtos;
using Gen8.Ledger.Core.WriteModel.Commands;
using CQRSlite.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Gen8.Ledger.Core.Domain;
using Gen8.Ledger.Core.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Gen8.Ledger.Api.Controllers
{
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ICommandSender _commandSender;
        private readonly IReadModelFacade _readmodel;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionController (ILogger<TransactionController> logger,
                                        IHttpContextAccessor httpContextAccessor,
                                        ICommandSender commandSender,
                                        IReadModelFacade readmodel)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _readmodel = readmodel;
            _commandSender = commandSender;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<BookDto> Get()
        {
            _logger.LogInformation(LoggingEvents.LIST_ITEMS, "Listing all items");
            return _readmodel.GetTransactionItems();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            _logger.LogInformation(LoggingEvents.GET_ITEM, "Getting item {0}", id.ToString());

            var transaction = _readmodel.GetTransactionItems().FirstOrDefault(p => p.AggregateId == id);
            if (transaction == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "GetById({ID}) NOT FOUND", id.ToString());
                return NotFound();
            }
            return new ObjectResult(transaction);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]JournalEntry journalEntry)
        {
            if (journalEntry == null || journalEntry.EventDate == null ||
                 journalEntry.Splits == null || journalEntry.Splits.Count < 2)
            {
                return BadRequest();
            }

            // TODO: Get user from claims
            /*string userId = _httpContextAccessor.HttpContext.User.Claims
                            .Where(c => c.Type == Constants.ClaimType)
                            .Select(c => c.Value).SingleOrDefault(); */
            string userId = "abc";

            BookDto book = _readmodel.Find("UserId", userId);
            if (book == null)
            {
                Guid journalEntryId = Guid.NewGuid();
                _commandSender.Send(new CreateJournal(journalEntryId, userId, journalEntry));
                _logger.LogInformation(LoggingEvents.INSERT_ITEM, "Item {0} Added. New Book", journalEntryId);
            }
            else
            {
                _commandSender.Send(new AddJournalEntry(book.AggregateId, userId, journalEntry));
                _logger.LogInformation(LoggingEvents.UPDATE_ITEM, "Item {0} Added", book.AggregateId);
            }

            return new OkResult();
        }
    }
}
