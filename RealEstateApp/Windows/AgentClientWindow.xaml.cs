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
    /// Логика взаимодействия для AgentClientWindow.xaml
    /// </summary>
    public partial class AgentClientWindow : Window
    {
        RealEstateDBEntities _db = new RealEstateDBEntities();
        public Window backWindow;
        public string currentTable = "Client";

        public AgentClientWindow(Window window)
        {
            this.backWindow = window;
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            backWindow.Show();
        }

        // Редактирование клиента
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Client client = ClientTable.SelectedItem as Client;
            EditClient editClientWindow = new EditClient(this, client, _db);
            editClientWindow.Show();
            this.Hide();
        }

        // Удаление клиента
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Client client = ClientTable.SelectedItem as Client;
            if (client.Supply.Count > 0)
            {
                MessageBox.Show("Нельзя удалить клиента связаного с потребностью", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (client.Demand.Count > 0)
            {
                MessageBox.Show("Нельзя удалить клиента связаного с предложением", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _db.Client.Remove(client);
                _db.SaveChanges();
                UpdateTable();
                MessageBox.Show("Клиент успешно удален!");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTable();
        }

        public void UpdateTable()
        {
            ClientTable.ItemsSource = _db.Client.ToList();
            AgentTable.ItemsSource = _db.Agent.ToList();
        }
        

        // Редактирование риэлтора
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        // Удаление риэлтора
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Agent agent = AgentTable.SelectedItem as Agent;
            if (agent.Supply.Count > 0)
            {
                MessageBox.Show("Нельзя удалить риэлтора связаного с потребностью", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (agent.Demand.Count > 0)
            {
                MessageBox.Show("Нельзя удалить риэлтора связаного с предложением", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _db.Agent.Remove(agent);
                _db.SaveChanges();
                UpdateTable();
                MessageBox.Show("Риэлтор успешно удален!");
            }
        }

        /// <summary>
        /// Изменение выбранной вкладки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentTable = TabCtrl.SelectedIndex == 0 ? "Client" : "Agent";
            TxtSearch.Text = "";
        }

        /// <summary>
        /// Поиск в реальном времени
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = ((TextBox)sender).Text;
            if (searchText == "")
            {
                UpdateTable();
                return;
            }
            if (currentTable == "Client")
            {
                
                List<Client> clients = new List<Client>();
                foreach (Client client in _db.Client)
                {
                    int count = 0;
                    string FIO = $"{client.Surname} {client.Name} {client.Patronymic}";
                    // Если длина текстового поиска больше
                    if (searchText.Length <= FIO.Length)
                    {
                        for (int i = 0; i < searchText.Length; i++)
                        {
                            if (searchText[i] != FIO[i])
                            {
                                count++;
                            }
                        }

                        count += FIO.Length - searchText.Length;
                    }
                    // Если длина ФИО больше
                    else
                    {
                        for (int i = 0; i < FIO.Length; i++)
                        {
                            if (FIO[i] != searchText[i])
                            {
                                count++;
                            }
                        }
                        count += searchText.Length - FIO.Length;
                    }

                    if (count <= 3) clients.Add(client);
                }

                ClientTable.ItemsSource = clients.ToList();
            }

            else
            {
                List<Agent> agents = new List<Agent>();
                foreach (Agent agent in _db.Agent)
                {
                    int count = 0;
                    string FIO = $"{agent.Surname} {agent.Name} {agent.Patronymic}";
                    // Если длина текстового поиска больше
                    if (searchText.Length <= FIO.Length)
                    {
                        for (int i = 0; i < searchText.Length; i++)
                        {
                            if (searchText[i] != FIO[i])
                            {
                                count++;
                            }
                        }

                        count += FIO.Length - searchText.Length;
                    }
                    // Если длина ФИО больше
                    else
                    {
                        for (int i = 0; i < FIO.Length; i++)
                        {
                            if (FIO[i] != searchText[i])
                            {
                                count++;
                            }
                        }
                        count += searchText.Length - FIO.Length;
                    }

                    if (count <= 3) agents.Add(agent);
                }

                AgentTable.ItemsSource = agents.ToList();
            }
        }

        // Добавление клиента
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AddClient addClientWindow = new AddClient(this);
            this.Hide();
            addClientWindow.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }
    }
}
