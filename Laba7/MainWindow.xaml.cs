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

namespace Laba7
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ObservableCollection<Car> carList;

        public MainWindow()
        {
            InitializeComponent();
            carList = new ObservableCollection<Car>();
            lbCars.DataContext = carList;
        }

        private void BtnShowList_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void BtnAddCar_Click(object sender, RoutedEventArgs e)
        {
            Car newCar = new Car()
            {
                brand = "Mazda",
                model = "sdv",
                year = 2001,
                Color = "green"
            };
            newCar.InsertCar();
            LoadData();
        }

        private void BtnEditCar_Click(object sender, RoutedEventArgs e)
        {
            if (lbCars.SelectedItem is Car)
            {
                var selectedCar = (Car)lbCars.SelectedItem;
                selectedCar.Color = "black";
                selectedCar.UpdateCar();
                LoadData();
            }
        }

        private void BtnDeleteCar_Click(object sender, RoutedEventArgs e)
        {
            if (lbCars.SelectedItem is Car)
            {
                var selectedCar = (Car)lbCars.SelectedItem;
                Car.DeleteCarById(selectedCar.carId);
                LoadData();
            }
        }

        private void LoadData()
        {
            carList.Clear();
            foreach (Car car in Car.GetAllCars())
            {
                carList.Add(car);
            }
        }
    }
}
