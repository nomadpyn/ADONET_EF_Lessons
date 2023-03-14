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
    /// Interaction logic for addPublisherWindow.xaml
    /// </summary>
    public partial class addPublisherWindow : Window
    {
        public addPublisherWindow()
        {
            InitializeComponent();
        }

// обработка нажатия кнопки добавить издателя
        private void addPubl_Click(object sender, RoutedEventArgs e)
        {
// создаем объект издателя 
            Publisher newPubl = new Publisher();
// добавляем ему данные полей
            newPubl.PublisherName = publName.Text;
            newPubl.Address = publAdress.Text;
// запускаем метод добавления издателя и закрываем окно
            ViewModel.addPublisher(newPubl);
            this.Close();
        }
    }
}
