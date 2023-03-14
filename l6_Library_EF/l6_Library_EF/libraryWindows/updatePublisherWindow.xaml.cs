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
    /// Interaction logic for updatePublisherWindow.xaml
    /// </summary>
    public partial class updatePublisherWindow : Window
    {
        Publisher updated = new Publisher();
        public updatePublisherWindow()
        {
            InitializeComponent();
            using (LibraryEntities db = new LibraryEntities())
            {
// загружаем список издателей из БД
                var data = db.Publisher.ToList();

                publishersBox.ItemsSource = data;
            }
        }

// обработка изменения состояния списка издателей
        private void publishersBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
// берем id издателя, которого изменяем
            updated.Id = (int)publishersBox.SelectedValue;
        }

        private void updatePublisher_Click(object sender, RoutedEventArgs e)
        {
// заполяем поля сущности из формы
            updated.PublisherName = publisherName.Text;
            updated.Address = publisherAdress.Text;
// запускаем метод изменения издателя и закрываем форму
            ViewModel.updPublisher(updated);
            this.Close();
        }
    }
}
