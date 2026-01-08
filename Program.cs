
using Microsoft.EntityFrameworkCore;
using SF_API.Common;
using SF_API.Data;
using SF_API.Interfaces;
using SF_API.Services;
using SF_API.Utils;
using System.Text.Json.Serialization;

namespace SF_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddScoped<IFighterService, FighterService>();
            builder.Services.AddScoped<IGameService, GameService>();
            builder.Services.AddScoped<IFighterVersionService, FighterVersionService>();
            builder.Services.AddScoped<IFighterMoveService, FighterMoveService>();
            builder.Services.AddScoped<IImageService, ImageService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseExceptionMiddleware();
            app.MapControllers();

            app.Run();
        }
    }
}
