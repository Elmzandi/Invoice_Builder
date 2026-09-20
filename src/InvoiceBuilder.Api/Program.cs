using Microsoft.EntityFrameworkCore;
using InvoiceBuilder.Api.Shared.Database;
using InvoiceBuilder.Api.Modules.Users.Authentication;
using InvoiceBuilder.Api.Modules.Users.Authentication.Registration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention());
    
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();    
builder.Services.AddScoped<RegisterService>();
builder.Services.AddScoped<RegisterHandler>();
var app = builder.Build();

app.UseHttpsRedirection();
app.MapPost("/test", () => "Hello");
app.MapRegisterEndpoint();
app.Run();

