using ArmadaMotors.Api.Extensions;
using ArmadaMotors.Api.Middlewares;
using ArmadaMotors.Data.DbContexts;
using ArmadaMotors.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ArmadaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.ConfigureCors();

builder.Services.ConfigureSwagger();
builder.Services.AddJwtService(builder.Configuration);
builder.Services.AddCustomServices();

var app = builder.Build();

app.InitAccessor();
app.InitEnvironment();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles("/assets");

app.UseAuthorization();

app.MapControllers();

app.Run();
