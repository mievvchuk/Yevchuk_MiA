using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation;
using FluentValidation.AspNetCore;
using LW4_Task2_MiA.Repositories;
using LW4_Task4_MiA.Mapping;
using LW4_Task4_MiA.Service;
using LW4_Task4_MiA.Settings;
using LW4_Task6_MiA.Service;
using LW4_Task6_MiA.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Mongo
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDb"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});

// Репозиторії
builder.Services.AddScoped<IRepository<LW4_Task2_MiA.Models.Category>>(sp =>
    new MongoRepository<LW4_Task2_MiA.Models.Category>(sp.GetRequiredService<IMongoDatabase>(),"Categories"));

builder.Services.AddScoped<IRepository<LW4_Task2_MiA.Models.User>>(sp =>
    new MongoRepository<LW4_Task2_MiA.Models.User>(sp.GetRequiredService<IMongoDatabase>(),"Users"));

builder.Services.AddScoped<IRepository<LW4_Task2_MiA.Models.Recipe>>(sp =>
    new MongoRepository<LW4_Task2_MiA.Models.Recipe>(sp.GetRequiredService<IMongoDatabase>(),"Recipes"));

builder.Services.AddScoped<IRepository<LW4_Task2_MiA.Models.Rating>>(sp =>
    new MongoRepository<LW4_Task2_MiA.Models.Rating>(sp.GetRequiredService<IMongoDatabase>(),"Ratings"));

// Сервіси
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IUserService, UserService>();
// Налаштування JWT
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddSingleton<ITokenService, TokenService>();

// Додавання Аутентифікації
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
    if (jwtSettings == null) throw new InvalidOperationException("JwtSettings not configured.");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
        RoleClaimType = ClaimTypes.Role, // Вказуємо, що роль знаходиться в ClaimTypes.Role
        ClockSkew = TimeSpan.Zero // Забираємо 5-хвилинний допуск за замовчуванням
    };
});

// Додавання Авторизації
builder.Services.AddAuthorization();
//AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

builder.Services.AddControllers().AddJsonOptions(o => { }).ConfigureApiBehaviorOptions(o => { });

builder.Services.Configure<Microsoft.AspNetCore.Mvc.MvcOptions>(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Будь ласка, введіть 'Bearer ', а потім ваш токен.",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<LW4_Task2_MiA.Validators.RecipeValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Додаємо аутентифікацію та авторизацію
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
