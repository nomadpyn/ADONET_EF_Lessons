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
// берем авторов и издателей из БД для вывода их в ComboBox для выбора
            using (LibraryEntities db =  new LibraryEntities())
            {
                var authors = db.Author.ToList();
                authorsBox.ItemsSource = authors;
                var publishers = db.Publisher.ToList();
                publBox.ItemsSource = publishers;
            }
        }

// обработка изменения списка с издателями
        private void publBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
// берем значение id издателя для поля idPublisher
            added.IdPublisher = (int)publBox.SelectedValue;
        }

// обработка изменения списка с авторами
        private void authorsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
// берем значение id автора для поля idAuthor
            added.IdAuthor = (int)authorsBox.SelectedValue;
        }

// обработка нажатия кнопки добавить книгу
        private void addBook_Click(object sender, RoutedEventArgs e)
        {
// в объект класса книга добавляем название из текстого поля с формы
            added.Title = bookName.Text;
// с помощью методов правильно задаем количество страниц и цену
            added.Pages = ViewModel.GetIntData(bookPages.Text);
            added.Price = ViewModel.GetIntData(bookPrice.Text);
// запускаем метод добавления книги и закрываем окно
            ViewModel.addBook(added);
            this.Close();
        }
    }
}
