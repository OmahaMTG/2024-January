using Fga.Net.AspNetCore;
using Fga.Net.AspNetCore.Authorization;
using Fga.Net.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;

namespace _1_Simple_AuthN
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.AddFilter("Fga.Net.AspNetCore.Authorization.FineGrainedAuthorizationHandler", LogLevel.Debug);

            builder.Services.AddControllers();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(jwtOptions =>
                    {
                        jwtOptions.TokenValidationParameters.ValidAudience = "ed36281d-955a-4da4-898d-c3f2187767e7";
                        jwtOptions.TokenValidationParameters.ValidIssuer = "a47078d6-821c-4b28-a3a5-efd2bfb61aed";
                    },
                    identityOptions =>
                    {
                        //identityOptions.Domain = "omaha.dev";
                        identityOptions.Instance = "https://login.microsoftonline.com/";
                        identityOptions.TenantId = "a47078d6-821c-4b28-a3a5-efd2bfb61aed";
                        identityOptions.ClientId = "ed36281d-955a-4da4-898d-c3f2187767e7";

                    });

            builder.Services.AddOpenFgaClient(config =>
            {
                config.SetStoreId(Constants.StoreID);
                config.ConfigureOpenFga(fgaConfig =>
                {
                    fgaConfig.SetConnection(Constants.ApiUrl);
                });
            });

            builder.Services.AddOpenFgaMiddleware(config =>
            {
                config.SetUserIdentifier("user", principal => $"{principal.Identity!.Name!}");
            });

            builder.Services.AddAuthorization(authOpt =>
            {
                authOpt.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                authOpt.AddPolicy("Reader", policy =>
                {
                    policy.RequireRole("Application.Read", "Application.Write");
                });

                authOpt.AddPolicy("Writer", policy =>
                {
                    policy.RequireRole("Application.Write");
                });

                //Add the FGA policy
                authOpt.AddPolicy(FgaAuthorizationDefaults.PolicyKey, p => p
                    .RequireAuthenticatedUser()
                    .RequireRole("Application.Read")
                    .AddFgaRequirement());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            //Add AuthZ and AuthN to the request pipeline
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            //Show sensitive information in debug window
            IdentityModelEventSource.ShowPII = true;
            IdentityModelEventSource.LogCompleteSecurityArtifact = true;

            app.Run();
        }
    }
}
