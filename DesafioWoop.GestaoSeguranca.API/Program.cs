using DesafioWoop.GestaoSeguranca.API.Core;
using DesafioWoop.GestaoSeguranca.API.Data;
using DesafioWoop.GestaoSeguranca.API.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MediatR;
using DesafioWoop.GestaoSeguranca.API.Commands;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddJwtConfiguration(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
                                {
                                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                                    {
                                        Name = "Authorization",
                                        Type = SecuritySchemeType.ApiKey,
                                        Scheme = "Bearer",
                                        BearerFormat = "JWT",
                                        In = ParameterLocation.Header,
                                        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer'[space] and then your token in the text input below. Example: \"Bearer 12345abcdef\"",
                                    });
                                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                                    {
                                        {
                                          new OpenApiSecurityScheme
                                          {
                                              Reference = new OpenApiReference
                                              {
                                                  Type = ReferenceType.SecurityScheme,
                                                  Id = "Bearer"
                                              }
                                          },
                                         new string[] {}
                                        }
                                    });
                                });


builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IQuestionarioUsuarioRepository, QuestionarioUsuarioRepository>();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
