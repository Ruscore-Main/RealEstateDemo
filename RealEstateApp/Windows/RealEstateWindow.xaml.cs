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
    /// Логика взаимодействия для RealEstateWindow.xaml
    /// </summary>
    public partial class RealEstateWindow : Window
    {
        Window backWindow;
        RealEstateDBEntities _db = new RealEstateDBEntities();

        public RealEstateWindow(Window window)
        {
            this.backWindow = window;
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            backWindow.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RealEstateTable.ItemsSource = _db.RealEstate.ToList();
            List<TypeRealEstate> typesRE = new List<TypeRealEstate>()
            {
                new TypeRealEstate()
                {
                    Name = "Все"
                }
            };
            typesRE.AddRange(_db.TypeRealEstate.ToList());
            CmbTypeRealEstate.ItemsSource = typesRE;
        }

        public void UpdateTable()
        {
            TypeRealEstate type = CmbTypeRealEstate.SelectedItem as TypeRealEstate;
            List<RealEstate> realEstates = _db.RealEstate.ToList();
            if (type.Name != "Все")
            {
                realEstates = realEstates.Where(el => el.TypeRealEstate == type).ToList();
            }

            RealEstateTable.ItemsSource = realEstates;
        }

        // Добавление недвижимости
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        // Изменение недвижинмости
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        // Удаление недвижимости
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        // Изменение выбранного типа
        private void CmbTypeRealEstate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTable();
        }
    }
}
