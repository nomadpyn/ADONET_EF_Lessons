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
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace l6_Library_EF
{
    /// <summary>
    /// Interaction logic for deleteAuthorWindow.xaml
    /// </summary>
    public partial class deleteAuthorWindow : Window
    {
        Author deleted = new Author();
        public deleteAuthorWindow()
        {
            InitializeComponent();
// забираем список авторов из БД для вывода в список в окне
            using (LibraryEntities db = new LibraryEntities())
            {
                var data = db.Author.ToList();
                authorsBox.ItemsSource = data;
            }
        }

// обработка изменения состояния списка авторов
        private void authorsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
// берем значение id автора, которого хотим удалить 
            deleted.Id = (int)authorsBox.SelectedValue;
        }

// обработка нажатия кнопки удалить автора
        private void deleteAuthor_Click(object sender, RoutedEventArgs e)
        {
// запускаем метод удаления автора и закрываем окно
            ViewModel.delAuthor(deleted);
            this.Close();
        }
    }
}
