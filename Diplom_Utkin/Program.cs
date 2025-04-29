using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NuGet.Configuration;
using System.Net;
using System.Text;

namespace Diplom_Utkin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromMinutes(5);
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtKey = builder.Configuration["JWT:Key"] ??
                    throw new InvalidOperationException("JWT Key is not Configured");
                var jwtIssuer = builder.Configuration["JWT:Issuer"] ??
                    throw new InvalidOperationException("JWT Issuer is not Configured");
                var jwtAudience = builder.Configuration["JWT:Audience"] ??
                    throw new InvalidOperationException("JWT Audience is not Configured");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = 
                        new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(jwtKey))
                };
            });
            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGet("/", async context => context.Response.Redirect("/LoginForm/Index"));
            

            app.MapRazorPages();

            app.Run();
        }
    }
}
