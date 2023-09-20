using MySqlTest;
using Polly;
using SqlDataGenerator.Library;

int batchSize = 10000;
int totalUsers = 40000000;

var dataGenerator = new DataGenerator();
using var userRepository = CreateUserRepository(args);
userRepository.OpenConnection();

var policy = Policy.Handle<Exception>().RetryForever(Console.WriteLine);

for (int i = 0; i < totalUsers; i += batchSize)
{
    var batch = dataGenerator.GenerateUserBatch(i, batchSize);
    Console.WriteLine($"Generated batch #{i}");
    policy.Execute(() => userRepository.InsertBatch(batch));
    Console.WriteLine($"Inserted batch #{i}");
}


IUserRepository CreateUserRepository(string[] args)
{
    if (args.Any(arg => arg.Contains("mysql")))
    {
        return new MySqlUserRepository("Server=localhost;Port=3306;Database=users_db;Uid=root;Pwd=example;");
    }

    if (args.Any(arg => arg.Contains("postgres")))
    {
        return new PostgresUserRepository("Server=localhost;Port=5432;Database=users_db;Uid=postgres;Pwd=example;");
    }

    throw new ArgumentException("Please specify database type.", nameof(args));
}