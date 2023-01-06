using DemoPractice.Model;
using DemoPractice.Repository;
using DemoPractice.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICommonDropDownRepository, CommonDropDownRepository>();
builder.Services.AddScoped<IUserRegistration, UserRegistration>();
builder.Services.AddScoped<IReligionRepository, ReligionRepository>();
builder.Services.AddSingleton<BaseAsyncRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
