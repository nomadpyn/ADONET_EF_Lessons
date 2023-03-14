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
    /// Interaction logic for deletePublisherWindow.xaml
    /// </summary>
    public partial class deletePublisherWindow : Window
    {
        Publisher deleted = new Publisher();
        public deletePublisherWindow()
        {
            InitializeComponent();
            using (LibraryEntities db = new LibraryEntities())
            {
// загружаем список издателей из БД в список
                var data = db.Publisher.ToList();
                publisherBox.ItemsSource = data;
            }
        }

// метод обработки состояния списка издателей
        private void publisherBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
// берем значение id издателя
            deleted.Id = (int)publisherBox.SelectedValue;
        }

// метод обработки нажатия кнопки удалить
        private void deletePublisher_Click(object sender, RoutedEventArgs e)
        {
// запускаем метод удаления издателя и закрываем окно
            ViewModel.delPublisher(deleted);
            this.Close();
        }
    }
}
