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
// строка подключения к БД
        private const string connString= @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Warehouse; Integrated Security = true";
        public MainWindow()
        {
            InitializeComponent();
        }
// асинхронный метод нажатия кнопки
        private async void startButton_Click(object sender, RoutedEventArgs e)
        {
// создаем переменные, в которых происходит выполнение в двух потоках
            var task1= getDataFromDB(connString, "select * from Providers");
            var task2 = getDataFromDB(connString, "select * from ProductType");
// выполняем методы и перекладываем данные в таблицы на форме
            gridProviders.ItemsSource =(await task1).DefaultView;
            gridProductTypes.ItemsSource = (await task2).DefaultView;          
        }
// создаем асинхронный метод доступа к БД к разным таблицам одновременно
        private static async Task<DataTable> getDataFromDB(string connString, string sqlQuery)
        {
            DataTable table = new DataTable();
            try
            {
                SqlConnection connection = new SqlConnection(connString);
 // создаем асинхронное подключение и выводим об этом сообщение в messageBox
                await connection.OpenAsync();
                MessageBox.Show($"{sqlQuery} подключение создано");
// создаем команду и выполняем асинхронное чтение с БД
                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
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
                MessageBox.Show($"{sqlQuery} подключение закрыто");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return table;
        }        
    }    
}
