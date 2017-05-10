using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
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

namespace lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {        
        MyDbContext ctx = new MyDbContext();
        List<Car>  carslist;
        public bool dec = false;
        public MainWindow()
        {
            InitializeComponent();            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            carslist = ctx.Cars.Include("Engine").ToList();
            dataGrid.ItemsSource = carslist;
            dataGrid.Columns[2].CanUserSort = true;
            //DataGridTextColumn engineType = new DataGridTextColumn();
            //engineType.Header = "Engine Type";
            //engineType.Binding = new Binding("EngineType");            
           // dataGrid.Columns.Add(engineType);
            fillDataGrid();
            dataGrid.ItemsSource = carslist;
            dataGrid.Items.Refresh(); 
            dataGrid.Sorting += new DataGridSortingEventHandler(SortHandler);
        }
        private void fillDataGrid()
        {
            foreach(Car c in carslist)
            {
                c.engineTypeDo();
            }
        }

        private void SortHandler(object sender, DataGridSortingEventArgs e)
        {
            
            if (!dec)
            {
                carslist.Sort(new ExtendedComparer(dec));
                dec = true;
                e.Column.SortDirection = ListSortDirection.Ascending;
            }
            else
            {
                carslist.Sort(new ExtendedComparer(dec));
                dec = false;
                e.Column.SortDirection = ListSortDirection.Descending;
            }
            dataGrid.Items.Refresh();
        }
        private void button_Click(object sender, RoutedEventArgs e) //FIND BUTTON
        {
            
            string searchBoxText = searchBox.Text;
            if ((searchBoxText).Length > 0)
            {
                if (comboBox.Text == "Year")
                {
                    int rok = 0;
                    bool sukces = int.TryParse(searchBoxText, out rok);
                    if (sukces) {
                        List<Car> searchList = carslist.FindAll(item => item.Year == rok);
                        dataGrid.ItemsSource = searchList;
                    }
                    else
                        MessageBox.Show("Podaj prawidłowy rok", "Warning");
                }else
                {
                    List<Car> searchList = carslist.FindAll(item => item.Model.Contains(searchBoxText));
                    dataGrid.ItemsSource = searchList;
                }
            }
            else
                dataGrid.ItemsSource = carslist;
        }

        private void addCarButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            window1.Owner = this;
        }
        public void addNewCar(Car car)
        {
            car.engineTypeDo();
            carslist.Add(car);
            ctx.Cars.Add(car);
            ctx.SaveChanges();
            dataGrid.Items.Refresh();
        }
        public void saveCar(Car car)
        {
            var obj = carslist.FirstOrDefault(x=> x.Id == car.Id);
            if (obj != null)
            {
                dataGrid.CommitEdit();
                obj = car;
                obj.engineTypeDo();
                Car carToUpdate = ctx.Cars.Where(x => x.Id == car.Id).FirstOrDefault();

                if (carToUpdate != null)
                {
                    ctx.Entry(carToUpdate).CurrentValues.SetValues(car);
                    ctx.SaveChanges();
                }
                //dataGrid.ItemsSource = carslist;
                dataGrid.CancelEdit();
                dataGrid.Items.Refresh();

            }
            

        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Car carTemp = dataGrid.CurrentItem as Car;
            if (carTemp != null)
            {
                int carId = carTemp.Id;
                var car = ctx.Cars.Where(x => x.Id == carId).FirstOrDefault();
                string year = car.Year.ToString();
                string displ = car.Engine.Displacement.ToString();
                string horse = car.Engine.HorsePower.ToString();
                Window1 window1 = new Window1(car.Model, year, car.Engine.Model, displ, horse, car.Id);
                window1.Show();
                window1.Owner = this;
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        { 
            Car carTemp = dataGrid.SelectedItem as Car;
            ctx.Cars.Remove(carTemp);
            ctx.SaveChanges();
            carslist.Remove(carTemp);
            //dataGrid.ItemsSource = carslist;
            dataGrid.Items.Refresh();
        }
        
    }
}
