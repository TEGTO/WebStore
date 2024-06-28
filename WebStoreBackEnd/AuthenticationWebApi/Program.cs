using AuthenticationManager;
using AuthenticationManager.Models;
using AuthenticationManager.Services;
using AuthenticationWebApi.Data;
using AuthenticationWebApi.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Middlewares;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuthIdentityDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AuthenticationConnection")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AuthIdentityDbContext>()
.AddDefaultTokenProviders();

var jwtSettings = new JwtSettings()
{
    Key = builder.Configuration["JwtSettings:Key"],
    Audience = builder.Configuration["JwtSettings:Audience"],
    Issuer = builder.Configuration["JwtSettings:Issuer"],
    ExpiryInMinutes = Convert.ToDouble(builder.Configuration["JwtSettings:ExpiryInMinutes"]),
};
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthorization();

builder.Services.AddScoped<JwtHandler>();
builder.Services.AddCustomJwtAuthentication(jwtSettings);

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
ValidatorOptions.Global.LanguageManager.Enabled = false;

builder.Services.ConfigureCustomInvalidModelStateResponseContollers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureDatabase<AuthIdentityDbContext>();

app.UseHttpsRedirection();
app.UseExceptionMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();