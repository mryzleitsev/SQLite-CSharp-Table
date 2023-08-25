using System.Data.SQLite;

namespace SQLite_CSharp_Table;

public class DatabaseManager
{
    private SQLiteConnection _connection;
    
    public DatabaseManager()
    {
        _connection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
        _connection.Open();
    }

    public void CareateTable()
    {
        string createTableQuery =
            "CREATE TABLE IF NOT EXISTS Users(Id INTEGER PRIMARY KEY, Username TEXT, Password TEXT)";
        using (SQLiteCommand command = new SQLiteCommand(createTableQuery, _connection))
        {
            command.ExecuteNonQuery();
        }
    }

    public void IncrementUsername(string id)
    {
        string incrementQuery = "UPDATE Users SET Username = Username || '_new' WHERE Id = @id";
        using (SQLiteCommand command = new SQLiteCommand(incrementQuery, _connection))
        {
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }

    public void UpdateUsername(string id, string newUsername)
    {
        string updateQuery = "UPDATE Users SET Username = @newUsername WHERE Id = @id";
        using (SQLiteCommand command = new SQLiteCommand(updateQuery, _connection))
        {
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@newUsername", newUsername);
            command.ExecuteNonQuery();
        }
    }

    public void DeleteUser(string id)
    {
        string deleteQuery = "DELETE FROM Users WHERE Id = @id";
        using (SQLiteCommand command = new SQLiteCommand(deleteQuery, _connection))
        {
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }

    public void CloseConnection()
    {
        _connection.Close();
    }

}