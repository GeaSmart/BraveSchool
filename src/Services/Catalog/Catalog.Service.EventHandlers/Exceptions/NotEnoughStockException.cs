namespace Catalog.Service.EventHandlers.Exceptions
{
    public class NotEnoughStockException : Exception
    {
        public NotEnoughStockException(string? message) : base(message)
        {

        }
    }
}