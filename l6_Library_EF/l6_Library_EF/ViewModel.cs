using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations.Builders;
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

        public static DataTable getAllAuthors()
        {
            DataTable data = createAuthorTable();

            try
            {
                List<Author> authors;
                using (LibraryEntities db = new LibraryEntities())
                {
                    authors = db.Author.ToList();

                    for (int i = 0; i < authors.Count; i++)
                    {
                        DataRow row = data.NewRow();

                        row["Name"] = authors[i].FirstName;
                        row["Fname"] = authors[i].LastName;
                        row["Books"] = authors[i].Book.Count();
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
        public static DataTable getAllPublishers()
        {
            DataTable data = createPublishersTable();

            try
            {
                List<Publisher> publishers;
                using (LibraryEntities db = new LibraryEntities())
                {
                    publishers = db.Publisher.ToList();

                    for (int i = 0; i < publishers.Count; i++)
                    {
                        DataRow row = data.NewRow();

                        row["Name"] = publishers[i].PublisherName;
                        row["Adress"] = publishers[i].Address;
                        row["Books"] = publishers[i].Book.Count();
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

            return books;
        }
        private static DataTable createAuthorTable()
        {
            DataTable authors = new DataTable("Authors");

            DataColumn Name = new DataColumn();
            Name.DataType = typeof(string);
            Name.ColumnName = "Name";
            authors.Columns.Add(Name);

            DataColumn Fname = new DataColumn();
            Fname.DataType = typeof(string);
            Fname.ColumnName = "Fname";
            authors.Columns.Add(Fname);

            DataColumn books = new DataColumn();
            books.DataType = typeof(int);
            books.ColumnName = "Books";
            authors.Columns.Add(books);

            return authors;
        }
        private static DataTable createPublishersTable()
        {
            DataTable publ = new DataTable("Publishers");

            DataColumn Name = new DataColumn();
            Name.DataType = typeof(string);
            Name.ColumnName = "Name";
            publ.Columns.Add(Name);

            DataColumn Adress = new DataColumn();
            Adress.DataType = typeof(string);
            Adress.ColumnName = "Adress";
            publ.Columns.Add(Adress);

            DataColumn books = new DataColumn();
            books.DataType = typeof(int);
            books.ColumnName = "Books";
            publ.Columns.Add(books);

            return publ;
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
        public static void addPublisher(Publisher obj)
        {
            using (LibraryEntities db = new LibraryEntities())
            {
                Publisher A = db.Publisher.Where(a => a.PublisherName == obj.PublisherName).Where(a => a.Address == obj.Address).FirstOrDefault();

                if (A == null)
                {
                    db.Publisher.Add(obj);
                    db.SaveChanges();
                }
            }
        }
        public static void updPublisher(Publisher obj)
        {
            using (LibraryEntities db = new LibraryEntities())
            {
                Publisher upd = db.Publisher.Find(obj.Id);
                upd.PublisherName = obj.PublisherName;
                upd.Address = obj.Address;
                db.SaveChanges();
            }
        }
        public static void delPublisher(Publisher obj)
        {
            try
            {
                using (LibraryEntities db = new LibraryEntities())
                {
                    Publisher del = db.Publisher.Find(obj.Id);
                    db.Publisher.Remove(del);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нельзя удалить издательство");
            }
        }

    }     
}
