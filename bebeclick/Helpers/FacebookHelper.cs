using Bebeclick.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Facebook;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Bebeclick.Helpers
{
    public class FacebookHelper
    {
        const string XmlSchemaString = "http://www.w3.org/2001/XMLSchema#string";

        public static string[] Scopes = new string[]
        {
            "email",
            "user_birthday"
        };

        public static Task AddBasicDetailsAsClaims(FacebookAuthenticatedContext context)
        {
            if (context == null)
                throw new InvalidOperationException("context");

            if (!context.Identity.HasClaim("urn:facebook:access_token", context.AccessToken))
                context.Identity.AddClaim(new Claim("urn:facebook:access_token", context.AccessToken, XmlSchemaString, "Facebook"));

            dynamic user = context.User;

            if (!context.Identity.HasClaim("urn:facebook:first_name", user.first_name.Value))
                context.Identity.AddClaim(new Claim("urn:facebook:first_name", user.first_name.Value, XmlSchemaString, "Facebook"));

            if (!context.Identity.HasClaim("urn:facebook:gender", user.gender.Value))
                context.Identity.AddClaim(new Claim("urn:facebook:gender", user.gender.Value, XmlSchemaString, "Facebook"));

            if (!context.Identity.HasClaim("urn:facebook:last_name", user.last_name.Value))
                context.Identity.AddClaim(new Claim("urn:facebook:last_name", user.last_name.Value, XmlSchemaString, "Facebook"));

            if (!context.Identity.HasClaim("urn:facebook:birthday", user.birthday.Value))
                context.Identity.AddClaim(new Claim("urn:facebook:birthday", user.birthday.Value, XmlSchemaString, "Facebook"));

            return Task.FromResult(0);
        }

        public static ExternalLoginConfirmationViewModel CreateModel(ExternalLoginInfo loginInfo)
        {
            if (loginInfo == null)
                throw new ArgumentNullException("loginInfo");

            var model = new ExternalLoginConfirmationViewModel();

            model.Name = loginInfo.ExternalIdentity.Name;
            model.Email = loginInfo.Email;

            var claim = loginInfo.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:facebook:first_name");
            if (claim != null)
                model.FirstName = claim.Value;

            claim = loginInfo.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:facebook:last_name");
            if (claim != null)
                model.LastName = claim.Value;

            claim = loginInfo.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:facebook:gender");
            if (claim != null)
                model.Gender = claim.Value;

            DateTime birthDay = DateTime.MinValue;
            claim = loginInfo.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:facebook:birthday");
            if (claim != null && DateTime.TryParseExact(claim.Value, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDay))
                model.BirthDay = birthDay;

            return model;
        }
    }
}