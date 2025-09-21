using Bainah.Core.Entities;
using Bainah.Core.Interfaces;
using Bainah.Infrastructure.Persistence;
using Bainah.Infrastructure.Repositories;
using Bainah.Infrastructure.Security;
using Core.Interfaces;
using CoreApi;
using Infrastructure.Repositories;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// SQL Server connection (local SQL Express)
builder.Services.AddDbContext<IdentityContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

// Repositories
builder.Services.AddAppServices();



// Localization
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("ar-SA"),
    };

    options.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
    options.SupportedCultures = supportedCultures;
});

// JWT configuration
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
var jwtOpt = builder.Configuration.GetSection("Jwt").Get<JwtOptions>()
             ?? new JwtOptions { Secret = "DevSecretKeyDontUseInProd123!", ExpiryMinutes = 60 };

var key = Encoding.UTF8.GetBytes(jwtOpt.Secret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization();

// Swagger with JWT support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bainah API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Enter: Bearer {your JWT token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Swagger only in Dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Authentication / Authorization
app.UseAuthentication();
app.UseAuthorization();

// Localization middleware
app.Use(async (context, next) =>
{
    var cultureQuery = context.Request.Headers["Accept-Language"];
    try
    {
        if (!string.IsNullOrWhiteSpace(cultureQuery))
        {
            var culture = new CultureInfo(cultureQuery);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }
    }
    catch (CultureNotFoundException)
    {
        var defaultCulture = new CultureInfo("ar");
        CultureInfo.CurrentCulture = defaultCulture;
        CultureInfo.CurrentUICulture = defaultCulture;
    }

    await next(context);
});

app.MapControllers();

// 🔹 Optional: Seed roles, users, and default pages
// SeedData.EnsureSeeded(app);

app.Run();
