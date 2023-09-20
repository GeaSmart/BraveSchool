using Client.Authentication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Client.Authentication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly string _identityUrl;

        [BindProperty(SupportsGet = true)]
        public string ReturnBaseUrl { get; set; }
        [BindProperty]
        public LoginViewModel model { get; set; }

        public bool HasInvalidAccess { get; set; }

        public IndexModel(
            ILogger<IndexModel> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _identityUrl = configuration.GetValue<string>("IdentityUrl");
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(model),
                    Encoding.UTF8,
                    "application/json"
                );

                var request = await client.PostAsync(_identityUrl + "identity/login", content);

                if (!request.IsSuccessStatusCode)
                {
                    HasInvalidAccess = true;
                    return Page();
                }

                var result = JsonSerializer.Deserialize<IdentityAccess>(
                    await request.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

                //return Redirect(ReturnBaseUrl + $"account/connect?access_token={result.AccessToken}");
                return Redirect(ReturnBaseUrl + $"?access_token={result.AccessToken}");//temporary till the other client is ready
            }
        }
    }
}