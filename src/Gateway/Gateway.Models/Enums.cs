namespace Gateway.Models
{
    public class Enums
    {
        public enum OrderStatus
        {
            Canceled,
            Pending,
            Approved
        }

        public enum OrderPayment
        {
            CreditCard,
            Paypal,
            BankTransfer
        }
    }
}
