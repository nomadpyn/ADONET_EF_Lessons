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

namespace l6_Library_EF
{
    /// <summary>
    /// Interaction logic for addPublisherWindow.xaml
    /// </summary>
    public partial class addPublisherWindow : Window
    {
        public addPublisherWindow()
        {
            InitializeComponent();
        }

        private void addPubl_Click(object sender, RoutedEventArgs e)
        {
            Publisher newPubl = new Publisher();
            newPubl.PublisherName = publName.Text;
            newPubl.Address = publAdress.Text;

            ViewModel.addPublisher(newPubl);
            this.Close();
        }
    }
}
