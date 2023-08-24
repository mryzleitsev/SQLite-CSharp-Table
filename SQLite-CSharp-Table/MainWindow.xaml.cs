using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SQLite;

namespace SQLite_CSharp_Table
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
        private string path = "Data_Table.db";
        private string cs = @"URI=file:" + AppDomain.CurrentDomain.BaseDirectory + "\\data_table.db";

        private SQLiteConnection con;
        private SQLiteCommand cmd;
        private SQLiteDataReader dr;

        public MainWindow()
        {
            InitializeComponent();
            DataGridTable.ItemsSource = Items;

            DataGridTextColumn nameColumn = new DataGridTextColumn();
            nameColumn.Header = "Name";
            nameColumn.Binding = new Binding("Name");

            DataGridTextColumn idColumn = new DataGridTextColumn();
            idColumn.Header = "Id";
            idColumn.Binding = new Binding("Id");
        }

        private void ShowData()
        {
            con = new SQLiteConnection(cs);
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NameTextBox.Text;
                int id = int.Parse(IdTextBox.Text);
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
}
