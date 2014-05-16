using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System;
using Bebeclick.Models;

namespace Bebeclick
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerS$DCR612q
#if DEBUG
            app.UseFacebookAuthentication(
               appId: "770000323020014",
               appSecret: "1a3c82df9d64007bc20ec26bc524d9ec");
#else
            app.UseFacebookAuthentication(
               appId: "806277869438073",
               appSecret: "dc813df21288180a87d066390acfb244");
#endif
#if DEBUG
            app.UseGoogleAuthentication(
                clientId: "676333113969-qmannacftg8aoe2rhl0e01o2jpa1ku88.apps.googleusercontent.com",
                clientSecret: "w4KoCgadkbzR7v9sepgGdVjt");
#else
            app.UseGoogleAuthentication(
                clientId: "1080722520326-jadofdikkua6lruhiv3lnarmhsk5ilo8.apps.googleusercontent.com",
                clientSecret: "W1Tpe0r59bQFeygaFVWq83Vo");
#endif
        }
    }
}