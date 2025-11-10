using FluentValidation;
using FluentValidation.AspNetCore;
using LW4_Task2_MiA.Repositories;
using LW4_Task4_MiA.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using LW4_Task4_MiA.Service;

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

builder.Services.AddControllers().AddJsonOptions(o => { }).ConfigureApiBehaviorOptions(o => { });

builder.Services.Configure<Microsoft.AspNetCore.Mvc.MvcOptions>(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<LW4_Task2_MiA.Validators.RecipeValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
