using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient("CoreApi", c => c.BaseAddress = new Uri(builder.Configuration["CoreApi:BaseUrl"] ?? "https://localhost:5001/"));
builder.Services.AddControllers();
var app = builder.Build();
app.UseAuthorization();
app.MapControllers();
app.Run();
