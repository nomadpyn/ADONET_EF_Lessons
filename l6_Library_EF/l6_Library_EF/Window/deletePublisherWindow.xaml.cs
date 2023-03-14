using l6_Library_EF.Model;
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
    /// Interaction logic for deletePublisherWindow.xaml
    /// </summary>
    public partial class deletePublisherWindow : Window
    {
        Publisher deleted = new Publisher();
        public deletePublisherWindow()
        {
            InitializeComponent();
            using (LibraryEntities db = new LibraryEntities())
            {
                var data = db.Publisher.ToList();
                publisherBox.ItemsSource = data;
            }
        }

        private void publisherBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            deleted.Id = (int)publisherBox.SelectedValue;
        }

        private void deletePublisher_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.delPublisher(deleted);
            this.Close();
        }
    }
}
