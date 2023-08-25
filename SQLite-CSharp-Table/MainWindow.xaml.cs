using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SQLite_CSharp_Table;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataGridTable.ItemsSource = Items;
    }

    public ObservableCollection<Users> Items { get; set; } = new();


    private void ShowData()
    {
        
    }


    private void DataGridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DataGridTable.SelectedItem is Users selectedUser)
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
            Items.Add(new Users { Name = name, Id = id });

            NameTextBox.Text = "";
            IdTextBox.Text = "";
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
            if (DataGridTable.SelectedItem is Users selectedUser)
            {
                selectedUser.Name = NameTextBox.Text;
                selectedUser.Id = int.Parse(IdTextBox.Text);
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
            if (DataGridTable.SelectedItem is Users selectedUser)
            {
                Items.Remove(selectedUser);
                NameTextBox.Text = "";
                IdTextBox.Text = "";
                DataGridTable.Items.Refresh();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}