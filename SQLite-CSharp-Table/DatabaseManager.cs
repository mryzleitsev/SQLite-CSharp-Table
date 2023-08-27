using System;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Windows;

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
        try
        {
            string createTableQuery =
                "CREATE TABLE IF NOT EXISTS Users(Id INTEGER PRIMARY KEY, Username TEXT NOT NULL)";
            using (SQLiteCommand command = new SQLiteCommand(createTableQuery, _connection))
            {
                command.ExecuteNonQuery();
            }

            MessageBox.Show("Table created succsessfully.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }

    public void IncrementUsername(string id, string newSuffix)
    {
        string incrementQuery = "UPDATE Users SET Username = Username || @newSuffix WHERE Id = @id";
        using (SQLiteCommand command = new SQLiteCommand(incrementQuery, _connection))
        {
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@newSuffix", newSuffix);
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

    
    public ObservableCollection<MainWindow.Users> GetUsers()
    {
        ObservableCollection<MainWindow.Users> users = new();

        string selectQuery = "SELECT Id, Username FROM Users";
        using (SQLiteCommand command = new SQLiteCommand(selectQuery, _connection))
        {
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string username = reader.GetString(1);
                    
                    users.Add(new MainWindow.Users{Id = id, Name = username});
                }    
            }
        }

        return users;
    }

 

}