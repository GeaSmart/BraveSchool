using Gateway.Api.Models;
using Gateway.Models.DTOs;

namespace Client.WebClient.Models
{
    public class CreateOrderModel
    {
        public DataCollection<ProductDto> Products { get; set; }
        public DataCollection<ClientDto> Clients { get; set; }
    }
}
