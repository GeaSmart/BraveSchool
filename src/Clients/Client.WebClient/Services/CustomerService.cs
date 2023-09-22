using Client.WebClient.Services.Config;
using Gateway.Api.Models;
using Gateway.Models.DTOs;
using System.Text.Json;

namespace Client.WebClient.Services
{
    public interface ICustomerService
    {
        Task<DataCollection<ClientDto>> GetAllAsync(int page, int take, IEnumerable<int> clients = null);
        Task<ClientDto> GetAsync(int id);
    }

    public class CustomerService : ICustomerService
    {
        private readonly HttpClient httpClient;
        private readonly string apiGatewayUrl;

        public CustomerService(
            HttpClient httpClient,
            ApiGatewayUrl apiGatewayUrl,
            IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);
            this.httpClient = httpClient;
            this.apiGatewayUrl = apiGatewayUrl.Value;
        }

        public async Task<DataCollection<ClientDto>> GetAllAsync(int page, int take, IEnumerable<int> clients = null)
        {
            var ids = string.Join(',', clients ?? new List<int>());

            var request = await httpClient.GetAsync($"{apiGatewayUrl}clients?page={page}&take={take}&ids={ids}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<ClientDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<ClientDto> GetAsync(int id)
        {
            var request = await httpClient.GetAsync($"{apiGatewayUrl}clients/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ClientDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
