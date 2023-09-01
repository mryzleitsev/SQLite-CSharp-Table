using System;
using System.Collections.ObjectModel;
using System.Data.SQLite;

namespace SQLite_CSharp_Table;

public class DatabaseManager
{
    private readonly SQLiteConnection _connection;

    public DatabaseManager()
    {
        _connection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
        _connection.Open();
    }

    public void CareateTable()
    {
        var createTableQuery =
            "CREATE TABLE IF NOT EXISTS Users(Id INTEGER PRIMARY KEY AUTOINCREMENT, Username TEXT NOT NULL)";
        using (var command = new SQLiteCommand(createTableQuery, _connection))
        {
            command.ExecuteNonQuery();
        }
    }


    public void IncrementUsername(string newUsername)
    {
        var incrementQuery = "INSERT INTO Users (Username) VALUES (@Username)";
        using (var command = new SQLiteCommand(incrementQuery, _connection))
        {
            command.Parameters.AddWithValue("@Username", newUsername);
            command.ExecuteNonQuery();
        }
    }
    
    public void UpdateUsername(string id, string newUsername)
    {
        var updateQuery = "UPDATE Users SET Username = @Username WHERE Id = @Id";
        using (var command = new SQLiteCommand(updateQuery, _connection))
        {
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Username", newUsername);
            command.ExecuteNonQuery();
        }
    }

    public void DeleteUser(string id)
    {
        var deleteQuery = "DELETE FROM Users WHERE Id = @id";
        using (var command = new SQLiteCommand(deleteQuery, _connection))
        {
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }

    public void CloseConnection()
    {
        _connection.Close();
    }
    
    public ObservableCollection<User> GetUsers()
    {
        ObservableCollection<User> users = new();

        var selectQuery = "SELECT Id, Username FROM Users";
        using (var command = new SQLiteCommand(selectQuery, _connection))
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var username = reader.GetString(1);

                    users.Add(new User { Id = id, Name = username });
                }
            }
        }

        return users;
    }
    public int GetMaxId()
    {
        var selectMaxIdQuery = "SELECT MAX(Id) FROM Users";
        using (var command = new SQLiteCommand(selectMaxIdQuery, _connection))
        {
            var result = command.ExecuteScalar();
            if (result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }
            return 0; 
        }
    }

}