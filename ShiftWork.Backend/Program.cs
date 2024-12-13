using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.CodeAnalysis;
//using AutoMapper.Extensions.Microsoft.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using ShiftWork.Backend.Data;
using ShiftWork.Backend.DTOs;
using ShiftWork.Backend.Models;
using ShiftWork.Backend.Services;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using Amazon.S3; // Add this import
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ShiftWorkContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShiftWorkContext") ?? throw new InvalidOperationException("Connection string 'ShiftWorkContext' not found.")));

// Add services to the container.
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetService<IConfiguration>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));
//builder.Services.Add(AppDomain.CurrentDomain.GetAssemblies());

// Register ScheduleShiftService with the DI container


builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IAreaService, AreaServices>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IScheduleShiftService, ScheduleShiftService>();
builder.Services.AddScoped<ITaskShiftService, TaskShiftService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<ICompanyService, CompanyService>();

builder.Services.AddScoped<ILocationRepository<ShiftWork.Backend.Models.Location>, LocationRepository>();
builder.Services.AddScoped<ICompanyRepository<Company>, CompanyRepository>();



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.Authority = "https://securetoken.google.com/shift-maps-location";
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = $"{builder.Configuration["Jwt:authDomain"]}",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:apiKey"])),
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:apiKey"])),
        ValidAudience = builder.Configuration["Jwt:projectId"],
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        //ValidateIssuerSigningKey = true
        //"https://securetoken.google.com/shift-maps-location",
    };
});

var apiCorsPolicy = "ApiCorsPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: apiCorsPolicy,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:4200",
                              "https://localhost:4200",
                              "http://localhost:32773",
                              "https://localhost:32774",
                              "https://main.d23hrr0t3ac536.amplifyapp.com",
                              "https://williamag929-cuddly-space-garbanzo-57v9vvrg9q3px7-4200.preview.app.github.dev")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                          //.WithMethods("OPTIONS", "GET");
                      });
});

// Register the memory cache service
builder.Services.AddMemoryCache();

 


if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDistributedMemoryCache(options =>
    {
        options.SizeLimit = 200 * 1024 * 1024; // 200MB
    });
}
else
{
    builder.Services.AddDistributedMemoryCache(options =>
    {
        options.SizeLimit = 2000 * 1024 * 1024; // 2000MB
    });

   /// builder.Services.AddStackExchangeRedisCache(options =>
   // {
   //     options.Configuration = $"{builder.Configuration["Redis:url"]}:{builder.Configuration["Redis:port"]}";
   //     options.InstanceName = "shift";
   // });
}

builder.Services.AddAWSService<IAmazonS3>(configuration.GetAWSOptions());
builder.Services.AddScoped<IAwsS3Service, AwsS3Service>();

builder.Services.AddLogging(builder => builder.AddConsole());
builder.Services.AddHttpClient<WebhookEmitterService>();
//using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
//ILogger logger = factory.CreateLogger<Program>();

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
