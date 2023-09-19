using static Gateway.Api.Models.Enums;

namespace Gateway.Api.Models
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public OrderPayment PaymentType { get; set; }
        public int ClientId { get; set; }
        public ICollection<OrderDetail> Items { get; set; } = new List<OrderDetail>();
        public DateTime CreatedAt { get; set; }
        public decimal Total { get; set; }

        public ClientDto Client { get; set; }
    }

    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }

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
