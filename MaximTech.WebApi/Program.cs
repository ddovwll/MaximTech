using MaximTech.Application.Contracts;
using MaximTech.Application.Services;
using MaximTech.Application.Services.Sort;
using MaximTech.Domain.Contracts;
using MaximTech.Domain.Models;
using Microsoft.Extensions.Options;
using RandomNumberGenerator = MaximTech.Infrastructure.Services.RandomNumberGenerator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));
builder.Services.AddSingleton(x => x.GetRequiredService<IOptions<Settings>>().Value);

builder.Services.AddHttpClient<IRandomNumberGenerator, RandomNumberGenerator>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["RandomApi"]);
});
builder.Services.AddScoped<ISortFactory, SortFactory>();
builder.Services.AddSingleton<ISort, TreeSort>();
builder.Services.AddSingleton<ISort, QuickSort>();
builder.Services.AddScoped<StringProcessService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();