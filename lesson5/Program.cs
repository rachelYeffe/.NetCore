using Tasks.Utilities;
using Middlware;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTask();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.Map("/favicon.ico", (a) =>
    a.Run(async c => await Task.CompletedTask));

app.UseMyLogMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
/*js*/
app.UseDefaultFiles();
app.UseStaticFiles();
/*js (remove "launchUrl" from Properties\launchSettings.json*/
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
