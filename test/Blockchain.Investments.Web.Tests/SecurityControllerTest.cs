using System.Linq;
using System.Collections.Generic;
using Gen8.Ledger.Api.Controllers;
using Gen8.Ledger.Core.Domain;
using Gen8.Ledger.Core.Repositories;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Gen8.Ledger.Api
{
    public class SecurityControllerTest
    {
        private readonly List<Security> _securities;
        private readonly ILogger<SecurityController> _mockLogger;
        private readonly IRepository<Security> _mockRepository;
        public SecurityControllerTest()
        {
            _mockLogger = A.Fake<ILogger<SecurityController>>();
            _mockRepository = A.Fake<IRepository<Security>>();
            _securities = new List<Security>
            {
                {
                    new Security
                        {
                            UniqueId = "587bc285d3b9f0278d5d2122",
                            Title = "Real",
                            Code = "986",
                            Fraction = 100,
                            Type = MarketType.ForexCurrency,
                            Pricing = PricingMechanism.Market,
                            ImageUrl = "http://www.xe.com/themes/xe/images/flags/big/brl.png",
                            DetailsUrl = "http://www.xe.com/currency/brl-brazilian-real",
                            Description = "The Brazilian Real is the currency of Brazil",
                            Namespace = "Forex",
                            QuoteSource = "www.xe.com"
                        }
                },
                {
                    new Security
                        {
                            UniqueId = "587be6c542000a36828dd9c5",
                            Title = "Bitcoin",
                            Code = "BTC",
                            Fraction = 100000000,
                            Type = MarketType.CryptoCurrency,
                            Pricing = PricingMechanism.Market,
                            ImageUrl = "http://www.xe.com/themes/xe/images/flags/big/xbt.png",
                            DetailsUrl = "http://www.coindesk.com/price/",
                            Description = "Crypto",
                            Namespace = "Forex",
                            QuoteSource = "www.coindesk.com"
                        }
                }
            };
        }

        [Fact]
        public void Security_GetById_ReturnsItem()
        {
            // Arrange
            Security item = _securities.FirstOrDefault();
            A.CallTo(() => _mockRepository.FindByObjectId(item.UniqueId))
                .Returns(item);
            var controller = new SecurityController(_mockLogger, _mockRepository);

            // Act
            var result = controller.Get(item.UniqueId);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var returnValue = Assert.IsType<Security>(objectResult.Value);
            Assert.NotNull(returnValue);
            Assert.Equal(returnValue.Code, item.Code);
        }

        [Fact]
        public void Security_GetById_NotFound()
        {
            // Arrange
            string id = "xxxxxxxxxxxxxxxxxxxxxxxx";
            A.CallTo(() => _mockRepository.FindByObjectId(id))
                .Returns(null);
            var controller = new SecurityController(_mockLogger, _mockRepository);

            // Act
            var result = controller.Get(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Security_GetAll_ReturnsCollection()
        {
            // Arrange
            A.CallTo(() => _mockRepository.FindAll())
                .Returns(_securities);

            var controller = new SecurityController(_mockLogger, _mockRepository);

            // Act
            IEnumerable<Security> actionResult = controller.Get();

            // Assert
            Assert.Equal(_securities, actionResult);
        }

        [Fact]
        public void Security_Create_ReturnsLocationHeader()
        {
            // Arrange
            Security item = _securities.FirstOrDefault();
            A.CallTo(() => _mockRepository.Create(item))
                .Returns(item);
            var controller = new SecurityController(_mockLogger, _mockRepository);

            // Act
            var result = controller.Post(item);

            // Assert
            var objectResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal("default", objectResult.RouteName);
            var returnValue = Assert.IsType<Security>(objectResult.Value);
            Assert.NotNull(returnValue);
            Assert.Equal(objectResult.RouteValues["id"], item.UniqueId);
        }

        [Fact]
        public void Security_Update_ReturnsContentResult()
        {
            // Arrange
            Security item = _securities.FirstOrDefault();
            A.CallTo(() => _mockRepository.FindByObjectId(item.UniqueId))
                .Returns(item);
            var controller = new SecurityController(_mockLogger, _mockRepository);

            // Act
            var result = controller.Put(item);

            // Assert
            var objectResult = Assert.IsType<AcceptedResult>(result);
            Assert.Equal(202, objectResult.StatusCode);
            var returnValue = Assert.IsType<Security>(objectResult.Value);
            Assert.NotNull(returnValue);
            Assert.Equal(returnValue.Code, item.Code);
        }

        [Fact]
        public void Security_Update_BadRequest()
        {
            // Arrange
            var controller = new SecurityController(_mockLogger, _mockRepository);

            // Act
            var result = controller.Put(new Security());

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Security_Delete_NotFound()
        {
            // Arrange
            string id = "xxxxxxxxxxxxxxxxxxxxxxxx";
            A.CallTo(() => _mockRepository.FindByObjectId(id))
                .Returns(null);
            var controller = new SecurityController(_mockLogger, _mockRepository);

            // Act
            var result = controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Security_Delete_ReturnsOk()
        {
            // Arrange
            Security item = _securities.FirstOrDefault();
            A.CallTo(() => _mockRepository.FindByObjectId(item.UniqueId))
                .Returns(item);
            var controller = new SecurityController(_mockLogger, _mockRepository);

            // Act
            var result = controller.Delete(item.UniqueId);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
