using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using System.Net;
using System.Net.Http.Headers;
using Gateway.Models.DTOs;
using Gateway.Api.Models;

namespace Gateway.Api.Aggregators
{
    public class OrderFullAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            var order = await responses[0].Items.DownstreamResponse().Content.ReadFromJsonAsync<OrderDto>();
            var clients = await responses[1].Items.DownstreamResponse().Content.ReadFromJsonAsync<DataCollection<ClientDto>>();
            var products = await responses[2].Items.DownstreamResponse().Content.ReadFromJsonAsync<DataCollection<ProductDto>>();

            order.Client = clients.Items.SingleOrDefault(x => x.ClientId == order.ClientId);
            foreach (var item in order.Items)
            {
                item.Product = products.Items.SingleOrDefault(x => x.ProductId == item.ProductId);
            }
            var jsonString = JsonConvert.SerializeObject(order, Formatting.Indented, new JsonConverter[] { new StringEnumConverter() });
            var stringContent = new StringContent(jsonString)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };
            return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
        }
    }
}
