using MySqlTest;

namespace SqlDataGenerator.Library;

public interface IUserRepository : IDisposable
{
    void OpenConnection();
    void InsertBatch(IEnumerable<User> users);
}