using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft​.Extensions​.Options;
using Gen8.Ledger.Core.Infrastructure;
using Gen8.Ledger.Core.Domain;
using Gen8.Ledger.Core.Repositories;
using Gen8.Ledger.Api.Requests.Accounts;
using AutoMapper;
using Gen8.Ledger.Core.WriteModel.Commands;
using CQRSlite.Commands;
using Gen8.Ledger.Core.ReadModel.Dtos;
using System;

namespace Gen8.Ledger.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICommandSender _commandSender;
        private readonly INoSqlRepository<AccountDto> _nosqlRepo;
        private readonly IRelationalRepository<AccountDto> _relationalRepo;
        private readonly ILogger<AccountController> _logger;

        public AccountController (ILogger<AccountController> logger, INoSqlRepository<AccountDto> nosqlRepo, IRelationalRepository<AccountDto> relationalRepo, ICommandSender commandSender, IMapper mapper)
        {
            _logger = logger;
            _nosqlRepo = nosqlRepo;
            _relationalRepo = relationalRepo;
            _commandSender = commandSender;
            _mapper = mapper;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<AccountDto> Get()
        {
            _logger.LogInformation(LoggingEvents.LIST_ITEMS, "Listing all items");
            return _nosqlRepo.FindAll();
        }

        // GET api/values/5
        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            _logger.LogInformation(LoggingEvents.GET_ITEM, "Getting item {0}", id);

            var account = _nosqlRepo.FindByAggregateId(id);
            if (account == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "GetById({ID}) NOT FOUND", id);
                return NotFound();
            }
            return new ObjectResult(account);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]AccountRequest request)
        {
            bool exists = _nosqlRepo.Exists(request.Id);
            if (!exists)
            {
                return BadRequest();
            }
           var command = _mapper.Map<AssignParentAccount>(request);
            _commandSender.Send(command);

            _logger.LogInformation(LoggingEvents.UPDATE_ITEM, "Item {0} Assign parent requested", request.Id);
            return new OkResult();
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]AccountRequest request)
        {
            bool exists = _nosqlRepo.Exists(request.Id);
            if (exists)
            {
                return BadRequest();
            }
            var command = _mapper.Map<CreateAccount>(request);
            _commandSender.Send(command);

            _logger.LogInformation(LoggingEvents.INSERT_ITEM, "Item {0} Creation requested", request.Id);
            return new OkResult();
        }

        // DELETE api/values/5
        [HttpDelete]
        public IActionResult Delete([FromBody]AccountRequest request)
        {
            bool exists = _nosqlRepo.Exists(request.Id);
            if (!exists)
            {
                return BadRequest();
            }
            var command = _mapper.Map<DeleteAccount>(request);
            _commandSender.Send(command);

            _logger.LogInformation(LoggingEvents.DELETE_ITEM, "Item {0} Delete requested", request.Id);
            return new OkResult();
        }
    }
}
