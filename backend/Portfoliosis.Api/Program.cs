
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Diagnostics;
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
            var logger = CreateTemporaryLogger("Program.cs");
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

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler(appBuilder =>
                    appBuilder.Run(async context =>
                    {
                        var handlerFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                        var exception = handlerFeature?.Error;

                        if (exception is ValidationException)
                        {
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        }
                        else
                        {
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsJsonAsync(new
                            {
                                StatusCode = StatusCodes.Status500InternalServerError,
                                Message = "An internal error occurred"
                            });

                        }
                    }));
            }

            // Add exception handling middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // ensure database is created
            using (var scope = app.Services.CreateScope())
            {
                bool isSuccessful = false;
                int increasingDelaySeconds = 2;
                Exception databaseException = null!;
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // try three times, then raise exception
                for (int i = 1; i < 4; i++)
                {
                    try
                    {
                        // sometimes database needs time to start up, so we'll wait
                        increasingDelaySeconds = (int)Math.Pow(increasingDelaySeconds, i);
                        Thread.Sleep(increasingDelaySeconds * 1000);
                        logger.LogInformation("Trying to ensure database is created...");
                        context.Database.EnsureCreated();
                        logger.LogInformation("Success! Database ensured.");
                        isSuccessful = true;
                        break;
                    }
                    catch (Exception e)
                    {
                        databaseException = e;
                        logger.LogWarning("Could not ensure that database is created, retrying...");
                    }
                }

                if (isSuccessful == false)
                {
                    throw new InvalidOperationException("Could not ensure that database is created!", databaseException) ;
                }
            }

            app.UseAuthorization();

            app.MapControllers();

            logger.LogInformation("Program.cs completed, starting up the App...");

            app.Run();
        }
        private static ILogger CreateTemporaryLogger(string categoryName) => LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger(categoryName);
    }
}
