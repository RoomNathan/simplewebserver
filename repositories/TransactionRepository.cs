using Microsoft.Data.SqlClient;
using Howest.Function;

namespace Howest.Function;

public class TransactionRepository
{
    private readonly string _connectionString;

    public TransactionRepository()
    {
        _connectionString = Environment.GetEnvironmentVariable("SqlConnectionString") ?? throw new InvalidOperationException("SqlConnectionString environment variable is not set");
    }

    public async Task<List<CoffeeTransaction>> GetAllTransactionsAsync()
    {
        List<CoffeeTransaction> transactions = new List<CoffeeTransaction>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT ID, DateTime, CashType, Card, Money, CoffeeName FROM Transactions";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            CoffeeTransaction transaction = new CoffeeTransaction
                            {
                                ID = reader.GetGuid(reader.GetOrdinal("ID")),
                                DateTime = reader.GetDateTime(reader.GetOrdinal("DateTime")),
                                CashType = reader["CashType"] as string ?? string.Empty,
                                Card = reader["Card"] as string ?? string.Empty,
                                Money = reader.GetDecimal(reader.GetOrdinal("Money")),
                                CoffeeName = reader["CoffeeName"] as string ?? string.Empty
                            };
                            transactions.Add(transaction);
                        }
                    }
                }
                catch (Exception)
                {
                    throw; // Re-throw the exception to be handled by the Azure Function runtime
                }
            }
        }

        return transactions;
    }

    public async Task<List<CoffeeNameResult>> GetAllCoffeeNamesAsync()
    {
        var coffeeNames = new List<CoffeeNameResult>();

        var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync();
        var query = "SELECT DISTINCT CoffeeName FROM Transactions";

        var command = new SqlCommand(query, connection);
        var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var record = new CoffeeNameResult();
            record.CoffeeName = reader["CoffeeName"] as string ?? string.Empty;
            coffeeNames.Add(record);
        }
        await reader.CloseAsync();
        await connection.CloseAsync();

        return coffeeNames;
    }
}
  