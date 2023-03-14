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
    /// Interaction logic for addAuthorWindow.xaml
    /// </summary>
    public partial class addAuthorWindow : Window
    {
        public addAuthorWindow()
        {
            InitializeComponent();
        }

        private void addAuthor_Click(object sender, RoutedEventArgs e)
        {
            Author newAuthor = new Author();
            newAuthor.FirstName = authorName.Text;
            newAuthor.LastName = authorFname.Text;

            ViewModel.addAuthor(newAuthor);
            this.Close();
        }
    }
}
