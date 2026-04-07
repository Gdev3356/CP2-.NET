using Microsoft.EntityFrameworkCore;
using Contra_Filé.Application.Services;
using Contra_Filé.Infrastructure.Persistence;


namespace Contra_Filé.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddDbContext<ContraFiléContext>(options =>
        {
            // Conexão com o MySQL
            var connectionString = builder.Configuration.GetConnectionString("ContraFiléMySQL");
            options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));
        });
        
        builder.Services.AddScoped<IPedidoService, PedidoService>();

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}