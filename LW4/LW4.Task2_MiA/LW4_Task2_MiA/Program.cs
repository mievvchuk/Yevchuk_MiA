using FluentValidation;
using FluentValidation.AspNetCore;
using LW4_Task2_MiA.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<InMemoryDb>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// FluentValidation
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
