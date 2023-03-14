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

// обработка нажатия кнопки Загрузить на вкладке Книги
        private void loadBooksButton_Click(object sender, RoutedEventArgs e)
        {
            booksGrid.ItemsSource = null;
            booksGrid.ItemsSource = ViewModel.getAllBooks().DefaultView;
        }

// обработка нажатия кнопки Загрузить на вкладке Авторы
        private void loadAutButton_Click(object sender, RoutedEventArgs e)
        {
            authorsGrid.ItemsSource = null;
            authorsGrid.ItemsSource = ViewModel.getAllAuthors().DefaultView;
        }

// обработка нажатия кнопки Загрузить на вкладке Издатели
        private void loadPubButton_Click(object sender, RoutedEventArgs e)
        {
            publGrid.ItemsSource = null;
            publGrid.ItemsSource = ViewModel.getAllPublishers().DefaultView;
        }

// обработка нажатия кнопки Добавить на вкладке Авторы
        private void addAutButton_Click(object sender, RoutedEventArgs e)
        {
            addAuthorWindow nw = new addAuthorWindow();
            nw.Owner = this;
            nw.ShowDialog();
        }

// обработка нажатия кнопки Изменить на вкладке Авторы
        private void updateAutButton_Click(object sender, RoutedEventArgs e)
        {
            updateAuthorWindow nw = new updateAuthorWindow();
            nw.Owner = this;
            nw.ShowDialog();
        }

// обработка нажатия кнопки Удалить на вкладке Авторы
        private void deleteAutButton_Click(object sender, RoutedEventArgs e)
        {
            deleteAuthorWindow nw = new deleteAuthorWindow();
            nw.Owner = this;
            nw.ShowDialog();
        }

// обработка нажатия кнопки Добавить на вкладке Издатели
        private void addPubButton_Click(object sender, RoutedEventArgs e)
        {
            addPublisherWindow nw = new addPublisherWindow();
            nw.Owner = this;
            nw.ShowDialog();
        }

// обработка нажатия кнопки Изменить на вкладке Издатели
        private void updatePubButton_Click(object sender, RoutedEventArgs e)
        {
            updatePublisherWindow nw = new updatePublisherWindow();
            nw.Owner = this;
            nw.ShowDialog();
        }

// обработка нажатия кнопки Удалить на вкладке Издатели
        private void deletePubButton_Click(object sender, RoutedEventArgs e)
        {
            deletePublisherWindow nw = new deletePublisherWindow();
            nw.Owner = this;
            nw.ShowDialog();
        }

// обработка нажатия кнопки Добавить на вкладке Книги
        private void addBooksButton_Click(object sender, RoutedEventArgs e)
        {
            addBookWindow nw = new addBookWindow();
            nw.Owner = this;
            nw.ShowDialog();
        }

// обработка нажатия кнопки Изменить на вкладке Книги
        private void updateBooksButton_Click(object sender, RoutedEventArgs e)
        {
            updateBookWindow nw = new updateBookWindow();
            nw.Owner = this;
            nw.ShowDialog();
        }

// обработка нажатия кнопки Удалить на вкладке Книги
        private void deleteBooksButton_Click(object sender, RoutedEventArgs e)
        {
            deleteBookWindow nw = new deleteBookWindow();
            nw.Owner = this;
            nw.ShowDialog();
        }
    }
}
