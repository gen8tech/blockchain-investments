using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Gen8.Ledger.Core.Infrastructure;
using Gen8.Ledger.Core.Domain;
using Gen8.Ledger.Core.Repositories;

namespace Gen8.Ledger.Api.Controllers
{
    [Route("api/[controller]")]
    public class PriceController : Controller
    {
        private readonly ILogger<PriceController> _logger;
        private readonly INoSqlRepository<Price> _nosqlRepo;
        private readonly IRelationalRepository<Price> _relationalRepo;

        public PriceController (ILogger<PriceController> logger, INoSqlRepository<Price> nosqlRepo, IRelationalRepository<Price> relationalRepo)
        {
            _logger = logger;
            _nosqlRepo = nosqlRepo;
            _relationalRepo = relationalRepo;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Price> Get()
        {
            _logger.LogInformation(LoggingEvents.LIST_ITEMS, "Listing all items");
            return _nosqlRepo.FindAll();
        }

        // GET api/values/5
        [HttpGet("{id:length(24)}")]
        public IActionResult Get(string id)
        {
            _logger.LogInformation(LoggingEvents.GET_ITEM, "Getting item {0}", id);

            var price = _nosqlRepo.FindByObjectId(id);
            if (price == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "GetById({ID}) NOT FOUND", id);
                return NotFound();
            }
            return new ObjectResult(price);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Price price)
        {
            if (price == null)
            {
                return BadRequest();
            }
            var newPrice = _nosqlRepo.Create(price);
            _logger.LogInformation(LoggingEvents.INSERT_ITEM, "Item {0} Created", newPrice.UniqueId);
            return new OkObjectResult(price);
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]Price price)
        {
            if (price == null || string.IsNullOrEmpty(price.UniqueId))
            {
                return BadRequest();
            }

            var currentPrice = _nosqlRepo.FindByObjectId(price.UniqueId);
            if (currentPrice == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "Update({0}) NOT FOUND", price.UniqueId);
                return NotFound();
            }

            _nosqlRepo.Update(price.UniqueId, price);
            _logger.LogInformation(LoggingEvents.UPDATE_ITEM, "Item {0} Updated", price.UniqueId);
            return new OkResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var price = _nosqlRepo.FindByObjectId(id);
            if (price == null)
            {
                return NotFound();
            }

            _nosqlRepo.Remove(id);
            _logger.LogInformation(LoggingEvents.DELETE_ITEM, "Item {0} Deleted", id);
            return new OkResult();
        }
    }
}
