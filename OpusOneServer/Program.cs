using System.Text.Json;
using System.Text.Json.Serialization;
using OpusOneServerBL.Models;
using Microsoft.EntityFrameworkCore;

namespace OpusOneServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region DB_CONTEXT
            string connection = builder.Configuration.GetConnectionString("OpusOneDB");
            builder.Services.AddDbContext<OpusOneDbContext>(options => options.UseSqlServer(connection));
            #endregion

            builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(180);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();

            app.MapControllers();

            app.Run();

        }
    }
}