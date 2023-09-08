using Catalog.Domain;
using Catalog.Service.EventHandlers;
using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.EventHandlers.Exceptions;
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
                        Stock = 1, //intento quitarle 1 cuando tiene 1 de stock
                        Action = ProductInStockAction.Substract 
                    }                    
                }
            }, new CancellationToken()).Wait();

            //Verify
            //It is implicit, there's no need to assert anything since the test will pass if no exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(NotEnoughStockException))]
        public void SubstractStockWhenProductHasNotEnoughStock()
        {
            //Prepare
            var context = ApplicationDbContextInMemory.Get();
            var logger = Logger.Get();

            var productInStockId = 2;
            var productId = 2;

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
            try
            {
                handler.Handle(new ProductInStockUpdateStockCommand
                {
                    Items = new List<ProductInStockUpdateItem>(){
                    new ProductInStockUpdateItem {
                        ProductId = productId,
                        Stock = 2,//intento quitarle 2 cuando tiene 1 de stock
                        Action = ProductInStockAction.Substract
                    }
                }
                }, new CancellationToken()).Wait();
            }
            catch(AggregateException aggregateException)//hacemos esto porque un m�todo as�ncrono lanza siempre un aggregate exception
            {
                if(aggregateException.GetBaseException() is NotEnoughStockException)
                {
                    throw new NotEnoughStockException(aggregateException?.InnerException?.Message);
                }
            }

            //Verify
            //it is on the annotation ExpectedException
        }

        [TestMethod]
        public void AddStockWhenProductExists()
        {
            //Prepare
            var context = ApplicationDbContextInMemory.Get();
            var logger = Logger.Get();

            var productInStockId = 3;
            var productId = 3;

            context.Stocks.Add(
                new ProductInStock
                {
                    ProductInStockId = productInStockId,
                    ProductId = productId,
                    Stock = 10
                }
            );
            context.SaveChanges();

            //Test
            var handler = new ProductInStockUpdateStockEventHandler(context, logger);

            handler.Handle(new ProductInStockUpdateStockCommand
            {
                Items = new List<ProductInStockUpdateItem>(){
                    new ProductInStockUpdateItem {
                        ProductId = productId,
                        Stock = 5, //le sumo 5 al stock actual de 10
                        Action = ProductInStockAction.Add
                    }
                }
            }, new CancellationToken()).Wait();

            //Verify
            var stockInDb = context.Stocks.SingleOrDefault(x => x.ProductId == productId).Stock;
            Assert.AreEqual(stockInDb, 15);
        }

        [TestMethod]
        public void AddStockWhenProductNotExists()
        {
            //Prepare
            var context = ApplicationDbContextInMemory.Get();
            var logger = Logger.Get();

            var productId = 4;

            //Test
            var handler = new ProductInStockUpdateStockEventHandler(context, logger);

            handler.Handle(new ProductInStockUpdateStockCommand
            {
                Items = new List<ProductInStockUpdateItem>(){
                    new ProductInStockUpdateItem {
                        ProductId = productId,
                        Stock = 5, //a�ado 5 de stock a producto que no existe a�n
                        Action = ProductInStockAction.Add
                    }
                }
            }, new CancellationToken()).Wait();

            //Verify
            var stockInDb = context.Stocks.SingleOrDefault(x => x.ProductId == productId).Stock;
            Assert.AreEqual(stockInDb, 5);
        }
    }
}