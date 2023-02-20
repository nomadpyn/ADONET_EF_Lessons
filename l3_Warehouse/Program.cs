using System;
using System.Data;
using Microsoft.Data.SqlClient;

string connection = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Warehouse; Integrated Security = true";
SqlConnection conn = new SqlConnection(connection);


static List<int> showAllProducts(ref SqlConnection conn)
{
    List<int> indexes = new List<int>();
    string sel = "select id, prodName from Products";

    try
    {
        SqlDataAdapter adapter = new SqlDataAdapter(sel, conn);

        DataTable data = new DataTable();

        adapter.Fill(data);

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

static void showProduct(ref SqlConnection conn)
{
    Console.WriteLine("Список всех товаров");

    List<int> ind = showAllProducts(ref conn);

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

    string sel = "select prodName, typeName, provName, prodCount, prodSelfPrice,prodDate " +
        $"From Products AS P, prdType AS pT, prdProvider AS pP Where P.id = {choise} " + 
        "AND P.typeId = pT.id AND P.provId = pP.id";
    try
    {
        SqlDataAdapter adapter = new SqlDataAdapter(sel, conn);

        DataTable data = new DataTable();

        adapter.Fill(data);

        Console.WriteLine(data.Rows[0][1].ToString() + ": " + data.Rows[0][0].ToString());
        Console.WriteLine("Себестоимость " + data.Rows[0][4].ToString() + ", " + data.Rows[0][3].ToString() + " шт");
        Console.WriteLine("Поставщик " + data.Rows[0][2].ToString() + ", Дата поставки " + data.Rows[0][5].ToString());
       
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

static List<int> showTypesOrProviders(ref SqlConnection conn, string arg = "type")
{
    List<int>indexes = new List<int>();

    string sel = "select id, typeName from prdType order by id";
    string message = "Список типов товаров:";
    if(arg == "provider") 
    {
        sel = "select id, provName from prdProvider order by id";
        message = "Список поставщиков:";
    }

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

static void showMinOrMaxCount(ref SqlConnection conn, string arg = "min")
{
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

static void showMinOrMaxSelfPrice(ref SqlConnection conn, string arg = "min")
{
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

static void showAllByTypeOrProvider(ref SqlConnection conn, string arg = "type")
{
    
    string message = "Список типов товаров:";
    if (arg == "provider")
    {
        message = "Список поставщиков:";
    }
    Console.WriteLine(message);

    List<int> ind = showTypesOrProviders(ref conn, arg);

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

static void oldestProduct (ref SqlConnection conn)
{
    string sel = "select id, prodName, prodDate From Products WHERE prodDate = (SELECT MIN(prodDate) From Products)";
    try
    {
        SqlDataAdapter adapter = new SqlDataAdapter(sel, conn);

        DataTable data = new DataTable();

        adapter.Fill(data);
        Console.WriteLine("Самый старый товар на складе");

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
