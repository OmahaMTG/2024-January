using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;

namespace RBAC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            builder.Services.AddAuthorization(authOpt =>
            {
                authOpt.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                authOpt.AddPolicy("Reader", policy =>
                {
                    policy
                        .RequireAuthenticatedUser()
                        .RequireRole("Application.Read", "Application.Write");
                });

                authOpt.AddPolicy("Writer", policy =>
                {
                    policy
                        .RequireAuthenticatedUser()
                        .RequireRole("Application.Write");
                });
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
