
using Microsoft.EntityFrameworkCore;
using SF_API.Common;
using SF_API.Data;
using SF_API.Interfaces;
using SF_API.Services;
using SF_API.Utils;

namespace SF_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddScoped<IFighterService, FighterService>();
            builder.Services.AddScoped<IGameService, GameService>();
            builder.Services.AddScoped<IFighterVersionService, FighterVersionService>();
            builder.Services.AddScoped<IFighterMoveService, FighterMoveService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.UseAuthorization();

            app.UseExceptionMiddleware();
            app.MapControllers();

            app.Run();
        }
    }
}
