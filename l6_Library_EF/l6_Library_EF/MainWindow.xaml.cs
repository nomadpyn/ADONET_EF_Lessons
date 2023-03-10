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

namespace l6_Library_EF
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

        private void loadBooksButton_Click(object sender, RoutedEventArgs e)
        {
            booksGrid.ItemsSource = null;
            booksGrid.ItemsSource = ViewModel.getAllBooks().DefaultView;
        }
        private void loadAutButton_Click(object sender, RoutedEventArgs e)
        {
            authorsGrid.ItemsSource = null;
            List<Author> data;
            using (LibraryEntities db = new LibraryEntities())
            {
                data = db.Author.ToList();
            }
            authorsGrid.ItemsSource = data;
        }
        private void loadPubButton_Click(object sender, RoutedEventArgs e)
        {
            publGrid.ItemsSource = null;
            List<Publisher> data;
            using (LibraryEntities db = new LibraryEntities())
            {
                data = db.Publisher.ToList();
            }
            publGrid.ItemsSource = data;
        }

        private void addAutButton_Click(object sender, RoutedEventArgs e)
        {
            addAuthorWindow nw = new addAuthorWindow();
            nw.Owner = this;
            nw.ShowDialog();
        }

        private void updateAutButton_Click(object sender, RoutedEventArgs e)
        {
            updateAuthorWindow nw = new updateAuthorWindow();
            nw.Owner = this.Owner;
            nw.ShowDialog();
        }
    }
}
