using LW4.Task1_MiA.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Recipes API v1");
        opt.RoutePrefix = "swagger";
    });
}
app.MapRecipeEndpoints();
app.MapUserEndpoints();
app.MapRatingEndpoints();
app.Run();
