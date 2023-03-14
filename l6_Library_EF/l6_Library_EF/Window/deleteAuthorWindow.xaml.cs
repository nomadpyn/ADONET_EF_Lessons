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
            using (LibraryEntities db = new LibraryEntities())
            {
                var data = db.Author.ToList();
                authorsBox.ItemsSource = data;
            }
        }

        private void authorsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            deleted.Id = (int)authorsBox.SelectedValue;
        }

        private void deleteAuthor_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.delAuthor(deleted);
            this.Close();
        }
    }
}
