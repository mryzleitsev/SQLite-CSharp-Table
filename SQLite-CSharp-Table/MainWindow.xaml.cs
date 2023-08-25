using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace SQLite_CSharp_Table;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataGridTable.ItemsSource = Items;
    }

    public ObservableCollection<Item> Items { get; set; } = new();


    private void ShowData()
    {
        
    }


    private void InsertButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var name = NameTextBox.Text;
            var id = int.Parse(IdTextBox.Text);
            Items.Add(new Item { Name = name, Id = id });

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
            
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}