using System;
using System.Data;
using Microsoft.Data.SqlClient;

string connection = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Warehouse; Integrated Security = true";
SqlConnection conn = new SqlConnection(connection);

showProduct(ref conn);
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
   