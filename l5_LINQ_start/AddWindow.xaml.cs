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

namespace l5_LINQ_start
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {        
        public AddWindow()
        {
            InitializeComponent();            
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product addProduct = new Product();
                addProduct.Name = ProdNameBox.Text;
                addProduct.Price = Func.GetIntData(ProdPriceBox.Text);
                addProduct.Count = Func.GetIntData(ProdCountBox.Text);
                MainWindow main = (MainWindow)this.Owner;
                Func.addDataToDB(addProduct);
                main.addDataToList(addProduct);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
