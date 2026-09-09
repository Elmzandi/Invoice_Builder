using Microsoft.EntityFrameworkCore;
using InvoiceBuilder.Api.Shared.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention());
    
var app = builder.Build();

app.UseHttpsRedirection();

app.Run();

