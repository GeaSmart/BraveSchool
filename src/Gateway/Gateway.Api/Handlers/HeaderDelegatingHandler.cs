using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace Gateway.Api.Handlers
{
    public class HeaderDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HeaderDelegatingHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated && _httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);                    
                }
            }

            return await base.SendAsync(request,cancellationToken);

            //IEnumerable<string> headerValues;
            //if (request.Headers.TryGetValues("access_token", out headerValues))
            //{
            //    string accessToken = headerValues.First();

            //    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //    request.Headers.Remove("access_token");
            //}

            //return await base.SendAsync(request, cancellationToken);
        }
    }
}
