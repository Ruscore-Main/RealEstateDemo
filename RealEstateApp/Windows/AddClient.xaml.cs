using RealEstateApp.Models;
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

namespace RealEstateApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        AgentClientWindow backWindow;
        RealEstateDBEntities _db = new RealEstateDBEntities();
        public AddClient(AgentClientWindow window)
        {
            this.backWindow = window;
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
                Client lastClient = _db.Client.OrderBy(el => el.Id).ToList().LastOrDefault() as Client;
                int id;
                id = lastClient != null ? lastClient.Id + 1 : 1;

                _db.Client.Add(new Client()
                {
                    Id = id,
                    Name = TxtName.Text,
                    Surname = TxtSurname.Text,
                    Patronymic = TxtPatronymic.Text,
                    Email = TxtEmail.Text,
                    PhoneNumber = TxtPhoneNumber.Text
                });

                _db.SaveChanges();
                MessageBox.Show("Клиент успешно добавлен!");
                backWindow.UpdateTable();
                this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            backWindow.Show();
        }
    }
}
