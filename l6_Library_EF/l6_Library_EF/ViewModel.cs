using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace l6_Library_EF
{
    public static class ViewModel
    {
        public static DataTable getAllBooks()
        {
            DataTable data = createBookTable();

            try
            {
                List<Book> books;
                using (LibraryEntities db = new LibraryEntities())
                {
                    books = db.Book.ToList();


                    for (int i = 0; i < books.Count; i++)
                    {
                        DataRow row = data.NewRow();

                        row["id"] = books[i].Id;
                        row["bookName"] = books[i].Title;
                        row["author"] = books[i].Author.FirstName + " " + books[i].Author.LastName;
                        row["pages"] = books[i].Pages;
                        row["price"] = books[i].Price;
                        row["publish"] = books[i].Publisher.PublisherName;
                        data.Rows.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return data;
        }

        private static DataTable createBookTable() 
        {
            DataTable books = new DataTable("Books");

            DataColumn idColumn = new DataColumn();
            idColumn.DataType = typeof(int);
            idColumn.ColumnName = "id";
            idColumn.AutoIncrement = true;
            books.Columns.Add(idColumn);

            DataColumn bookName = new DataColumn();
            bookName.DataType = typeof(string);
            bookName.ColumnName = "bookName";
            books.Columns.Add(bookName);

            DataColumn author = new DataColumn();
            author.DataType = typeof(string);
            author.ColumnName = "author";
            books.Columns.Add(author);

            DataColumn pages = new DataColumn();
            pages.DataType = typeof(int);
            pages.ColumnName = "pages";
            books.Columns.Add(pages);

            DataColumn price = new DataColumn();
            price.DataType = typeof(int);
            price.ColumnName = "price";
            books.Columns.Add(price);

            DataColumn publish = new DataColumn();
            publish.DataType = typeof(string);
            publish.ColumnName = "publish";
            books.Columns.Add(publish);

            DataColumn[] keys = new DataColumn[1];
            keys[0] = idColumn;
            books.PrimaryKey = keys;

            return books;
        }

        public static void addAuthor(Author obj)
        {
            using (LibraryEntities db = new LibraryEntities())
            {
                Author A = db.Author.Where(a => a.LastName == obj.LastName).Where(a => a.FirstName == obj.FirstName).FirstOrDefault();

                if (A == null)
                {
                    db.Author.Add(obj);
                    db.SaveChanges();
                }

            }
        }

        public static void updAuthor(Author obj)
        {
            using( LibraryEntities db = new LibraryEntities())
            {
                Author upd = db.Author.Find(obj.Id);

                upd.FirstName = obj.FirstName;
                upd.LastName = obj.LastName;

                db.SaveChanges();
            }
        }
        public static void delAuthor(Author obj)
        {
            try
            {
                using (LibraryEntities db = new LibraryEntities())
                {
                    Author del = db.Author.Find(obj.Id);

                    db.Author.Remove(del);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нельзя удалить автора");
            }
        }
    }     
}
