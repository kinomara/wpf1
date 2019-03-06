using System;
using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for EditForm.xaml
    /// </summary>
    public partial class EditForm : Window
    {
        bool IsCancel;
        public EditForm()
        {
            IsCancel = true;
            
            InitializeComponent();
            if (Clients.CClient.Id.ToString() != "")
            {
                //txtId.Text = Clients.CClient.Id.ToString();
                txtFirstName.Text = Clients.CClient.FirstName;
                txtLastName.Text = Clients.CClient.LastName;
                txtDate.Text = Clients.CClient.DateBirth.ToString();
                this.Title = "Редактирование записи";
            }
            else
            {
                this.Title = "Добавление новой записи";
            }
        }

        // передача фамилии, имени, др в основную форму
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (Clients.CClient.FirstName != txtFirstName.Text)
                Clients.CClient.FirstName = txtFirstName.Text;
            if (Clients.CClient.LastName != txtLastName.Text)
                Clients.CClient.LastName = txtLastName.Text;
            if (Clients.CClient.DateBirth.ToString() != txtDate.Text)
                Clients.CClient.DateBirth = DateTime.Parse(txtDate.Text);
            IsCancel = false;
            this.Close();          
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // проверка на пустые поля
            if (!IsCancel && (txtFirstName.Text == "" && txtLastName.Text == ""))
            {
                MessageBox.Show("Не заполненные поля");
                e.Cancel = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            IsCancel = true;
            this.Close();
        }
    }
}
