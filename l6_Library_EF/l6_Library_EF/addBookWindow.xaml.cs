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
    /// Interaction logic for addBookWindow.xaml
    /// </summary>
    public partial class addBookWindow : Window
    {
        Book added = new Book();
        public addBookWindow()
        {
            InitializeComponent();

            using (LibraryEntities db =  new LibraryEntities())
            {
                var authors = db.Author.ToList();
                authorsBox.ItemsSource = authors;
                var publishers = db.Publisher.ToList();
                publBox.ItemsSource = publishers;
            }
        }

        private void publBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            added.IdPublisher = (int)publBox.SelectedValue;
        }

        private void authorsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            added.IdAuthor = (int)authorsBox.SelectedValue;
        }

        private void addBook_Click(object sender, RoutedEventArgs e)
        {
            added.Title = bookName.Text;
            added.Pages = ViewModel.GetIntData(bookPages.Text);
            added.Price = ViewModel.GetIntData(bookPrice.Text);
            ViewModel.addBook(added);
            this.Close();
        }
    }
}
