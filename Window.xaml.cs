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

namespace lab4
{
    /// <summary>
    /// Interaction logic for Window.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private bool add = true;
        private int id;
        public Window1()
        {
            InitializeComponent();
            add = true;
        }

        public Window1(string model, string year, string engine, string displ, string horse, int ID)
        {
            InitializeComponent();
            add = false;
            modelBox.Text = model;
            yearBox.Text = year;
            engineBox.Text = engine;
            displacementBox.Text = displ;
            horsePowerBox.Text = horse;
            id = ID;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Car car = new Car();
            Engine engine = new Engine();
            int rok = 0;
            bool sukces = int.TryParse(yearBox.Text, out rok);
            if (sukces)
            {
                car.Year = rok;
                car.Model = modelBox.Text;
                double displ, power;
                bool sukcesDispl = double.TryParse(displacementBox.Text, out displ);
                bool sukcesPower = double.TryParse(horsePowerBox.Text, out power);
                if (sukcesDispl)
                {
                    if (sukcesPower)
                    {
                        engine.Displacement = displ;
                        engine.HorsePower = power;
                        engine.Model = engineBox.Text;
                        car.Engine = engine;
                        if (add)
                            ((MainWindow)this.Owner).addNewCar(car);
                        else
                        {
                            car.Id = id;
                            ((MainWindow)this.Owner).saveCar(car);
                        }
                        Close();
                    }
                    else
                        MessageBox.Show("Podaj prawidłowy Horse power", "Warning");
                }
                else
                    MessageBox.Show("Podaj prawidłowy displacement", "Warning");
            }
            else
                MessageBox.Show("Podaj prawidłowy rok", "Warning");

        }
    }
}
