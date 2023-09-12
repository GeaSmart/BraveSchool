using Microsoft.Extensions.Options;
using Order.Service.Proxies.Catalog.Commands;
using System.Text;
using System.Text.Json;

namespace Order.Service.Proxies.Catalog
{
    public interface ICatalogProxy
    {
        Task UpdateStockAsync(ProductInStockUpdateStockCommand command);
    }
    public class CatalogProxy : ICatalogProxy
    {
        private readonly HttpClient httpClient;
        private readonly ApiUrls apiUrls;

        public CatalogProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls)
        {
            this.httpClient = httpClient;
            this.apiUrls = apiUrls.Value;
        }
        public async Task UpdateStockAsync(ProductInStockUpdateStockCommand command)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(command),
                Encoding.UTF8,
                "application/json"
            );
            var request = await httpClient.PutAsync($"{apiUrls.CatalogUrl}/stocks",content);
            request.EnsureSuccessStatusCode();
        }
    }
}
