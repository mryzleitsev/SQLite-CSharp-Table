using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SQLite_CSharp_Table;

public partial class MainWindow : Window
{
    private readonly DatabaseManager _databaseManager;

    public MainWindow()
    {
        InitializeComponent();

        _databaseManager = new DatabaseManager();
        _databaseManager?.CareateTable();

        LoadDataFromDB();
        DataGridTable.ItemsSource = Items;
    }

    public ObservableCollection<User> Items { get; set; } = new();

    private void LoadDataFromDB()
    {
        Items = _databaseManager.GetUsers();
    }

    private void DataGridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DataGridTable.SelectedItem is User selectedUser)
        {
            NameTextBox.Text = selectedUser.Name;
            IdTextBox.Text = selectedUser.Id.ToString();
        }
        else
        {
            NameTextBox.Text = "";
            IdTextBox.Text = "";
        }
    }

    private void InsertButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var name = NameTextBox.Text;
            var id = int.Parse(IdTextBox.Text);
            Items.Add(new User { Name = name, Id = id });

            _databaseManager.IncrementUsername(Convert.ToString(id), name);

            NameTextBox.Text = "";
            IdTextBox.Text = "";


            DataGridTable.ItemsSource = Items;
            DataGridTable.Items.Refresh();
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (DataGridTable.SelectedItem is User selectedUser)
            {
                selectedUser.Name = NameTextBox.Text;
                selectedUser.Id = int.Parse(IdTextBox.Text);

                _databaseManager.UpdateUsername(Convert.ToString(selectedUser.Id), selectedUser.Name);

                DataGridTable.ItemsSource = Items;
                DataGridTable.Items.Refresh();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (DataGridTable.SelectedItem is User selectedUser)
            {
                _databaseManager.DeleteUser(Convert.ToString(selectedUser.Id));

                Items.Remove(selectedUser);
                NameTextBox.Text = "";
                IdTextBox.Text = "";

                DataGridTable.ItemsSource = Items;
                DataGridTable.Items.Refresh();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}