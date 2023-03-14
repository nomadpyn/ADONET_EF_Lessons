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
    /// Interaction logic for updateBookWindow.xaml
    /// </summary>
    public partial class updateBookWindow : Window
    {
        Book updated = new Book();
        public updateBookWindow()
        {
            InitializeComponent();

            using(LibraryEntities db = new LibraryEntities())
            {
                var books = db.Book.ToList();
                bookBox.ItemsSource = books;
                var authors = db.Author.ToList();
                authorsBox.ItemsSource = authors;
                var publishers = db.Publisher.ToList();
                publBox.ItemsSource = publishers;
            }
        }

        private void bookBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updated.Id = (int)bookBox.SelectedValue;
        }

        private void authorsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updated.IdAuthor = (int)authorsBox.SelectedValue;
        }

        private void publBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updated.IdPublisher = (int)publBox.SelectedValue;
        }

        private void updBook_Click(object sender, RoutedEventArgs e)
        {
            updated.Title = bookName.Text;
            updated.Pages = ViewModel.GetIntData(bookPages.Text);
            updated.Price = ViewModel.GetIntData(bookPrice.Text);
            ViewModel.updBook(updated);
            this.Close();
        }
    }
}
