using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;

namespace l5_LINQ_start
{
    public static class Func
    {
        const string connString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Warehouse; Integrated Security = true";

        public static List<Product> getStartUpData()
        {
            List<Product> list = new List<Product>();
            try
            {
                SqlConnection conn = new SqlConnection(connString);

                string select = "SELECT NameProd, PriceProd, CountProd FROM Products";

                conn.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string name = rdr.GetString("NameProd");
                    int price = rdr.GetInt32("PriceProd");
                    int count = rdr.GetInt32("CountProd");
                    list.Add(new Product(name,price,count));
                }

                conn.Close();
            }
            catch (Exception ex) { }
            return list;
        }
    }
}
