

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using StopajHesapAPI.Models;
using StopajHesapAPI.Services.Abstraction;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IUserService _userService;
    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IUserService userService) : base(options, logger, encoder, clock)
    {
        _userService = userService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            AuthenticateResult.Fail("Başlık Bulunamadi");
        }

        UserModel user = null;

        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
            var username = credentials[0];
            var password = credentials[1];

            user = await _userService.Login(username, password);
        }
        catch
        {
            return AuthenticateResult.Fail("Baslik Bulunamadi");
        }

        if (user == null)

            throw new UnauthorizedAccessException("Bir hata oluştu");


        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.userName)
        };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);

    }
}