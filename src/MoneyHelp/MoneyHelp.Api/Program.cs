using MoneyHelp.DataAccess.EntityFramework;
using MoneyHelp.Services;

using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccess(builder.Configuration)
    .AddServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Host.UseNLog();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
