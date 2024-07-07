using AuthenticationManager;
using AuthenticationManager.Configuration;
using AuthenticationManager.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MessageQueue.Configuration;
using MessageQueue.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Middlewares;
using Shared;
using WebStoreApi.Data;
using WebStoreApi.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("WebStoreConnection") ?? throw new InvalidOperationException("Connection string 'WebStoreConnection' is not found.");
builder.Services.AddDbContextFactory<WebStoreDbContext>(options =>
    options.UseNpgsql(connectionString));

var jwtSettings = new JwtSettings
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

var messageQueueFactorySettings = new FactorySettings
{
    HostName = builder.Configuration["MessageQueue:HostName"],
    Port = Convert.ToInt32(builder.Configuration["MessageQueue:Port"]),
    UserName = builder.Configuration["MessageQueue:UserName"],
    Password = builder.Configuration["MessageQueue:Password"],
};
builder.Services.AddSingleton(messageQueueFactorySettings);
builder.Services.AddSingleton<IMessageQueueService, MessageQueueService>();

builder.Services.AddSingleton<IProductsService, ProductsService>();
builder.Services.AddSingleton<IUserCartService, UserCartService>();

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

builder.Services.AddOutputCache();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:ConnectionString"];
    options.InstanceName = "redis-webstore";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureDatabase<WebStoreDbContext>();

app.UseHttpsRedirection();

app.UseOutputCache();

app.UseExceptionMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();