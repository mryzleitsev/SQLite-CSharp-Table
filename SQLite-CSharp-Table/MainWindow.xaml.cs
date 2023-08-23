using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SQLite_CSharp_Table
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();

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
            
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        { 
            
        }
        public class Item
        {
            public string Name { get; set; }
            public int Id { get; set; }
        }
    }
}