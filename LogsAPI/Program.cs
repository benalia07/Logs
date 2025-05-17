using LogsAPI.Controllers;
using LogsAPI.Service;
using LogsAPI.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(); 



builder.Services.AddScoped<ILogEntryStorage>(provider =>
    new LogEntryStorage(builder.Configuration.GetConnectionString("INV")));

builder.Services.AddScoped<ILogEntryService, LogEntryService>(); 



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(); 
}

app.UseAuthorization(); 
app.MapControllers(); 


app.Run();