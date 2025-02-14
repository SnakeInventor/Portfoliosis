
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Portfoliosis.Application.Commands;
using Portfoliosis.Core.Repositories;
using Portfoliosis.Infrastructure;

namespace Portfoliosis.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Repositories
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();

            // Add UnitOfWork
            builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();

            // MediatR and CQRS
            builder.Services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblyContaining<SubmitMessageCommand>();
            });

            // Data access

            var dbSection = builder.Configuration.GetSection("DB");
            string connection = $"Host={dbSection["HOST"]};" +
                                $"Port={dbSection["PORT"]};" +
                                $"Database={dbSection["NAME"]};" +
                                $"Username={dbSection["USER"]};" +
                                $"Password={dbSection["PASSWORD"]};";
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseNpgsql(connection)
                );

            
            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                string xmlPath = Path.Combine(AppContext.BaseDirectory, assemblyName + ".xml");
                c.IncludeXmlComments(xmlPath);
            });

            var app = builder.Build();

            // ensure database is created
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
