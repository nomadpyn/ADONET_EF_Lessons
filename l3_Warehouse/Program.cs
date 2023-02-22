using System.Data;
using Microsoft.Data.SqlClient;

// создаем строку для подключения и само подключения к БД
string connection = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Warehouse; Integrated Security = true";
SqlConnection conn = new SqlConnection(connection);

delProvider(ref conn);

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
    // запускаем цикл на корректный ввод данных от пользователя методом getIndex
    int choise = getIndex(ref ind);
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
// запускаем цикл на корректный ввод данных от пользователя методом getIndex
    int choise = getIndex(ref ind);
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

// метод добавления новых типов товара в БД
static void addPrdType(ref SqlConnection conn)
{
// создаем строку для вывода всех типов на экран, чтобы не ввести такой же, т.к. они уникальны
    string select = "select typeName from prdType";
    try
    {
// создаем адаптер и команды для insert
        SqlDataAdapter adapter = new SqlDataAdapter(select, conn);
        SqlCommandBuilder commandBuilder= new SqlCommandBuilder(adapter);
        DataTable dt = new DataTable();
// заполням нашу DataTable
        adapter.Fill(dt);
        foreach (DataRow row in dt.Rows)
        {
            Console.WriteLine(row["typeName"]);
        }
// ввод нового типа товара и добавление его в новую строку 
        Console.WriteLine("\nВведите имя для нового типа товара");
        DataRow newRow = dt.NewRow();
        newRow["typeName"] = Console.ReadLine();
        dt.Rows.Add(newRow);
// Update таблицы в БД с помощью адаптера
        adapter.Update(dt);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

// метод добавления новых поставщиков в БД
static void addPrdProvider(ref SqlConnection conn)
{
// создаем строку для вывода всех поставщиков, чтобы не повторить ввод уникального значения
    string select = "select provName from prdProvider";
    try
    {
// создаем адаптер и команды для insert
        SqlDataAdapter adapter = new SqlDataAdapter(select, conn);
        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
        DataTable dt = new DataTable();
// заполням нашу DataTable
        adapter.Fill(dt);
        foreach (DataRow row in dt.Rows)
        {

            Console.WriteLine(row["provName"]);
        }
// ввод нового товара и добавление его в новую строку 
        Console.WriteLine("\nВведите имя нового поставщика");
        DataRow newRow = dt.NewRow();
        newRow["provName"] = Console.ReadLine();
        dt.Rows.Add(newRow);
// Update таблицы в БД с помощью адаптера
        adapter.Update(dt);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

// метод, который возвращает выбранный индекс типа товара или поставщика, в зависимости от аргумента
static int selectTypeOrProvider(ref SqlConnection conn, string arg = "type")
{
// в завимости от аргумента метода создаем строки запроса и сообщения
    string sel = "select id, typeName from prdType order by typeName";
    string message = "Список типов товаров:";
    if (arg == "provider")
    {
        sel = "select id, provName from prdProvider order by provName";
        message = "Список поставщиков:";
    }    
    try
    {
// создаем адаптер и DataTable
        SqlDataAdapter adapter = new SqlDataAdapter(sel, conn);
        DataTable data = new DataTable();
        adapter.Fill(data);
// Создаем List индексов для запроса из БД
        List<int> indexes = new List<int>();
// выводим все строки из DataTable, а индекс добавляем в List для дальнейшей обработки        
        Console.WriteLine(message);
        foreach (DataRow row in data.Rows)
        {
            indexes.Add((int)row["id"]);
            Console.WriteLine(row[1] +" - " + row["id"]);             
        }
// запускаем цикл на корректный ввод данных от пользователя методом getIndex
        int choise = getIndex(ref indexes);

        return choise;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    return 0;
}

// метод добавления нового товара в БД
static void addProduct(ref SqlConnection conn)
{
// создаем строку на запрос
    string select = "select * from Products";
    try
    {
// создаем адаптер и датасет
        SqlDataAdapter adapter = new SqlDataAdapter(select, conn);       
        DataSet set = new DataSet();
        adapter.Fill(set);
// поочередно вводим данные в новую строку таблицы
        DataTable dt = set.Tables[0];
        DataRow newRow = dt.NewRow();
// ввод имени товара
        Console.WriteLine("Введите название товара");
        newRow["prodName"] = Console.ReadLine();
// выбираем тип товара из существующих с помощью selectTypeOrProvider 
        Console.WriteLine("Выберите тип товара");
        newRow["typeId"] = selectTypeOrProvider(ref conn, "type");
 // выбираем поставщика из существующих с помощью selectTypeOrProvider 
        Console.WriteLine("Выберите поставщика");
        newRow["provId"] = selectTypeOrProvider(ref conn, "provider");
// ввод количества товара, и если не успешно то ставим по умолчанию 1
        Console.WriteLine("Введите количество товара");
        newRow["prodCount"] = Int32.TryParse(Console.ReadLine(),out int resultС) ? resultС : 1;
        Console.WriteLine("Введите себестоимость товара");
// ввод себестоимости товара, и если не успешно то ставим по умолчанию 1
        newRow["prodSelfPrice"] = Int32.TryParse(Console.ReadLine(), out int resultSp) ? resultSp : 1;
// ввод даты поступления, либо выбор текущей даты, либо в случае провала парсинга тоже добавляется текущая дата        
        Console.WriteLine("Введите дату поставки (год,месяц,число) или введите фразу текущая дата");
        newRow["prodDate"] = DateTime.TryParse(Console.ReadLine(),out DateTime dateTime) ? dateTime : DateTime.Now;
// добавляем данные в нашу таблицу в Dataset и синхронизируем с БД
        dt.Rows.Add(newRow);
        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
        adapter.Update(set);
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

// метод обновления информации о типах товара или поставщиках
static void updPrdTypeOrProvider(ref SqlConnection conn, string arg = "type")
{
// выбираем индекс, позиции которую надо изменить методом selectTypeOrProvider
    Console.WriteLine("Выберите позицию для изменения по индексу");
    int change;
// в зависимости от аргументов метода выбираем изменения поставщика или типа товара
    if (arg == "provider")
    {
        change = selectTypeOrProvider(ref conn, "provider");
        Console.WriteLine("Введите новое имя для этого поставщика");
    }
    else
    {
        change = selectTypeOrProvider(ref conn, "type");
        Console.WriteLine("Введите новое имя для этого типа товара");
    }
// считываем новое имя
    string newName = Console.ReadLine();    
    try
    {
// в зависимости от аргументов создаем строку запроса и имя колонки для изменения данных
        string sel = null;
        string collumnName =null; 
        if (arg == "provider")
        {
            sel = $"select id, provName from prdProvider where id = {change}";
            collumnName = "provName";
        }
        else
        {
            sel = $"select id, typeName from prdType where id = {change}";
            collumnName = "typeName";
        }
// создаем адаптер и считываем даннные
        SqlDataAdapter adapter = new SqlDataAdapter(sel, conn);
        SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
        DataTable data = new DataTable();
        adapter.Fill(data);
// изменяем имя данных в выбранной колонке и обновляем данные в таблице
        data.Rows[0][collumnName] = newName;
        adapter.Update(data);                    
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

// метод обновления информации о товаре в БД
static void updProduct(ref SqlConnection conn)
{
// выводим все продукты на консоль и получаем их id
    List<int> list = showAllProducts(ref conn);
    Console.WriteLine("Какой товар вы хотите изменить?");
// запускаем цикл на корректный ввод данных от пользователя методом getIndex
    int idProduct = getIndex(ref list);
    Console.WriteLine("Для изменения данных введите их, для пропуска нажмите Enter");

    try 
    {
        string select = $"Select * From Products Where id = {idProduct}";
        // создаем адаптер и считываем даннные
        SqlDataAdapter adapter = new SqlDataAdapter(select, conn);
        SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
        DataTable data = new DataTable();
        adapter.Fill(data);
// запрашиваем изменение имени товара
        Console.WriteLine($"Текущее имя: {data.Rows[0]["prodName"]}");
        Console.WriteLine("Введите имя товара");
        string name = Console.ReadLine();
        if(name != null && name != "")
        {
            data.Rows[0]["prodName"] = name;
        }
// запрашиваем изменение количества товара
        Console.WriteLine($"Текущее количество: {data.Rows[0]["prodCount"]}");
        Console.WriteLine("Введите количество товара");
        if (Int32.TryParse(Console.ReadLine(), out int resultС))
        {
            if (resultС > 0)
            {
                data.Rows[0]["prodCount"] = resultС;
            }
        }
// запрашиваем изменение себестоимости товара
        Console.WriteLine($"Текущая себестоимость: {data.Rows[0]["prodSelfPrice"]}");
        Console.WriteLine("Введите себестоимость товара");
        if (Int32.TryParse(Console.ReadLine(), out int resultP))
        {
            if (resultP > 0)
            {
                data.Rows[0]["prodSelfPrice"] = resultP;
            }
        }
// запрашиваем изменение даты поставки товара
        Console.WriteLine($"Текущая дата поставки: {data.Rows[0]["prodDate"]}");
        Console.WriteLine("Введите дату поставки товара(год, месяц, день)");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime dateTime))
        {
            data.Rows[0]["prodDate"] = dateTime;
        }
// синхронизируем данные с БД
        adapter.Update(data);
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

// метод выбора id из list с данными
static int getIndex(ref List<int> indexes)
{
    int choise = 0;
    do
    {
        Console.WriteLine("Выберите товар по индексу");
        if (Int32.TryParse(Console.ReadLine(), out choise))
        {
            if (!indexes.Contains(choise))
            {
                Console.WriteLine("Вы неправильно выбрали, попробуйте еще раз");
            }
        }
        else
            Console.WriteLine("Вы неправильно выбрали, попробуйте еще раз");
    }
    while (!indexes.Contains(choise));

    return choise;
}

// метод удалине товара из основной таблицы
static void delProduct(ref SqlConnection conn)
{
 // выводим список товаров, доступных к удалению
    Console.WriteLine("Список товаров, доступных для удаления");
    List<int> list = showAllProducts(ref conn);
    int delId = getIndex(ref list);

    try
    {
// создаем строку подключения, адаптер и commandBuilder
        string select = $"select id, prodName from products where id = {delId}";
        SqlDataAdapter adapter = new SqlDataAdapter(select, conn);
        SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
        DataTable data = new DataTable();
        adapter.Fill(data);
// подтверждаем удаление
        Console.WriteLine($"Вы хотите удалить <<{data.Rows[0]["prodName"]}>> и все сопутствующие данные");
        Console.WriteLine("Вы уверены Да(Y)/Нет(N)");
        string choise = Console.ReadLine();
        if(choise == "Y" || choise == "Да")
        {
            data.Rows[0].Delete();
            adapter.Update(data);
            Console.WriteLine("Данные успешно удалены");
        }
        else
        {
            Console.WriteLine("Вы отменили операцию удаления");
        }
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

// метод удаления типа товара 
static void delType(ref SqlConnection conn)
{
    try
    {
// создаем строку для запроса и адаптер для выгрузки данных
        string select = "SELECT id, typeName FROM prdType " +
            "WHERE id  NOT IN(Select distinct typeId from Products)";

        SqlDataAdapter adapter = new SqlDataAdapter(select, conn);        
        DataTable data = new DataTable();
        adapter.Fill(data);
// выводим список доступных для удаления типов товаров
        Console.WriteLine("Следующие типы товаров доступны для удаления (т.к. нет взаимосвязей)");
        List<int> indexes = new List<int>();
// выводим имена и сохраняем в list их id
        foreach(DataRow row in data.Rows)
        {
            Console.WriteLine(row["typeName"] + " " + row["id"]);
            indexes.Add((int)row["id"]);
        }
// если что то доступно для удаления то предоставляем выбор
        if (indexes.Count > 0)
        {
            int ind = getIndex(ref indexes);
// очищаем list, адаптер и DataTable
            indexes.Clear();
            data.Clear();
            adapter.Dispose();
// новым запросом заполняем таблицу из БД
            select = $"select id,typeName from prdType where id = {ind}";
            adapter = new SqlDataAdapter(select, conn);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
            adapter.Fill(data);
// подтверждаем удаление
            Console.WriteLine($"Вы хотите удалить <<{data.Rows[0]["typeName"]}>>");
            Console.WriteLine("Вы уверены Да(Y)/Нет(N)");
            string choise = Console.ReadLine();
            if (choise == "Y" || choise == "Да")
            {
                data.Rows[0].Delete();
                adapter.Update(data);
                Console.WriteLine("Данные успешно удалены");
            }
            else
            {
                Console.WriteLine("Вы отменили операцию удаления");
            }
        }
        else
            Console.WriteLine("Нет доступных типов товара для удаления");
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

// метод удаление поставщиков
static void delProvider(ref SqlConnection conn)
{
    try
    {
 // создаем строку для запроса и адаптер для выгрузки данных
        string select = "SELECT id, provName FROM prdProvider " +
            "WHERE id  NOT IN(Select distinct provId from Products)";

        SqlDataAdapter adapter = new SqlDataAdapter(select, conn);
        DataTable data = new DataTable();
        adapter.Fill(data);
// выводим список доступных для удаления поставщиков
        Console.WriteLine("Следующие поставщики доступны для удаления (т.к. нет взаимосвязей)");
        List<int> indexes = new List<int>();
// выводим имена и сохраняем в list их id
        foreach (DataRow row in data.Rows)
        {
            Console.WriteLine(row["provName"] + " " + row["id"]);
            indexes.Add((int)row["id"]);
        }
// если что то доступно для удаления то предоставляем выбор
        if (indexes.Count > 0)
        {
            int ind = getIndex(ref indexes);
// очищаем list, адаптер и DataTable
            indexes.Clear();
            data.Clear();
            adapter.Dispose();
// новым запросом заполняем таблицу из БД
            select = $"select id,provName from prdProvider where id = {ind}";
            adapter = new SqlDataAdapter(select, conn);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
            adapter.Fill(data);
// подтверждаем удаление
            Console.WriteLine($"Вы хотите удалить <<{data.Rows[0]["provName"]}>>");
            Console.WriteLine("Вы уверены Да(Y)/Нет(N)");
            string choise = Console.ReadLine();
            if (choise == "Y" || choise == "Да")
            {
                data.Rows[0].Delete();
                adapter.Update(data);
                Console.WriteLine("Данные успешно удалены");
            }
            else
            {
                Console.WriteLine("Вы отменили операцию удаления");
            }
        }
        else
            Console.WriteLine("Нет доступных поставщиков для удаления");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}