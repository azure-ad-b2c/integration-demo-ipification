using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace B2C.IPificationDemo.Authentication
{
    public static class AuthConfig
    {
        private const string AuthenticationPoliciesSectionName = "AuthenticationPolicies";

        public static IEnumerable<IConfigurationSection> GetAuthenticationConfigurationSections(this IConfiguration configuration) =>
            configuration.GetSection(AuthenticationPoliciesSectionName).GetChildren();

        public static IEnumerable<string> GetAuthenticationPolicies(this IConfiguration configuration) =>
            configuration.GetAuthenticationConfigurationSections().Select(c => c.Key);

        public static class ErrorCodes
        {
            public const string CancelledByUser = "AADB2C90091";
            public const string ForgotPassword = "AADB2C90118";
        }

        public static class ClaimTypes {
            public const string AuthnContextReference = "http://schemas.microsoft.com/claims/authnclassreference";
            
            public const string TrustFrameworkPolicy = "tfp";
        }
    }
}