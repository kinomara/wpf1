using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Clients.ClientContext db;
        public MainWindow()
        {
            InitializeComponent();
            db = new Clients.ClientContext();
            try
            {
                // загрузка данных
                db.Clients.Load();
                clientsGrid.ItemsSource = db.Clients.Local.ToBindingList();
                (clientsGrid.Columns[3] as DataGridTextColumn).Binding.StringFormat = "dd-MM-yyyy";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // удаление записи
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (clientsGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < clientsGrid.SelectedItems.Count; i++)
                {
                    Clients.Clients client = clientsGrid.SelectedItems[i] as Clients.Clients;
                    if (client != null)
                    {
                        db.Clients.Remove(client);
                    }
                }
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }

        // редактирование записи
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (clientsGrid.SelectedItem != null)
            {
                Clients.Clients client = clientsGrid.SelectedItem as Clients.Clients;
                Clients.CClient.Id = client.Id;
                Clients.CClient.FirstName = client.FirstName;
                Clients.CClient.LastName = client.LastName;
                Clients.CClient.DateBirth = client.DateBirth;

                // форма редактирования
                EditForm newF = new EditForm();
                newF.ShowDialog();

                // сохранение изменений
                client.FirstName = Clients.CClient.FirstName;
                client.LastName = Clients.CClient.LastName;
                client.DateBirth = Clients.CClient.DateBirth;
                clientsGrid.Items.Refresh();

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // вставка записи
        private void insertButton_Click(object sender, RoutedEventArgs e)
        {
            Clients.CClient.DateBirth = DateTime.Now;
            Clients.CClient.FirstName = "";
            Clients.CClient.LastName = "";

            // форма вставки
            EditForm newForm = new EditForm();
            newForm.ShowDialog();

            if (Clients.CClient.FirstName != "")
            {
                // добавляем новую запись
                Clients.Clients newClient = new Clients.Clients();
                newClient.FirstName = Clients.CClient.FirstName;
                newClient.LastName = Clients.CClient.LastName;
                newClient.DateBirth = Clients.CClient.DateBirth;
                db.Clients.Add(newClient);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
