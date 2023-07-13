using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using FindMeApartmentsWebApp.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using System.Text; // include at the top
using System.Collections;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using FindMeApartmentsWebApp.Interfaces;
using FindMeApartmentsWebApp.Repos;
using Microsoft.Extensions.Options;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("MongoDBConnection"); //it fetch connection from appsettings.json

builder.Services.AddOptions<MongodbSettings>().Bind(configuration.GetSection("MongodbSettings"));
builder.Services.AddSingleton<IMongoClient>(s =>    //dependency injection
{
    var connectionString= s.GetService<IOptions<MongodbSettings>>()?.Value.connectionString;
    var settings = MongoClientSettings.FromConnectionString(connectionString);
    settings.ServerApi = new ServerApi(ServerApiVersion.V1);

    return new MongoClient(settings);
});

builder.Services.AddScoped<IZillowRepository, MongoZillowRepository>(); //dependency injection
// builder.Services.AddScoped<IZillowRepository,JsonZillowRepository>(); //dependency injection

builder.Services.AddControllers();
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
