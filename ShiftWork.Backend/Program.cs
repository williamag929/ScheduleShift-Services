using AutoMapper;
//using AutoMapper.Extensions.Microsoft.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ShiftWork.Backend.Data;
using ShiftWork.Backend.DTOs;
using ShiftWork.Backend.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ShiftWorkContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShiftWorkContext") ?? throw new InvalidOperationException("Connection string 'ShiftWorkContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(typeof(Program));
//builder.Services.Add(AppDomain.CurrentDomain.GetAssemblies());

var apiCorsPolicy = "ApiCorsPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: apiCorsPolicy,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:4200", 
                              "https://localhost:4200",
                              "https://main.d23hrr0t3ac536.amplifyapp.com")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                          //.WithMethods("OPTIONS", "GET");
                      });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();
    //app.UseMigrationsEndPoint();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("ApiCorsPolicy");
app.UseAuthorization();
app.MapControllers();
app.Run();
