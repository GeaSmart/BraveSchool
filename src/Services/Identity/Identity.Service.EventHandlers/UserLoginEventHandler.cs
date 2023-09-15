using Identity.Domain;
using Identity.Persistence.Database;
using Identity.Service.EventHandlers.Commands;
using Identity.Service.EventHandlers.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Service.EventHandlers
{
    public class UserLoginEventHandler : IRequestHandler<UserLoginCommand, IdentityAccess>
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public UserLoginEventHandler(SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            this.signInManager = signInManager;
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<IdentityAccess> Handle(UserLoginCommand notification, CancellationToken cancellationToken)
        {
            var result = new IdentityAccess();
            var user = await context.Users.SingleAsync(x => x.Email == notification.Email);
            var response = await signInManager.CheckPasswordSignInAsync(user, notification.Password, false);

            if (response.Succeeded)            
                result = await GenerateToken(user, result);
            
            return result;
        }

        private async Task<IdentityAccess> GenerateToken(ApplicationUser user, IdentityAccess identity)
        {
            var secretKey = configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };

            var roles = await context.Roles
                                      .Where(x => x.UserRoles.Any(y => y.UserId == user.Id))
                                      .ToListAsync();

            foreach (var role in roles)
            {
                claims.Add(
                    new Claim(ClaimTypes.Role, role.Name)
                );
            }

            var expirationDate = DateTime.UtcNow.AddDays(1);//this time can be configured
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            identity.AccessToken = tokenHandler.WriteToken(createdToken);
            identity.Expiration = expirationDate;

            return identity;
        }
    }    
}
