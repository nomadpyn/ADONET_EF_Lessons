using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        BindingList<Product> dataList;
        int args;
        string direct;
        public MainWindow()
        {
            InitializeComponent();
        }               

        private void selectByPriceButton_Click(object sender, RoutedEventArgs e)
        {
            PriceWindow nw = new PriceWindow();            
            nw.Owner = this;
            nw.ShowDialog();
            if (direct == "less")
            {
                var data = Func.getLessProductsPrice(ref dataList, this.args);
                this.dataGrid.ItemsSource = null;
                this.dataGrid.ItemsSource = data;
            }
        }
        private void selectByCountButton_Click(object sender, RoutedEventArgs e)
        {
            CountWindow nw = new CountWindow();
            nw.Show();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow nw = new AddWindow();
            nw.Owner = this;
            nw.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            dataList = Func.getStartUpData();
            dataGrid.ItemsSource = dataList;
        }

        public void addDataToList(Product obj)
        {
            this.dataList.Add(obj);
        }

        public void addByLinq(int args, string direct)
        {
            this.args = args;
            this.direct = direct;
        }

        private void clearArgs()
        {
            this.args = 0;
            this.direct = null;
        }
    }
}
