using Client.WebClient.Services.Config;
using Gateway.Api.Models;
using Gateway.Models.DTOs;
using System.Text;
using System.Text.Json;

namespace Client.WebClient.Services
{
    public interface IOrderService
    {
        Task<DataCollection<OrderDto>> GetAllAsync(int page, int take);
        Task<OrderDto> GetAsync(int id);
        Task CreateAsync(OrderDto orderDto);
    }

    public class OrderService : IOrderService
    {
        private readonly string apiGatewayUrl;
        private readonly HttpClient httpClient;

        public OrderService(
            HttpClient httpClient,
            ApiGatewayUrl apiGatewayUrl,
            IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);
            this.httpClient = httpClient;
            this.apiGatewayUrl = apiGatewayUrl.Value;
        }

        public async Task<DataCollection<OrderDto>> GetAllAsync(int page, int take)
        {
            var request = await httpClient.GetAsync($"{apiGatewayUrl}ordersWithClients?page={page}&take={take}");
            request.EnsureSuccessStatusCode();
            
            return JsonSerializer.Deserialize<DataCollection<OrderDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,IncludeFields = true
                }
            );
        }

        public async Task<OrderDto> GetAsync(int id)
        {
            var request = await httpClient.GetAsync($"{apiGatewayUrl}ordersFull/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<OrderDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task CreateAsync(OrderDto orderDto)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(orderDto),
                Encoding.UTF8,
                "application/json"
            );

            var request = await httpClient.PostAsync($"{apiGatewayUrl}orders", content);
            request.EnsureSuccessStatusCode();
        }
    }
}
