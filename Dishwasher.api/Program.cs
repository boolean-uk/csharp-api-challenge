using Dishwasher.api;
using Dishwasher.engine;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DishwasherProgramsData>();
builder.Services.AddScoped<DishwasherProgramsRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.ConfigureDishwasherProgramsEndpoint();

app.Run();
