using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Mobius.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetRating(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Rating");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        //public static void SetRating(this IIdentity identity)
        //{
        //    var claim = ((ClaimsIdentity)identity).FindFirst("Rating");
        //}
    }
}