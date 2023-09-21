using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Client.WebClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly string authenticationUrl;

        public AccountController(IConfiguration configuration)
        {
            this.authenticationUrl = configuration.GetValue<string>("AuthenticationUrl");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return Redirect(authenticationUrl + $"?ReturnBaseUrl={this.Request.Scheme}://{this.Request.Host}/");
        }

        [HttpGet]
        public async Task<IActionResult> Connect(string access_token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(access_token);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, securityToken.Claims.FirstOrDefault(x => x.Type == "nameid").Value),
                new Claim(ClaimTypes.Name, securityToken.Claims.FirstOrDefault(x => x.Type == "unique_name").Value),
                new Claim(ClaimTypes.Email, securityToken.Claims.FirstOrDefault(x => x.Type == "email").Value),
                new Claim("access_token", securityToken.RawData)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IssuedUtc = DateTime.UtcNow.AddHours(12)//must be less than the JWT API token lifetime
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return Redirect("~/");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/");
        }
    }
}
