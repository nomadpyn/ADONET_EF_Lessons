﻿using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;

SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Vegetables_Fruits; Integrated Security = true");



static void addDataToDb(ref SqlConnection conn)
{
	try
	{
		conn.Open();

        string insert = "insert into Veg_Fru (Name,Type,Color,Calories) " +
            "Values (@Name,@Type,@Color,@Calories)";
        SqlCommand cmd = new SqlCommand(insert, conn);
        cmd.Parameters.AddRange(GetParameters());
        cmd.ExecuteNonQuery();
		conn.Close();
	}
	catch (Exception ex)
	{

		Console.WriteLine(ex.Message);
	}
}

static SqlParameter[] GetParameters()
{
    int choise = 0;
    do
    {
        Console.WriteLine("Вы хотите добавить овощ(1) или фрукт(2)?");
        if (Int32.TryParse(Console.ReadLine(), out choise))
        {
            if (choise != 1 && choise != 2)
            {
                Console.WriteLine("Вы неправильно выбрали, попробуйте еще раз");
            }
        }
        else
            Console.WriteLine("Вы неправильно выбрали, попробуйте еще раз");
    }
    while (choise != 1 && choise != 2);
    Console.WriteLine("Введите название продукта");
    string name = Console.ReadLine();
    Console.WriteLine("Введите цвет");
    string color = Console.ReadLine();
    int calories = 0;
    do
    {
        Console.WriteLine("Введите калорийность");

        if (Int32.TryParse(Console.ReadLine(), out calories))
        {
            if (calories < 0)
            {
                Console.WriteLine("Калорийность не может быть отрицательная!");
            }
        }
        else
            Console.WriteLine("Вы ввели данные не корректно");
    }
    while (calories < 0);

    SqlParameter paramName = new SqlParameter("@Name", SqlDbType.NVarChar);
    paramName.Value = name;
    SqlParameter paramType = new SqlParameter("@Type", SqlDbType.NVarChar);
    if (choise == 1)
    {
        paramType.Value = "Овощ";
    }
    else
    {
        paramType.Value = "Фрукт";
    }
    SqlParameter paramColor = new SqlParameter("@Color",SqlDbType.NVarChar);
    paramColor.Value = color;
    SqlParameter paramCalories = new SqlParameter("@Calories", SqlDbType.Int);
    paramCalories.Value = calories;

    return new SqlParameter[] {paramName,paramType, paramColor, paramCalories};
} 

static void showAllData(ref SqlConnection conn)
{
    try
    {
        conn.Open();

        string select = "Select * From Veg_Fru";

        SqlCommand cmd = new SqlCommand(select, conn);

        SqlDataReader data = cmd.ExecuteReader();

        while (data.Read())
        {
            Console.WriteLine($"{data["Name"]} Цвет {data["Color"]}, калорийность {data["Calories"]}");
        }
        data.Close();

        conn.Close();
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
    }
}

static void showNames(ref SqlConnection conn) 
{
    try
    {
        conn.Open();

        string select = "Select Distinct Name From Veg_Fru";

        SqlCommand cmd = new SqlCommand(select, conn);

        SqlDataReader data = cmd.ExecuteReader();

        while (data.Read())
        {
            Console.WriteLine($"{data["Name"]}");
        }
        data.Close();

        conn.Close();
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
    }
}

static void showColors(ref SqlConnection conn)
{
    try
    {
        conn.Open();

        string select = "Select Distinct Color From Veg_Fru";

        SqlCommand cmd = new SqlCommand(select, conn);

        SqlDataReader data = cmd.ExecuteReader();

        while (data.Read())
        {
            Console.WriteLine($"{data["Color"]}");
        }
        data.Close();

        conn.Close();
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
    }
}