using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SQLite_CSharp_Table;

public partial class MainWindow
{
    private readonly DatabaseManager _databaseManager;

    public MainWindow()
    {
        InitializeComponent();

        _databaseManager = new DatabaseManager();
        _databaseManager.CreateTable();

        LoadDataFromDataBase();
        DataGridTable.ItemsSource = Items;
    }


    private ObservableCollection<User> Items { get; set; } = new();

    private void LoadDataFromDataBase()
    {
        Items = _databaseManager.GetUsers();
    }


    private void DataGridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DataGridTable.SelectedItem is User selectedUser)
        {
            FirstNameTextBox.Text = selectedUser.FirstName;
            IdTextBox.Text = selectedUser.Id.ToString();
            LastNameTextBox.Text = selectedUser.LastName;
            TelephoneTextBox.Text = selectedUser.Telephone;
            MailTextBox.Text = selectedUser.Email;
        }
        else
        {
            FirstNameTextBox.Text = "";
            IdTextBox.Text = Convert.ToString(_databaseManager.GetMaxId() + 1);
            LastNameTextBox.Text = "";
            MailTextBox.Text = "";
            TelephoneTextBox.Text = "";
        }
    }

    private void InsertButton_Click(object sender, RoutedEventArgs e)
    {
        if (FirstNameTextBox.Text == string.Empty || LastNameTextBox.Text == string.Empty ||
            MailTextBox.Text == string.Empty || TelephoneTextBox.Text == string.Empty)
            MessageBox.Show("Some textbox is empty, please enter info in empty textbox");
        else
            try
            {
                var firstName = FirstNameTextBox.Text;
                var lastName = LastNameTextBox.Text;
                var email = MailTextBox.Text;
                var telephone = TelephoneTextBox.Text;

                Items.Add(new User { FirstName = firstName });
                Items.Add(new User { LastName = lastName });
                Items.Add(new User { Email = email });
                Items.Add(new User { Telephone = telephone });

                _databaseManager.IncrementUsername(firstName, lastName, email, telephone);

                FirstNameTextBox.Text = "";
                IdTextBox.Text = "";
                LastNameTextBox.Text = "";
                MailTextBox.Text = "";
                TelephoneTextBox.Text = "";

                LoadDataFromDataBase();
                DataGridTable.ItemsSource = Items;
                DataGridTable.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (DataGridTable.SelectedItem is User selectedUser)
            {
                selectedUser.FirstName = FirstNameTextBox.Text;
                selectedUser.Id = int.Parse(IdTextBox.Text);
                selectedUser.LastName = LastNameTextBox.Text;
                selectedUser.Email = MailTextBox.Text;
                selectedUser.Telephone = TelephoneTextBox.Text;


                _databaseManager.UpdateUsername(Convert.ToString(selectedUser.Id), selectedUser.FirstName,
                    selectedUser.LastName, selectedUser.Email, selectedUser.Telephone);


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
                FirstNameTextBox.Text = "";
                IdTextBox.Text = "";
                LastNameTextBox.Text = "";
                MailTextBox.Text = "";
                TelephoneTextBox.Text = "";

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