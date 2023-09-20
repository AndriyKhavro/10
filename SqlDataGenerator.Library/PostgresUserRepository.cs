using MySqlTest;
using Npgsql;
using NpgsqlTypes;

namespace SqlDataGenerator.Library;

public sealed class PostgresUserRepository : IUserRepository
{
    private readonly NpgsqlConnection _connection;
    private bool _isConnectionOpen;

    public PostgresUserRepository(string connectionString)
    {
        _connection = new NpgsqlConnection(connectionString);
    }

    public void OpenConnection()
    {
        _connection.Open();

        _isConnectionOpen = true;

        Console.WriteLine($"Connection opened. Connection Timeout: {_connection.ConnectionTimeout}.");
    }

    public void InsertBatch(IEnumerable<User> users)
    {
        EnsureConnectionOpened();

        using var transaction = _connection.BeginTransaction();
        using var command = CreateInsertCommand();
        command.Transaction = transaction;

        foreach (var user in users)
        {
            SetUser(command, user);

            command.ExecuteNonQuery();
        }

        transaction.Commit();
    }


    public void Dispose()
    {
        _connection.Close();
        _connection.Dispose();
    }

    private void EnsureConnectionOpened()
    {
        if (!_isConnectionOpen)
        {
            OpenConnection();
        }
    }

    private static void SetUser(NpgsqlCommand command, User user)
    {
        command.Parameters["@username"].Value = user.Username;
        command.Parameters["@email"].Value = user.Email;
        command.Parameters["@first_name"].Value = user.FirstName;
        command.Parameters["@last_name"].Value = user.LastName;
        command.Parameters["@date_of_birth"].Value = user.DateOfBirth;
    }

    private NpgsqlCommand CreateInsertCommand()
    {
        var command = _connection.CreateCommand();
        command.CommandText = "INSERT INTO users (username, email, first_name, last_name, date_of_birth) " +
                              "VALUES (@username, @email, @first_name, @last_name, @date_of_birth)";

        command.Parameters.Add("@username", NpgsqlDbType.Varchar);
        command.Parameters.Add("@email", NpgsqlDbType.Varchar);
        command.Parameters.Add("@first_name", NpgsqlDbType.Varchar);
        command.Parameters.Add("@last_name", NpgsqlDbType.Varchar);
        command.Parameters.Add("@date_of_birth", NpgsqlDbType.Date);
        return command;
    }
}