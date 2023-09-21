using Client.WebClient.Services.Config;
using Gateway.Api.Models;
using Gateway.Models.DTOs;
using System.Text;
using System.Text.Json;
using static Gateway.Models.Enums;

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
        private readonly string _apiGatewayUrl;
        private readonly HttpClient _httpClient;

        public OrderService(
            HttpClient httpClient,
            ApiGatewayUrl apiGatewayUrl,
            IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);
            _httpClient = httpClient;
            _apiGatewayUrl = apiGatewayUrl.Value;
        }

        public async Task<DataCollection<OrderDto>> GetAllAsync(int page, int take)
        {
            var request = await _httpClient.GetAsync($"{_apiGatewayUrl}ordersWithClients?page={page}&take={take}");
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
            var request = await _httpClient.GetAsync($"{_apiGatewayUrl}orders/{id}");
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

            var request = await _httpClient.PostAsync($"{_apiGatewayUrl}orders", content);
            request.EnsureSuccessStatusCode();
        }
    }
}
