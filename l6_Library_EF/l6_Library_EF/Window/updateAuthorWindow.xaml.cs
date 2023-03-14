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
                var data = db.Author.ToList();

                authorsBox.ItemsSource = data;
            }
        }

        private void authorsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                updated.Id = (int)authorsBox.SelectedValue;
        }

        private void updateAuthor_Click(object sender, RoutedEventArgs e)
        {
            updated.FirstName = authorName.Text;
            updated.LastName = authorFname.Text;
            ViewModel.updAuthor(updated);
            this.Close();
        }
    }
}
