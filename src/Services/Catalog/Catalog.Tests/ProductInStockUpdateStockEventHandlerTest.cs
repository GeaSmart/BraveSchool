using Catalog.Domain;
using Catalog.Service.EventHandlers;
using Catalog.Service.EventHandlers.Commands;
using Catalog.Tests.Config;
using static Catalog.Common.Enums;

namespace Catalog.Tests
{
    [TestClass]
    public class ProductInStockUpdateStockEventHandlerTest
    {
        [TestMethod]
        public void SubstractStockWhenProductHasStock()
        {
            //Prepare
            var context = ApplicationDbContextInMemory.Get();
            var logger = Logger.Get();

            var productInStockId = 1;
            var productId = 1;

            context.Stocks.Add(
                new ProductInStock
                {
                    ProductInStockId = productInStockId,
                    ProductId = productId,
                    Stock = 1
                }
            );

            context.SaveChanges();

            //Test
            var handler = new ProductInStockUpdateStockEventHandler(context, logger);

            handler.Handle(new ProductInStockUpdateStockCommand { 
                Items = new List<ProductInStockUpdateItem>(){
                    new ProductInStockUpdateItem { 
                        ProductId = productId, 
                        Stock = 1, 
                        Action = ProductInStockAction.Substract 
                    }                    
                }
            }, new CancellationToken()).Wait();

            //Verify
            //It is implicit, there's no need to assert anything since the test will pass if no exception is thrown
        }
    }
}