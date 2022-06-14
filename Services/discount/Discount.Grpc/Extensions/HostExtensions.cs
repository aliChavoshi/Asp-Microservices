using Npgsql;

namespace Discount.Grpc.Extensions;

public static class HostExtensions
{
    public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
    {
        var retryForAvailability = retry.Value;

        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var configuration = services.GetRequiredService<IConfiguration>();
        var logger = services.GetRequiredService<ILogger<TContext>>();

        try
        {
            logger.LogInformation("Migrating postresql database.");

            using var connection = new NpgsqlConnection
                (configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            using var command = new NpgsqlCommand
            {
                Connection = connection
            };

            command.CommandText = "DROP TABLE IF EXISTS Coupons";
            command.ExecuteNonQuery();

            command.CommandText = @"CREATE TABLE Coupons(Id SERIAL PRIMARY KEY, 
                                                                ProductName VARCHAR(24) NOT NULL,
                                                                Description TEXT,
                                                                Amount INT)";
            command.ExecuteNonQuery();

            command.CommandText =
                "INSERT INTO Coupons(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
            command.ExecuteNonQuery();

            command.CommandText =
                "INSERT INTO Coupons(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
            command.ExecuteNonQuery();

            logger.LogInformation("Migrated postresql database.");
        }
        catch (NpgsqlException ex)
        {
            logger.LogError(ex, "An error occurred while migrating the postresql database");

            if (retryForAvailability < 50)
            {
                retryForAvailability++;
                Thread.Sleep(2000);
                host.MigrateDatabase<TContext>(retryForAvailability);
            }
        }

        return host;
    }
}