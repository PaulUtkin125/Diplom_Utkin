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

            builder.Services.AddDistributedMemoryCache();

            // Обязательные сервисы
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDistributedMemoryCache(); // Для хранения сессий в памяти

            builder.Configuration.AddJsonFile("appsettings.json");

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGet("/", async context => context.Response.Redirect("/HomePage/Index"));
            

            app.MapRazorPages();

            app.Run();
        }
    }
}
