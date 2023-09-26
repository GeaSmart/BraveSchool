using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using System.Net;
using Gateway.Models.DTOs;
using Gateway.Api.Models;
using System.Net.Http.Headers;

namespace Gateway.Api.Aggregators
{
    public class CustomAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            var orders = await responses[0].Items.DownstreamResponse().Content.ReadFromJsonAsync<DataCollection<OrderDto>>();
            var clients = await responses[1].Items.DownstreamResponse().Content.ReadFromJsonAsync<DataCollection<ClientDto>>();

            foreach (var order in orders.Items)
            {
                order.Client = clients.Items.FirstOrDefault(x => x.ClientId == order.ClientId);
            }
            var jsonString = JsonConvert.SerializeObject(orders, Formatting.Indented, new JsonConverter[] { new StringEnumConverter() });
            var stringContent = new StringContent(jsonString)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };
            return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
        }
    }
}
