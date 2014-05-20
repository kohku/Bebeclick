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
        public static Task AddBasicDetailsAsClaims(FacebookAuthenticatedContext context)
        {
            if (context == null)
                throw new InvalidOperationException("context");

            context.Identity.AddClaim(new Claim("AuthenticationType", context.Identity.AuthenticationType));
            context.Identity.AddClaim(new Claim("FacebookAccessToken", context.AccessToken));
            context.Identity.AddClaim(new Claim("FacebookID", context.Id));

            //if (!string.IsNullOrEmpty(context.Name))
            //    context.Identity.AddClaim(new Claim("FacebookName", context.Name));
            //if (!string.IsNullOrEmpty(context.Email))
            //    context.Identity.AddClaim(new Claim("FacebookEmail", context.Email));

            //dynamic user = context.User;

            //if (user.gender != null && !string.IsNullOrEmpty(user.gender.ToString()))
            //    context.Identity.AddClaim(new Claim("FacebookGender", user.gender.ToString()));
            //if (user.first_name != null && !string.IsNullOrEmpty(user.first_name.ToString()))
            //    context.Identity.AddClaim(new Claim("FacebookFirstName", user.first_name.ToString()));
            //if (user.last_name != null && !string.IsNullOrEmpty(user.last_name.ToString()))
            //    context.Identity.AddClaim(new Claim("FacebookLastName", user.last_name.ToString()));

            return Task.FromResult(0);
        }
    }
}