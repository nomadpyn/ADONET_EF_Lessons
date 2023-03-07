using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Media.TextFormatting;
using System.ComponentModel;

namespace l5_LINQ_start
{
    public static class Func
    {
        const string connString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Warehouse; Integrated Security = true";

        public static BindingList<Product> getStartUpData()
        {
            BindingList<Product> list = new BindingList<Product>();
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

        public static void addDataToDB(Product obj)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connString);
                string insert = "INSERT INTO Products (NameProd, PriceProd, CountProd) VALUES (@NameProd, @PriceProd, @CountProd)";
                conn.Open();
                SqlCommand cmd = new SqlCommand(insert, conn);
                SqlParameter paramName = new SqlParameter("@NameProd", SqlDbType.NVarChar);
                paramName.Value = obj.Name;
                cmd.Parameters.Add(paramName);
                SqlParameter paramPrice = new SqlParameter("@PriceProd", SqlDbType.Int);
                paramPrice.Value = obj.Price;
                cmd.Parameters.Add(paramPrice);
                SqlParameter paramCount = new SqlParameter("@CountProd", SqlDbType.Int);
                paramCount.Value = obj.Count;
                cmd.Parameters.Add(paramCount);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex) { }        
        }
        public static int GetIntData(string text)
        {
            int result;
            if(Int32.TryParse(text,out result))
            {
                if(result > 0)
                {
                    return result;
                }
            }
            return 0;
        }

        public static List<Product> getLessProductsPrice(ref BindingList<Product> products, int price)
        {
            return products.Where(p => p.Price < price).OrderBy(p => p.Price).ToList();
        }

        public static List<Product> getMoreProductsPrice(ref BindingList<Product> products, int price)
        {
            return products.Where(p => p.Price > price).OrderBy(p => p.Price).ToList();
        }
        public static List<Product> getLessProductsCount(ref BindingList<Product> products, int count)
        {
            return products.Where(p => p.Count < count).OrderBy(p => p.Count).ToList();
        }

        public static List<Product> getMoreProductsCount(ref BindingList<Product> products, int count)
        {
            return products.Where(p => p.Count > count).OrderBy(p => p.Count).ToList();
        }
    }
}
