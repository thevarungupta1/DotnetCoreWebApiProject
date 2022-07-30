using DotnetCoreWebApiProject.Models;
using DotnetCoreWebApiProject.Services;
using DotnetCoreWebApiProject.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddDbContextPool<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("mydb"));
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
