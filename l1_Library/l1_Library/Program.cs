using Microsoft.Data.SqlClient;
using System.Data;

string connection = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Library; Integrated Security = true";



//метод добавления данных об авторе в базу данных localDb
static void addAuthorToDb(string connString)
{
    try
    {
// открываем соединение с базой данных в присоединенном режиме
        SqlConnection conn = new SqlConnection(connString);
        conn.Open();
// запрашиваем у пользователя данные для внесения в таблицу
        Console.WriteLine("Введите имя автора");
        string name = Console.ReadLine();
        Console.WriteLine("Введите фамилию автора");
        string fname = Console.ReadLine();
// создаем параметры для безопасного ввода данных в таблицу
        SqlParameter pName = new SqlParameter("@FirstName", SqlDbType.NVarChar);
        pName.Value = name;
        SqlParameter pFname = new SqlParameter("@LastName", SqlDbType.NVarChar);
        pFname.Value = fname;
// строка запроса SQL
        string insertString = "insert into Authors (FirstName, LastName) Values (@FirstName,@LastName)";
// выполняем команду и закрываем все подключения
        SqlCommand cmd = new SqlCommand(insertString,conn);
        cmd.Parameters.AddRange(new SqlParameter[] { pName, pFname });
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
//метод добавления данных об книге в базу данных localDb
static void addBookToDb(string connString)
{
    try
    {
// открываем соединение с базой данных в присоединенном режиме
        SqlConnection conn = new SqlConnection(connString);
        conn.Open();
// узнаем сколько авторов у нас в базе и запрашиваем ввод у пользователя с проверкой на корректность данных
        SqlCommand chckAthrs = new SqlCommand("select * from Authors", conn);
        SqlDataReader sqlAthrs = chckAthrs.ExecuteReader();
        int line = 0;
        List<int> indexes = new List<int>();
        while(sqlAthrs.Read()) 
        {
            indexes.Add((int)(sqlAthrs["id"]));
            Console.WriteLine($"{sqlAthrs["FirstName"]} {sqlAthrs["LastName"]}");
            line++;
        }
        sqlAthrs.Close();
        Console.WriteLine($"У вас базе {line} авторов. Выберите автора в данном диапазоне посредством ввода цифры");
        string choise = Console.ReadLine();
        int index;
// проверяем данных на корректность
        if(int.TryParse(choise, out index))
        {
            if (!indexes.Contains(index)) 
            {
                index = indexes[0];
                Console.WriteLine("Вы ввели неправильный индекс, автор по умолчанию выбран первый");
            }
        }
        else
        {
            index = indexes[0];
        }
// запрашиваем данные у пользователя для ввода в базу данных
        Console.WriteLine("Введите название книги");
        string title = Console.ReadLine();
        Console.WriteLine("Введите стоимость книги");
        int price = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Введите количество страниц");
        int pages = Int32.Parse(Console.ReadLine());
// создаем параметры для безопасного ввода данных в таблицу
        SqlParameter pAuthor = new SqlParameter("@AuthorId", SqlDbType.Int);
        pAuthor.Value = index;
        SqlParameter pTitle = new SqlParameter("@Title", SqlDbType.NVarChar);
        pTitle.Value = title;
        SqlParameter pPrice = new SqlParameter("@Price", SqlDbType.Int);
        pPrice.Value = price;
        SqlParameter pPages = new SqlParameter("@Pages", SqlDbType.Int);
        pPages.Value = pages;
// создаем строку SQL запрсоа
        string insertString = "insert into Books (AuthorId, Title,Price,Pages) " +
            "Values (@AuthorId,@Title,@Price,@Pages)";
// выполняем команду и закрываем все подключения
        SqlCommand cmd = new SqlCommand(insertString, conn);
        cmd.Parameters.AddRange(new SqlParameter[] { pAuthor,pTitle,pPrice,pPages});
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        conn.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
//метод вывода на экран количества авторов и количество книг в базе данных
static void getCountAthrsAndBks(string connString)
{
    try 
    {
// открываем соединение с базой данных в присоединенном режиме
        SqlConnection conn = new SqlConnection(connString);
        conn.Open();

        SqlCommand cmd = new SqlCommand(connString, conn);
// создаем команду для запросов, которые вернут нам скалярное значение
        cmd.CommandText = "Select COUNT (id) From Authors";
        var countAthrs = cmd.ExecuteScalar();
        cmd.CommandText = "Select COUNT (id) From Books";
        var countBks = cmd.ExecuteScalar();
// вывод полученных данных на экран
        Console.WriteLine($"В базе данных авторов: {countAthrs}");
        Console.WriteLine($"В базе данных книг: {countBks}");

        conn.Close();
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
//метод вычисления суммы всех книг и суммы всех страниц в таблице Books
static void calcSumBooks(string connString)
{
    try 
    {
// открываем соединение с базой данных в присоединенном режиме
        SqlConnection conn = new SqlConnection(connString);
        conn.Open();
// переменные для хранения данных о всех суммах
        int sumPrice = 0, sumPages = 0;
// создаем строку запроса SQL
        string getString = "Select Title,Price,Pages From Books";
        SqlCommand cmd = new SqlCommand(getString, conn);
        
        SqlDataReader reader = cmd.ExecuteReader();
// пока есть данные для считывание работаем с полученными данными
        while (reader.Read())
        {
            Console.WriteLine($"Книга : {reader["Title"]}");
            Console.WriteLine($"{reader["Pages"]} страниц, цена {reader["Price"]} руб.");
            sumPages += (int)reader["Pages"];
            sumPrice += (int)reader["Price"];
        }
        Console.WriteLine($"Все книги стоят {sumPrice} руб и в них суммарно страниц - {sumPages}");

        conn.Close();
    }
    catch(Exception ex) 
    { 
        Console.WriteLine(ex.Message);
    }
}