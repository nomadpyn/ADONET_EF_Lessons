﻿using l6_Library_EF.Model;
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
    /// Interaction logic for updateAuthorWindow.xaml
    /// </summary>
    public partial class updateAuthorWindow : Window
    {
        Author updated = new Author();
        public updateAuthorWindow()
        {
            InitializeComponent();
            using (LibraryEntities db = new LibraryEntities())
            {
// берем список авторов из БД в список в форму
                var data = db.Author.ToList();
                authorsBox.ItemsSource = data;
            }
        }

// метод обработки изменения состояние списка 
        private void authorsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
// берем id автора
                updated.Id = (int)authorsBox.SelectedValue;
        }

// метод обработки нажатия кнопки изменить
        private void updateAuthor_Click(object sender, RoutedEventArgs e)
        {
// заполняем поля сущности из формы
            updated.FirstName = authorName.Text;
            updated.LastName = authorFname.Text;
// запускаем метод изменения автора и закрываем окно
            ViewModel.updAuthor(updated);
            this.Close();
        }
    }
}
