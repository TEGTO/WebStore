using AuthenticationManager;
using AuthenticationManager.Models;
using AuthenticationManager.Services;
using AuthenticationWebApi.Data;
using AuthenticationWebApi.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Middlewares.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WebStoreDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<WebStoreDbContext>()
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

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
                .Where(x => x.Value.ValidationState == ModelValidationState.Invalid)
                .SelectMany(x => x.Value.Errors.Select(e => new FluentValidation.Results.ValidationFailure(x.Key, e.ErrorMessage)))
                .ToList();
        throw new ValidationException(errors);
    };
});

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

app.UseHttpsRedirection();
app.UseExceptionMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();