using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    
    public class RebateServiceTests
    {
        [Fact]
        public void Calculate_ShouldReturn_Success()
        {
            var mockRebateDataStore = new Mock<IRebateDataStore>();
            var mockProdcutDataStore = new Mock<IProductDataStore>();

            mockRebateDataStore.Setup(m => m.GetRebate("r1"))
                .Returns(new Types.Rebate { 
                    Amount = 2, 
                    Identifier = "r1", 
                    Incentive = Types.IncentiveType.FixedRateRebate, 
                    Percentage = 0.10M 
                });

            mockProdcutDataStore.Setup(m => m.GetProduct("p1")).Returns(new Types.Product { 
                Id = 1, 
                Identifier = "p1", 
                Price = 75, 
                SupportedIncentives = Types.SupportedIncentiveType.FixedRateRebate 
            });

            var service = new RebateService(mockRebateDataStore.Object, mockProdcutDataStore.Object);

            var result = service.Calculate(new Types.CalculateRebateRequest { ProductIdentifier = "p1", RebateIdentifier = "r1", Volume = 10 });

            Assert.True(result.Success);
        }

        [Fact]
        public void Calculate_ShouldReturn_Failure()
        {
            var mockRebateDataStore = new Mock<IRebateDataStore>();
            var mockProdcutDataStore = new Mock<IProductDataStore>();

            mockRebateDataStore.Setup(m => m.GetRebate("r1"))
                .Returns(new Types.Rebate
                {
                    Amount = 2,
                    Identifier = "r1",
                    Incentive = Types.IncentiveType.FixedRateRebate,
                });

            mockProdcutDataStore.Setup(m => m.GetProduct("p1")).Returns(new Types.Product { 
                Id = 1, Identifier = "p1", 
                Price = 75, 
                SupportedIncentives = Types.SupportedIncentiveType.FixedRateRebate 
            });

            var service = new RebateService(mockRebateDataStore.Object, mockProdcutDataStore.Object);

            var result = service.Calculate(new Types.CalculateRebateRequest { ProductIdentifier = "p1", RebateIdentifier = "r1", Volume = 10 });

            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_ShouldReturn_FixedRate()
        {
            var mockRebateDataStore = new Mock<IRebateDataStore>();
            var mockProdcutDataStore = new Mock<IProductDataStore>();

            mockRebateDataStore.Setup(m => m.GetRebate("r1"))
                .Returns(new Types.Rebate
                {
                    Amount = 2,
                    Identifier = "r1",
                    Incentive = Types.IncentiveType.FixedRateRebate,
                    Percentage = 0.10M
                });

            mockProdcutDataStore.Setup(m => m.GetProduct("p1")).Returns(new Types.Product { 
                Id = 1, 
                Identifier = "p1", 
                Price = 75, 
                SupportedIncentives = Types.SupportedIncentiveType.FixedRateRebate,
            });

            var service = new RebateService(mockRebateDataStore.Object, mockProdcutDataStore.Object);

            var result = service.Calculate(new Types.CalculateRebateRequest { ProductIdentifier = "p1", RebateIdentifier = "r1", Volume = 10 });

            Assert.Equal(75, result.RebateAmount);
        }

        [Fact]
        public void Calculate_ShouldReturn_FixedCashAmount()
        {
            var mockRebateDataStore = new Mock<IRebateDataStore>();
            var mockProdcutDataStore = new Mock<IProductDataStore>();

            mockRebateDataStore.Setup(m => m.GetRebate("r1"))
                .Returns(new Types.Rebate
                {
                    Amount = 2,
                    Identifier = "r1",
                    Incentive = Types.IncentiveType.FixedCashAmount,
                });

            mockProdcutDataStore.Setup(m => m.GetProduct("p1")).Returns(new Types.Product { 
                Id = 1, 
                Identifier = "p1", 
                Price = 75, 
                SupportedIncentives = Types.SupportedIncentiveType.FixedCashAmount 
            });

            var service = new RebateService(mockRebateDataStore.Object, mockProdcutDataStore.Object);

            var result = service.Calculate(new Types.CalculateRebateRequest { ProductIdentifier = "p1", RebateIdentifier = "r1", Volume = 10 });

            Assert.Equal(2, result.RebateAmount);
        }

        [Fact]
        public void Calculate_ShouldReturn_AmountPerUom()
        {
            var mockRebateDataStore = new Mock<IRebateDataStore>();
            var mockProdcutDataStore = new Mock<IProductDataStore>();

            mockRebateDataStore.Setup(m => m.GetRebate("r1"))
                .Returns(new Types.Rebate
                {
                    Amount = 2,
                    Identifier = "r1",
                    Incentive = Types.IncentiveType.AmountPerUom,
                });

            mockProdcutDataStore.Setup(m => m.GetProduct("p1")).Returns(new Types.Product { 
                Id = 1, 
                Identifier = "p1", 
                Price = 75, 
                SupportedIncentives = Types.SupportedIncentiveType.AmountPerUom 
            });

            var service = new RebateService(mockRebateDataStore.Object, mockProdcutDataStore.Object);

            var result = service.Calculate(new Types.CalculateRebateRequest { ProductIdentifier = "p1", RebateIdentifier = "r1", Volume = 10 });

            Assert.Equal(20, result.RebateAmount);
        }
    }
}
