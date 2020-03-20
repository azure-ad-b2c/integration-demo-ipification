using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace B2C.IPificationDemo.Authentication
{
    public static class AuthExtensions
    {
        public static bool ContainsOidcErrorDescriptionContent(this HttpRequest request, string error) {
            const string errorKey = OpenIdConnectParameterNames.ErrorDescription;

            var errorDescription = request.HasFormContentType
                ? (request.Form.ContainsKey(errorKey) ? request.Form[errorKey] : StringValues.Empty)
                : (request.Query.ContainsKey(errorKey) ? request.Query[errorKey] : StringValues.Empty);

            return errorDescription.Any(e => e.Contains(error));
        }
    }
}