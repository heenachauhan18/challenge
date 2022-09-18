using AutoMapper;
using CosmosCRUD.Data;
using CosmosCRUD.DTOs;
using CosmosCRUD.Mapper;
using CosmosCRUD.Repositories;
using CosmosCRUD.Services;
using CosmosCRUD.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserRequestValidator>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connection with cosmos DB.
builder.Services.AddSingleton<CosmosDbService>(InitializeCosmosClientInstanceAsync(builder.Configuration.GetSection("CosmosDb"))
    .GetAwaiter().GetResult());

// For Mapper.
builder.Services.AddAutoMapper(typeof(Program));
var mapperConfig = new MapperConfiguration(mc => {
    mc.AddProfile(new MapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// MediatR Configuration
builder.Services.AddMediatR(typeof(Program));

// Injecting repository and service dependency
builder.Services.AddTransient<IUsersService, UsersServiceImpl>();
builder.Services.AddTransient<UsersRepository>();
builder.Services.AddSingleton<IValidator<UserRequestDTO>, UserRequestValidator>();

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

// Creates a Cosmos DB database and a container with the specified partition key. 
static async Task<CosmosDbService> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
{
    string databaseName = configurationSection.GetSection("DatabaseName").Value;
    string containerName = configurationSection.GetSection("ContainerName").Value;
    string account = configurationSection.GetSection("Account").Value;
    string key = configurationSection.GetSection("Key").Value;

    Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
    CosmosDbService cosmosDbService = new CosmosDbService(client, databaseName, containerName);

    Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
    await database.Database.CreateContainerIfNotExistsAsync(containerName, "/emailAddress");

    return cosmosDbService;
}