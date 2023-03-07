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

namespace l5_LINQ_start
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }               

        private void selectByPriceButton_Click(object sender, RoutedEventArgs e)
        {
            CountWindow nw = new CountWindow();
            nw.Show();
        }
        private void selectByCountButton_Click(object sender, RoutedEventArgs e)
        {
            PriceWindow nw = new PriceWindow();
            nw.Show();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow nw = new AddWindow();
            nw.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = Func.getStartUpData();
        }
    }
}
