using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using System.Net;
using System.Net.Http.Headers;
using Gateway.Models.DTOs;
using Gateway.Api.Models;
using Newtonsoft.Json.Linq;
using System.IO.Compression;
using System.Text;

namespace Gateway.Api.Aggregators
{
    public class CustomAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {

            List<Header> header = new List<Header>();
            try
            {
                var headers = responses.SelectMany(x => x.Items.DownstreamResponse().Headers).ToList();

                var ordersByteArray = await responses[0].Items.DownstreamResponse().Content.ReadAsByteArrayAsync();
                var ordersData = Decompress(ordersByteArray);
                var orders = ConvertToJson(ordersData).ToObject<DataCollection<OrderDto>>();

                var clientsByteArray = await responses[1].Items.DownstreamResponse().Content.ReadAsByteArrayAsync();
                var clientsData = Decompress(clientsByteArray);
                var clients = ConvertToJson(clientsData).ToObject<DataCollection<ClientDto>>();

                foreach (var order in orders.Items)
                {
                    order.Client = clients.Items.FirstOrDefault(x => x.ClientId == order.ClientId);
                }

                var stringContent = new StringContent(JsonConvert.SerializeObject(orders), Encoding.UTF8, "application/json");

                return new DownstreamResponse(stringContent, HttpStatusCode.OK, headers, "OK");
            }
            catch (Exception ex)
            {
                return new DownstreamResponse(null, System.Net.HttpStatusCode.InternalServerError, header, null);
            }

            //var orders = await responses[0].Items.DownstreamResponse().Content.ReadFromJsonAsync<DataCollection<OrderDto>>();
            //var clients = await responses[1].Items.DownstreamResponse().Content.ReadFromJsonAsync<DataCollection<ClientDto>>();

            //foreach (var order in orders.Items)
            //{
            //    order.Client = clients.Items.FirstOrDefault(x => x.ClientId == order.ClientId);
            //}

            //var jsonString = JsonConvert.SerializeObject(orders, Formatting.Indented, new JsonConverter[] { new StringEnumConverter() });

            //var stringContent = new StringContent(jsonString)
            //{
            //    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            //};

            //return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");


        }

        private static byte[] Decompress(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                zipStream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }

        private static JObject ConvertToJson(byte[] data)
        {
            JObject jObj;
            using (var ms = new MemoryStream(data))
            using (var streamReader = new StreamReader(ms))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                jObj = (JObject)JToken.ReadFrom(jsonReader);
            }
            return jObj;
        }
    }
}
