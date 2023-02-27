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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using Microsoft.Data.SqlClient;

namespace l4_Async_Warehouse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string connString= @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Warehouse; Integrated Security = true";
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void startButton_Click(object sender, RoutedEventArgs e)
        {

            var task1= getDataFromDB(connString, "select * from Providers");
            var task2 = getDataFromDB(connString, "select * from ProductType");

            gridProviders.ItemsSource =(await task1).DefaultView;
            gridProductTypes.ItemsSource = (await task2).DefaultView;          
        }
        private static async Task<DataTable> getDataFromDB(string connString, string sqlQuery)
        {
            SqlConnection connection = new SqlConnection(connString);

            await connection.OpenAsync();

            //MessageBox.Show($"{sqlQuery} подключение создано");

            SqlCommand cmd = new SqlCommand(sqlQuery, connection);

            DataTable table = new DataTable();

            using (SqlDataReader reader = await cmd.ExecuteReaderAsync()) 
            {
                int line = 0;
                do
                {
                    while (await reader.ReadAsync())
                    {
                        if (line == 0)
                        {
                            for (int i = 0; i <
                            reader.FieldCount; i++)
                            {
                                table.Columns.Add(reader.
                                GetName(i));
                            }
                            line++;
                        }
                        DataRow row = table.NewRow();
                        for (int i = 0; i < reader.
                        FieldCount; i++)
                        {
                            row[i] = await reader.
                            GetFieldValueAsync<Object>(i);
                        }
                        table.Rows.Add(row);
                    }
                } while (reader.NextResult());
            }

            //MessageBox.Show($"{sqlQuery} подключение закрыто");

            return table;
        }        
    }    
}
