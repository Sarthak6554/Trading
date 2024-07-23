using Trading.API.Factory.DataSourceFactory;
using Trading.API.Interface;
using Trading.API.Services;
using Trading.API.Services.PredictionAlgorithms;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IStockPrediction, StockPrediction>();
builder.Services.AddSingleton<IStockPredictionAlgorithm, AverageValuePrediction>();
builder.Services.AddSingleton<CsvDataSourceFactory>();

builder.Services.AddSingleton<DataSourceFactoryResolver>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
