using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using B2C.IPificationDemo.Authentication;

namespace B2C.IPificationDemo.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public HomeController(
            IConfiguration configuration,
            IAuthenticationSchemeProvider authenticationSchemeProvider
        ) : base()
        {
            Configuration = configuration;
            AuthenticationSchemeProvider = authenticationSchemeProvider;
        }

        public IConfiguration Configuration { get; }

        public IAuthenticationSchemeProvider AuthenticationSchemeProvider { get; }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignIn(string policy)
        {
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(Confirmed))
            }, policy);
        }

        [Authorize]
        public IActionResult Confirmed()
        {
            ViewData["GivenName"] = User.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value ?? "";
            ViewData["Surname"] = User.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value ?? "";
            ViewData["Claims"] = User.Claims ?? Enumerable.Empty<Claim>();
            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            var schemesToSignOut = new[] { CookieAuthenticationDefaults.AuthenticationScheme }.ToList();

            var currentAuthPolicy = User.Claims?.FirstOrDefault(c => c.Type == AuthConfig.ClaimTypes.TrustFrameworkPolicy)
                ?? User.Claims?.FirstOrDefault(c => c.Type == AuthConfig.ClaimTypes.AuthnContextReference);
            if (currentAuthPolicy != null)
            {
                schemesToSignOut.Add(currentAuthPolicy.Value);
            }

            var availableSchemes = (await AuthenticationSchemeProvider.GetAllSchemesAsync())
                            .Select(s => s.Name).ToList();
            schemesToSignOut = schemesToSignOut.Where(s => availableSchemes.Contains(s)).ToList();

            return new SignOutResult(schemesToSignOut);
        }
    }
}
