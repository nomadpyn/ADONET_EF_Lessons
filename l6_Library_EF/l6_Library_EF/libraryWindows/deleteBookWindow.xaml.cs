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
// загружаем из БД книги, авторов и издателей
                var books = db.Book.ToList();
                var authors = db.Author.ToList();
                var publishers = db.Publisher.ToList();
                bookBox.ItemsSource = books;
            }
        }

// обработка изменения состояния списка книг
        private void bookBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
// берем значение id книги
            deleted.Id = (int)bookBox.SelectedValue;
        }

// обработка нажатия кнопки удалить
        private void deleteBook_Click(object sender, RoutedEventArgs e)
        {
// запускаем метод удаления книги и закрываем окно
            ViewModel.delBook(deleted);
            this.Close();
        }
    }
}
