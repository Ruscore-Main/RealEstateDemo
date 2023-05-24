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
    /// Логика взаимодействия для AddRealEstateWindow.xaml
    /// </summary>
    public partial class AddRealEstateWindow : Window
    {
        RealEstateWindow backWindow;
        string curType = "Квартира";
        RealEstateDBEntities _db;
        public AddRealEstateWindow(RealEstateWindow window, RealEstateDBEntities db)
        {
            this.backWindow = window;
            this._db = db;
            InitializeComponent();
        }

        private void CmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                curType = ((ComboBoxItem)CmbType.SelectedItem).Content.ToString();
                if (curType == "Квартира")
                {
                    LblArea.Visibility = Visibility.Visible;
                    LblRooms.Visibility = Visibility.Visible;
                    LblFloor.Content = "Этаж";
                    LblFloor.Visibility = Visibility.Visible;

                    TxtArea.Visibility = Visibility.Visible;
                    TxtRooms.Visibility = Visibility.Visible;
                    TxtFloor.Visibility = Visibility.Visible;
                }
                else if (curType == "Дом")
                {
                    LblArea.Visibility = Visibility.Visible;
                    LblRooms.Visibility = Visibility.Visible;
                    LblFloor.Content = "Этажность";
                    LblFloor.Visibility = Visibility.Visible;

                    TxtArea.Visibility = Visibility.Visible;
                    TxtRooms.Visibility = Visibility.Visible;
                    TxtFloor.Visibility = Visibility.Visible;
                }
                else
                {
                    LblArea.Visibility = Visibility.Visible;
                    LblRooms.Visibility = Visibility.Hidden;
                    LblFloor.Visibility = Visibility.Hidden;

                    TxtArea.Visibility = Visibility.Visible;
                    TxtRooms.Visibility = Visibility.Hidden;
                    TxtFloor.Visibility = Visibility.Hidden;
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            backWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            float? lt = null, lg = null;
            float _lt, _lg;
            if (TxtLatitude.Text != "" && float.TryParse(TxtLatitude.Text, out _lt))
            {
                if (_lt < -90 && _lt > 90)
                {
                    MessageBox.Show("Широта должна быть от -90 до 90");
                    return;
                }
                else
                {
                    lt = _lt;
                }
                
            }

            if (TxtLongitude.Text != "" && float.TryParse(TxtLongitude.Text, out _lg))
            {
                if (_lg < -180 && _lg > 180)
                {
                    MessageBox.Show("Долгота должна быть от -180 до 180");
                    return;
                }
                else
                {
                    lg = _lg;
                }
            }

            float? area = null;
            if (float.TryParse(TxtArea.Text, out _)) {
                area = float.Parse(TxtArea.Text);
            }

            int? amountRooms = null;
            if (int.TryParse(TxtRooms.Text, out _))
            {
                amountRooms = int.Parse(TxtRooms.Text);
            }

            int? floor = null;
            if (int.TryParse(TxtFloor.Text, out _))
            {
                floor = int.Parse(TxtFloor.Text);
            }

            RealEstate realEstate = new RealEstate()
            {
                AddressCity = TxtCity.Text,
                AddressStreet = TxtStreet.Text,
                AddressHouse = TxtHouse.Text,
                AddressNumber = TxtAppartment.Text,
                Latitude = lt,
                Longitude = lg,
                Area = area,
            };

            if (curType == "Квартира")
            { 
                TypeRealEstate appartment = _db.TypeRealEstate.FirstOrDefault(el => el.Name == "Квартира");
                realEstate.AmountRooms = amountRooms;
                realEstate.Floor = floor;
                realEstate.TypeRealEstate = appartment;

            }
            else if (curType == "Дом")
            {
                TypeRealEstate house = _db.TypeRealEstate.FirstOrDefault(el => el.Name == "Дом");
                realEstate.AmountRooms = amountRooms;
                realEstate.Floor = floor;
                realEstate.TypeRealEstate = house;
            }
            else
            {
                TypeRealEstate land = _db.TypeRealEstate.FirstOrDefault(el => el.Name == "Земля");
                realEstate.TypeRealEstate = land;
            }

            _db.RealEstate.Add(realEstate);
            _db.SaveChanges();
            backWindow.UpdateTable();
        }
    }
}
