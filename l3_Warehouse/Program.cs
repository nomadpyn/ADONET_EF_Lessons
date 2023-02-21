using System;
using System.Data;
using Microsoft.Data.SqlClient;
// создаем строку для подключения и само подключения к БД
string connection = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Warehouse; Integrated Security = true";
SqlConnection conn = new SqlConnection(connection);

// метод который показывает все товары на складе и возвращает список id 
static List<int> showAllProducts(ref SqlConnection conn)
{
    List<int> indexes = new List<int>();
// создаем строку запроса
    string sel = "select id, prodName from Products";

    try
    {
// создаем адаптер
        SqlDataAdapter adapter = new SqlDataAdapter(sel, conn);

        DataTable data = new DataTable();
// заполняем нашу DataTable запросом с помощью адаптера
        adapter.Fill(data);
// перебираем строки, заносим индексы в лист и выводим данные в консоль
        foreach (DataRow row in data.Rows)
        {
            var cells = row.ItemArray;
            indexes.Add((int)row[0]);
            foreach (var cell in cells)
            {
                Console.Write(cell + "\t");
            }
            Console.WriteLine();
        }
    }
    catch(Exception ex) 
    {
        Console.WriteLine(ex.Message);
    }

    return indexes;
}   
// метод вывода всей информации о конкретном товаре
static void showProduct(ref SqlConnection conn)
{
    Console.WriteLine("Список всех товаров");
// получаем список всех id в базе данных вместе с выводом данных на экран
    List<int> ind = showAllProducts(ref conn);
// запускаем цикл на корректный ввод данных от пользователя
    int choise=-1;
    do
    {
        Console.WriteLine("Выберите товар по индексу");
        if (Int32.TryParse(Console.ReadLine(), out choise))
        {
            if (!ind.Contains(choise))
            {
                Console.WriteLine("Вы неправильно выбрали, попробуйте еще раз");
            }

        }
        else
            Console.WriteLine("Вы неправильно выбрали, попробуйте еще раз");
    }
    while (!ind.Contains(choise));
// строка запроса на вывод всех данных о товаре из многотабличной базы данных
    string sel = "select prodName, typeName, provName, prodCount, prodSelfPrice,prodDate " +
        $"From Products AS P, prdType AS pT, prdProvider AS pP Where P.id = {choise} " + 
        "AND P.typeId = pT.id AND P.provId = pP.id";
    try
    {
        SqlDataAdapter adapter = new SqlDataAdapter(sel, conn);

        DataTable data = new DataTable();

        adapter.Fill(data);
// берем информацию из data и выводим в консоль в нужной нам последовательности
        Console.WriteLine(data.Rows[0][1].ToString() + ": " + data.Rows[0][0].ToString());
        Console.WriteLine("Себестоимость " + data.Rows[0][4].ToString() + ", " + data.Rows[0][3].ToString() + " шт");
        Console.WriteLine("Поставщик " + data.Rows[0][2].ToString() + ", Дата поставки " + data.Rows[0][5].ToString());
       
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
// метод вывода информации обо всех типах товара или обо всех поставщиках, одновременно с возвращением списка их индексов
static List<int> showTypesOrProviders(ref SqlConnection conn, string arg = "type")
{
    List<int>indexes = new List<int>();
// создаем переменные запроса и сообщения в зависимости от аргументов метода, по умолчанию тип товара
    string sel = "select id, typeName from prdType order by id";
    string message = "Список типов товаров:";
    if(arg == "provider") 
    {
        sel = "select id, provName from prdProvider order by id";
        message = "Список поставщиков:";
    }
// перебираем строки, заносим индексы в лист и выводим данные в консоль
    try
    {
        SqlDataAdapter adapter = new SqlDataAdapter(sel, conn);

        DataTable data = new DataTable();

        adapter.Fill(data);

        Console.WriteLine(message);
        foreach (DataRow row in data.Rows)
        {
            var cells = row.ItemArray;
            indexes.Add((int)cells[0]);
            foreach (var cell in cells)
            {
                Console.Write(cell + " ");
            }
            Console.WriteLine();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

    return indexes;
}
//  метод вывода товара с минимальным или максимальным количеством на складе
static void showMinOrMaxCount(ref SqlConnection conn, string arg = "min")
{
// создаем строку запроса и сообще в зависимости от аргуметов метода, по умолчанию минимум
    string sel = "SELECT prodName, prodCount FROM Products WHERE prodCount = (SELECT MIN(prodCount) From Products)";
    string message = "Товар с минимальным количеством :";
    if (arg == "max")
    {
        sel = "SELECT prodName, prodCount FROM Products WHERE prodCount = (SELECT MAX(prodCount) From Products)";
        message = "Товар с максимальным количеством :";
    }

    try
    {
        SqlDataAdapter adapter = new SqlDataAdapter(sel, conn);

        DataTable data = new DataTable();

        adapter.Fill(data);
// выводим сообщения в зависимости от аргумента и выводи информацию в консоль
        Console.WriteLine(message);
        foreach (DataRow row in data.Rows)
        {
            var cells = row.ItemArray;
            foreach (var cell in cells)
            {
                Console.Write(cell + "\t");
            }
            Console.WriteLine();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
// метод вывода товара с минимальной или максимальной ценой себестоимости, по умолчанию минимум
static void showMinOrMaxSelfPrice(ref SqlConnection conn, string arg = "min")
{
// создаем строку запроса и сообще в зависимости от аргуметов метода, по умолчанию минимум
    string sel = "SELECT prodName, prodSelfPrice FROM Products WHERE prodSelfPrice = (SELECT MIN(prodSelfPrice) From Products)";
    string message = "Товар с минимальной себестоимостью :";
    if (arg == "max")
    {
        sel = "SELECT prodName, prodSelfPrice FROM Products WHERE prodSelfPrice = (SELECT MAX(prodSelfPrice) From Products)";
        message = "Товар с максимальной себестоимостью :";
    }

    try
    {
        SqlDataAdapter adapter = new SqlDataAdapter(sel, conn);

        DataTable data = new DataTable();

        adapter.Fill(data);
// выводим сообщения в зависимости от аргумента и выводи информацию в консоль
        Console.WriteLine(message);
        foreach (DataRow row in data.Rows)
        {
            var cells = row.ItemArray;
            foreach (var cell in cells)
            {
                Console.Write(cell + "\t");
            }
            Console.WriteLine();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
// метод вывода товаров по типу или по поставщику, по умолчанию тип
static void showAllByTypeOrProvider(ref SqlConnection conn, string arg = "type")
{
 // создаем сообщение в зависимости от аргументов, по умолчанию тип 
    string message = "Список типов товаров:";
    if (arg == "provider")
    {
        message = "Список поставщиков:";
    }
    Console.WriteLine(message);
// получаем список индексов, в зависимости от аргументов, типа товаров или поставщики
    List<int> ind = showTypesOrProviders(ref conn, arg);
 // запускаем цикл на корректный ввод данных от пользователя
    int choise = -1;
    do
    {
        Console.WriteLine("Выберите по индексу");
        if (Int32.TryParse(Console.ReadLine(), out choise))
        {
            if (!ind.Contains(choise))
            {
                Console.WriteLine("Вы неправильно выбрали, попробуйте еще раз");
            }

        }
        else
            Console.WriteLine("Вы неправильно выбрали, попробуйте еще раз");
    }
    while (!ind.Contains(choise));
 //создаем строку запроса в зависимости от аргументов, по умолчанию тип 
    string sel = $"select  prodName From Products AS P, prdType AS pT Where P.typeId = pT.id AND P.typeId = {choise}";
    if(arg == "provider")
    {
        sel = $"select prodName From Products AS P, prdProvider AS pP Where P.provId = pP.id AND P.provId = {choise}";
    }

    try
    {
        SqlDataAdapter adapter = new SqlDataAdapter(sel, conn);

        DataTable data = new DataTable();

        adapter.Fill(data);
// перебираем DataTable и выводим данные из нее в консоль
        foreach (DataRow row in data.Rows)
        {
            var cells = row.ItemArray;
            foreach (var cell in cells)
            {
                Console.Write(cell + " ");
            }
            Console.WriteLine();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
// метод вывода самого старого товара со склада
static void oldestProduct (ref SqlConnection conn)
{
 // создаем запрос на вывод самого старого товара на складе
    string sel = "select id, prodName, prodDate From Products WHERE prodDate = (SELECT MIN(prodDate) From Products)";
    try
    {
        SqlDataAdapter adapter = new SqlDataAdapter(sel, conn);

        DataTable data = new DataTable();

        adapter.Fill(data);
        Console.WriteLine("Самый старый товар на складе");
// перебираем DataTable и выводим данные из нее в консоль
        foreach (DataRow row in data.Rows)
        {
            var cells = row.ItemArray;
            foreach (var cell in cells)
            {
                Console.Write(cell + " ");
            }
            Console.WriteLine();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

static void addPrdType(ref SqlConnection conn)
{
    string select = "select typeName from prdType";

    try
    {
        SqlDataAdapter adapter = new SqlDataAdapter(select, conn);
        SqlCommandBuilder commandBuilder= new SqlCommandBuilder(adapter);

        DataTable dt = new DataTable();

        adapter.Fill(dt);
        foreach (DataRow row in dt.Rows)
        {
            Console.WriteLine(row["typeName"]);
        }
        Console.WriteLine("\nВведите имя для нового типа товара");
        DataRow newRow = dt.NewRow();
        newRow["typeName"] = Console.ReadLine();
        dt.Rows.Add(newRow);

        adapter.Update(dt);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

static void addPrdProvider(ref SqlConnection conn)
{
    string select = "select provName from prdProvider";

    try
    {
        SqlDataAdapter adapter = new SqlDataAdapter(select, conn);
        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

        DataTable dt = new DataTable();

        adapter.Fill(dt);
        foreach (DataRow row in dt.Rows)
        {

            Console.WriteLine(row["provName"]);
        }
        Console.WriteLine("\nВведите имя нового поставщика");
        DataRow newRow = dt.NewRow();
        newRow["provName"] = Console.ReadLine();
        dt.Rows.Add(newRow);

        adapter.Update(dt);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}