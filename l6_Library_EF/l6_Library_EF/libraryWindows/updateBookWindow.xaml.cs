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
// берем данные из БД для вывода в списки на форме
                var books = db.Book.ToList();
                bookBox.ItemsSource = books;
                var authors = db.Author.ToList();
                authorsBox.ItemsSource = authors;
                var publishers = db.Publisher.ToList();
                publBox.ItemsSource = publishers;
            }
        }

// метод обработки изменения состояния списка книг
        private void bookBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
// берем id книги, которую меняем
            updated.Id = (int)bookBox.SelectedValue;
        }

// метод обработки изменения состояния списка книг
        private void authorsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
// берем id для поля idAuthor
            updated.IdAuthor = (int)authorsBox.SelectedValue;
        }

// метод обработки изменения состояния списка книг
        private void publBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
// берем id для поля idPublisher
            updated.IdPublisher = (int)publBox.SelectedValue;
        }

// метод обработки нажатия кнопки изменить
        private void updBook_Click(object sender, RoutedEventArgs e)
        {
// заполняем поле сущности из формы 
            updated.Title = bookName.Text;
// заполняем поля с помощью метода получения корректного числа
            updated.Pages = ViewModel.GetIntData(bookPages.Text);
            updated.Price = ViewModel.GetIntData(bookPrice.Text);
// запускаем метод изменения книги и закрываем окно
            ViewModel.updBook(updated);
            this.Close();
        }
    }
}
