using Microsoft.Owin.Security.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Bebeclick.Controllers
{
    public class FacebookController
    {
        const string XmlSchemaString = "http://www.w3.org/2001/XMLSchema#string";
        public static Task AddBasicDetailsAsClaims(FacebookAuthenticatedContext context)
        {
            if (context == null)
                throw new InvalidOperationException("context");

            if (!context.Identity.HasClaim("urn:facebook:access_token", context.AccessToken))
                context.Identity.AddClaim(new Claim("urn:facebook:access_token", context.AccessToken, XmlSchemaString, "Facebook"));

            return Task.FromResult(0);
        }
    }
}