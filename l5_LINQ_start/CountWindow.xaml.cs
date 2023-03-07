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

namespace l5_LINQ_start
{
    /// <summary>
    /// Interaction logic for CountWindow.xaml
    /// </summary>
    public partial class CountWindow : Window
    {
        public CountWindow()
        {
            InitializeComponent();
        }

        private void lessButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)this.Owner;
            int args = Func.GetIntData(getInt.Text);
            main.addByLinq(args, "less");
            this.Close();
        }

        private void moreButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)this.Owner;
            int args = Func.GetIntData(getInt.Text);
            main.addByLinq(args, "more");
            this.Close();
        }
    }
}
