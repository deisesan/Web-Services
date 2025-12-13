using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TarefasBackEnd.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
            
var key = Encoding.ASCII.GetBytes("UmTokenMuitoGrandeEDiferenteParaNinguemDescobrir");

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
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

builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("DBTarefas"));
builder.Services.AddTransient<ITarefaRepository, TarefaRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        
        var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;
        
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, "Erro não tratado: {Message}", exception?.Message);
        
        var errorResponse = System.Text.Json.JsonSerializer.Serialize(new
        {
            error = "Erro interno do servidor",
            message = exception?.Message ?? "Ocorreu um erro inesperado",
            stackTrace = app.Environment.IsDevelopment() ? exception?.StackTrace : null
        });
        
        await context.Response.WriteAsync(errorResponse);
    });
});

app.UseAuthentication();  
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
