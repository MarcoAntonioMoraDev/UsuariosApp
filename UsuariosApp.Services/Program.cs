using UsuariosApp.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwagger();
builder.Services.AddCorsConfig();
builder.Services.AddDependencyInjection();

var app = builder.Build();

app.UseSwaggerDoc();
app.UseCorsConfig();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }
