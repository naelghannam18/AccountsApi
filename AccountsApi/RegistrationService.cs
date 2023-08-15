﻿
using Core.Services.Contracts;
using Core.Services.Implementations;
using Domain.Configurations;
using Infrastructure.Mappings;
using Infrastructure.Repositories.Contracts;
using Infrastructure.Repositories.Implementations;

namespace AccountsApi;

public static class RegistrationService
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.RegisterConfigurationClasses();
        builder.RegisterDatabaseServices();
        builder.RegisterRepositories();
        builder.RegisterApplicationServices();
    }

    private static void RegisterDatabaseServices(this WebApplicationBuilder builder)
    {
    }

    private static void RegisterRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IAccountsRepository, AccountsRepository>();
        builder.Services.AddTransient<ICustomersRepository, CustomersRepository>();
        builder.Services.AddTransient<ITransactionsRepository, TransactionsRepository>();

    }

    private static void RegisterApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(Mappings));
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        builder.Services.AddScoped<ITransactionService, TransactionService>();
        builder.Services.AddScoped<IAccountService, AccountService>();
    }

    private static void RegisterConfigurationClasses(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<MongoDbConfiguration>(builder.Configuration.GetSection("MongoDB"));
    }
}
