using AppStock.Data;
using AppStock.Services;
using AppStock.Services.StockService;
using Microsoft.EntityFrameworkCore;

namespace AppStock
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            var connectionstring = builder.Configuration.GetConnectionString("cs");

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionstring);
            });
            builder.Services.Configure<TradingOptions>(
             builder.Configuration.GetSection("TradingOptions"));


            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IStockService, StockService>();
            builder.Services.AddHttpClient<IFinnhubService, FinnhubService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Trade}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
