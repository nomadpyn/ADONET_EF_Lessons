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
            authorsGrid.ItemsSource = ViewModel.getAllAuthors().DefaultView;
        }
        private void loadPubButton_Click(object sender, RoutedEventArgs e)
        {
            publGrid.ItemsSource = null;
            publGrid.ItemsSource = ViewModel.getAllPublishers().DefaultView;
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
            nw.Owner = this;
            nw.ShowDialog();
        }

        private void deleteAutButton_Click(object sender, RoutedEventArgs e)
        {
            deleteAuthorWindow nw = new deleteAuthorWindow();
            nw.Owner = this;
            nw.ShowDialog();
        }

        private void addPubButton_Click(object sender, RoutedEventArgs e)
        {
            addPublisherWindow nw = new addPublisherWindow();
            nw.Owner = this;
            nw.ShowDialog();
        }

        private void updatePubButton_Click(object sender, RoutedEventArgs e)
        {
            updatePublisherWindow nw = new updatePublisherWindow();
            nw.Owner = this;
            nw.ShowDialog();
        }

        private void deletePubButton_Click(object sender, RoutedEventArgs e)
        {
            deletePublisherWindow nw = new deletePublisherWindow();
            nw.Owner = this;
            nw.ShowDialog();
        }

        private void addBooksButton_Click(object sender, RoutedEventArgs e)
        {
            addBookWindow nw = new addBookWindow();
            nw.Owner = this;
            nw.ShowDialog();
        }

        private void updateBooksButton_Click(object sender, RoutedEventArgs e)
        {
            updateBookWindow nw = new updateBookWindow();
            nw.Owner = this;
            nw.ShowDialog();
        }
    }
}
