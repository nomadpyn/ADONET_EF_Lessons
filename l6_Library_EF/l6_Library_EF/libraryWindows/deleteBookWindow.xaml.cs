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
    /// Interaction logic for deleteBookWindow.xaml
    /// </summary>
    public partial class deleteBookWindow : Window
    {
        Book deleted = new Book();
        public deleteBookWindow()
        {
            InitializeComponent();

            using (LibraryEntities db = new LibraryEntities())
            {
                var books = db.Book.ToList();
                var authors = db.Author.ToList();
                var publishers = db.Publisher.ToList();
                bookBox.ItemsSource = books;
            }
        }

        private void bookBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            deleted.Id = (int)bookBox.SelectedValue;
        }

        private void deleteBook_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.delBook(deleted);
            this.Close();
        }
    }
}
