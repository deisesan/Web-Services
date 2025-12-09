using Microsoft.EntityFrameworkCore;
using TarefasBackEnd.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("DBTarefas"));
builder.Services.AddTransient<ITarefaRepository, TarefaRepository>();

var app = builder.Build();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
