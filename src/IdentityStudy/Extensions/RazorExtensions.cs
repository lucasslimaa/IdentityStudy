using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;

namespace IdentityStudy.Extensions
{
    public static class RazorExtensions
    {
        public static bool IfClaim(this RazorPage page, string claimName, string claimValue)
        {
            return CustomAuth.ValidateUserClaims(page.Context, claimName, claimValue);
        }

        public static string IfClaimShow(this RazorPage page, string claimName, string claimValue)
        {
            return CustomAuth.ValidateUserClaims(page.Context, claimName, claimValue) ? "" : "disabled";
        }

        public static IHtmlContent IfClaimShow(this IHtmlContent page, HttpContext context,string claimName, string claimValue)
        {
            return CustomAuth.ValidateUserClaims(context, claimName, claimValue) ? page : null;
        }
    }
}
