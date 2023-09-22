using Client.WebClient.Services.Config;
using Gateway.Api.Models;
using Gateway.Models.DTOs;
using System.Text.Json;

namespace Client.WebClient.Services
{
    public interface ICatalogService
    {
        Task<DataCollection<ProductDto>> GetAllAsync(int page, int take, IEnumerable<int> clients = null);
        Task<ProductDto> GetAsync(int id);
    }

    public class CatalogService : ICatalogService
    {
        private readonly HttpClient httpClient;
        private readonly string apiGatewayUrl;

        public CatalogService(
            HttpClient httpClient,
            ApiGatewayUrl apiGatewayUrl,
            IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);
            this.httpClient = httpClient;
            this.apiGatewayUrl = apiGatewayUrl.Value;
        }

        public async Task<DataCollection<ProductDto>> GetAllAsync(int page, int take, IEnumerable<int> clients = null)
        {
            var ids = string.Join(',', clients ?? new List<int>());

            var request = await httpClient.GetAsync($"{apiGatewayUrl}products?page={page}&take={take}&ids={ids}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<ProductDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<ProductDto> GetAsync(int id)
        {
            var request = await httpClient.GetAsync($"{apiGatewayUrl}products/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ProductDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
