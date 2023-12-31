﻿using System.Text.Json.Serialization;
using static Gateway.Models.Enums;

namespace Gateway.Models.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus Status { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderPayment PaymentType { get; set; }
        public int ClientId { get; set; }
        public ICollection<OrderDetailDto> Items { get; set; } = new List<OrderDetailDto>();
        public DateTime CreatedAt { get; set; }
        public decimal Total { get; set; }
        public ClientDto Client { get; set; }
    }
}
