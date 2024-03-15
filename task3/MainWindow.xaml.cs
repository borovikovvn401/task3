using Microsoft.EntityFrameworkCore;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using task3.Model;

namespace task3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<Model.Type> types = EfModel.init().Types.ToList();
            types.Insert(0, new Model.Type { Name = "Все" });
            cbTypes.ItemsSource = types;

            cbTypes.SelectedIndex = 0;

        }


        public void update()
        {
            IEnumerable<Tour> tours = EfModel.init().Tours.Include(p => p.CountryCodeNavigation).Include(p => p.Types)
                .Where(p => p.Name.Contains(tbSearch.Text) || p.Description.Contains(tbSearch.Text));


            if(cbTypes.SelectedIndex >0)
            {
                tours = tours.Where(p => p.Types.Contains(cbTypes.SelectedItem as Model.Type));
            }

            if (cbOnlyActual.IsChecked == true)
            {
                tours = tours.Where(p => p.IsActual);
            }

            lvTours.ItemsSource = tours.ToList();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            update();
        }

        private void cbTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            update();
        }

        private void cbOnlyActual_Checked(object sender, RoutedEventArgs e)
        {

            update();
        }
    }
}
