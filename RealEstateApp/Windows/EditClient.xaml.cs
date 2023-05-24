using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using RealEstateApp.Models;

namespace RealEstateApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditClient.xaml
    /// </summary>
    public partial class EditClient : Window
    {
        AgentClientWindow backWindow;
        Client curClient;
        RealEstateDBEntities _db;

        public EditClient(AgentClientWindow window, Client client, RealEstateDBEntities db)
        {
            this.backWindow = window;
            this.curClient = client;
            this._db = db;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TxtPhoneNumber.Text == "" && TxtEmail.Text == "")
            {
                MessageBox.Show("Заполните одно из полей: Email или Номер телефона", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Client client = _db.Client.FirstOrDefault(el => el.Id == curClient.Id);

                if (client != null)
                {
                    client.Name = TxtName.Text;
                    client.Surname = TxtSurname.Text;
                    client.Patronymic = TxtPatronymic.Text;
                    client.Email = TxtEmail.Text;
                    client.PhoneNumber = TxtPhoneNumber.Text;
                    _db.SaveChanges();
                    MessageBox.Show("Клиент успешно обновлен!");
                    backWindow.UpdateTable();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Произошла непредвиденная ошибка", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TxtName.Text = curClient.Name;
            TxtSurname.Text = curClient.Surname;
            TxtPatronymic.Text = curClient.Patronymic;
            TxtEmail.Text = curClient.Email;
            TxtPhoneNumber.Text = curClient.PhoneNumber;

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            backWindow.Show();
        }
    }
}
