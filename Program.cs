using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));

// <snippet_UseSwagger>
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}
// </snippet_UseSwagger>

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
