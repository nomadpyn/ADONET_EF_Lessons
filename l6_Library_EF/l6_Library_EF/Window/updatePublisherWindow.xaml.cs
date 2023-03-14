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
    /// Interaction logic for updatePublisherWindow.xaml
    /// </summary>
    public partial class updatePublisherWindow : Window
    {
        Publisher updated = new Publisher();
        public updatePublisherWindow()
        {
            InitializeComponent();
            using (LibraryEntities db = new LibraryEntities())
            {
                var data = db.Publisher.ToList();

                publishersBox.ItemsSource = data;
            }
        }

        private void publishersBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updated.Id = (int)publishersBox.SelectedValue;
        }

        private void updatePublisher_Click(object sender, RoutedEventArgs e)
        {
            updated.PublisherName = publisherName.Text;
            updated.Address = publisherAdress.Text;
            ViewModel.updPublisher(updated);
            this.Close();
        }
    }
}
