using l6_Library_EF.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;

namespace l6_Library_EF
{
    public static class ViewModel
    {
// метод создания таблицы для вывода списка книг в datagrid
        public static DataTable getAllBooks()
        {
// создаем макет таблицы с помощью метода
            DataTable data = createBookTable();
            try
            {
// с помощью ef загружаем данные по книгам в лист
                List<Book> books;
                using (LibraryEntities db = new LibraryEntities())
                {
                    books = db.Book.ToList();
// в цикле заполняем таблицу данными из БД как нам надо
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

// метод создания таблицы для вывода списка авторов в datagrid
        public static DataTable getAllAuthors()
        {
// создаем макет таблицы с помощью метода
            DataTable data = createAuthorTable();
            try
            {
// с помощью ef загружаем данные по авторам в лист
                List<Author> authors;
                using (LibraryEntities db = new LibraryEntities())
                {
                    authors = db.Author.ToList();
 // в цикле заполняем таблицу данными из БД как нам надо
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

// метод создания таблицы для вывода списка издателей в datagrid
        public static DataTable getAllPublishers()
        {
// создаем макет таблицы с помощью метода
            DataTable data = createPublishersTable();
            try
            {
// с помощью ef загружаем данные по издателям в лист
                List<Publisher> publishers;
                using (LibraryEntities db = new LibraryEntities())
                {
                    publishers = db.Publisher.ToList();
// в цикле заполняем таблицу данными из БД как нам надо
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

// метод создания шаблона таблицы книги
        private static DataTable createBookTable() 
        {
// создаем таблицу Книги
            DataTable books = new DataTable("Books");
// создаем колонку bookName
            DataColumn bookName = new DataColumn();
            bookName.DataType = typeof(string);
            bookName.ColumnName = "bookName";
            books.Columns.Add(bookName);
// создаем колонку author
            DataColumn author = new DataColumn();
            author.DataType = typeof(string);
            author.ColumnName = "author";
            books.Columns.Add(author);
// создаем колонку pages
            DataColumn pages = new DataColumn();
            pages.DataType = typeof(int);
            pages.ColumnName = "pages";
            books.Columns.Add(pages);
// создаем колонку price
            DataColumn price = new DataColumn();
            price.DataType = typeof(int);
            price.ColumnName = "price";
            books.Columns.Add(price);
// создаем колонку publish
            DataColumn publish = new DataColumn();
            publish.DataType = typeof(string);
            publish.ColumnName = "publish";
            books.Columns.Add(publish);
// возвращаем шаблон таблицы с пустыми строками
            return books;
        }

// метод создания шаблона таблицы авторы
        private static DataTable createAuthorTable()
        {
// создаем таблицу Авторы
            DataTable authors = new DataTable("Authors");
// создаем колонку Name
            DataColumn Name = new DataColumn();
            Name.DataType = typeof(string);
            Name.ColumnName = "Name";
            authors.Columns.Add(Name);
// создаем колонку Fname
            DataColumn Fname = new DataColumn();
            Fname.DataType = typeof(string);
            Fname.ColumnName = "Fname";
            authors.Columns.Add(Fname);
// Создаем колонку Books
            DataColumn books = new DataColumn();
            books.DataType = typeof(int);
            books.ColumnName = "Books";
            authors.Columns.Add(books);
// возвращаем шаблон таблицы с пустыми строками
            return authors;
        }

// метод создания шаблона издатели
        private static DataTable createPublishersTable()
        {
// создаем таблицу Издатели
            DataTable publ = new DataTable("Publishers");
// создаем колонку Name
            DataColumn Name = new DataColumn();
            Name.DataType = typeof(string);
            Name.ColumnName = "Name";
            publ.Columns.Add(Name);
// создаем колонку Adress
            DataColumn Adress = new DataColumn();
            Adress.DataType = typeof(string);
            Adress.ColumnName = "Adress";
            publ.Columns.Add(Adress);
// создаем колонку Books
            DataColumn books = new DataColumn();
            books.DataType = typeof(int);
            books.ColumnName = "Books";
            publ.Columns.Add(books);
// возвращаем шаблон таблицы с пустыми строками
            return publ;
        }

// метод добавления нового автора в БД
        public static void addAuthor(Author obj)
        {
            using (LibraryEntities db = new LibraryEntities())
            {
// проверям на наличие такого автора в БД
                Author A = db.Author.Where(a => a.LastName == obj.LastName).Where(a => a.FirstName == obj.FirstName).FirstOrDefault();
// если отсутствует такой, то добавляем и сохраняем изменения
                if (A == null)
                {
                    db.Author.Add(obj);
                    db.SaveChanges();
                }
            }
        }

// метод изменения существующего автора в БД
        public static void updAuthor(Author obj)
        {
            using( LibraryEntities db = new LibraryEntities())
            {
// берем автора из БД по id
                Author upd = db.Author.Find(obj.Id);
// изменяем все поля сущности и сохраняем изменения
                upd.FirstName = obj.FirstName;
                upd.LastName = obj.LastName;
                db.SaveChanges();
            }
        }

// метод удаления автора из БД
        public static void delAuthor(Author obj)
        {
            try
            {
                using (LibraryEntities db = new LibraryEntities())
                {
// берем автора по id
                    Author del = db.Author.Find(obj.Id);
// удаляем, если это возможно и сохраняем изменения
                    db.Author.Remove(del);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нельзя удалить автора");
            }
        }

// метод добавления нового издателя в БД
        public static void addPublisher(Publisher obj)
        {
            using (LibraryEntities db = new LibraryEntities())
            {
 // проверям на наличие такого издателя в БД
                Publisher A = db.Publisher.Where(a => a.PublisherName == obj.PublisherName).Where(a => a.Address == obj.Address).FirstOrDefault();
// если отсутствует такой, то добавляем и сохраняем изменения
                if (A == null)
                {
                    db.Publisher.Add(obj);
                    db.SaveChanges();
                }
            }
        }

// метод изменения существующего издателя в БД
        public static void updPublisher(Publisher obj)
        {
            using (LibraryEntities db = new LibraryEntities())
            {
// берем издателя из БД по id
                Publisher upd = db.Publisher.Find(obj.Id);
// изменяем все поля сущности и сохраняем изменения
                upd.PublisherName = obj.PublisherName;
                upd.Address = obj.Address;
                db.SaveChanges();
            }
        }

// метод удаления издателя из БД
        public static void delPublisher(Publisher obj)
        {
            try
            {
                using (LibraryEntities db = new LibraryEntities())
                {
// берем издателя по id
                    Publisher del = db.Publisher.Find(obj.Id);
// удаляем, если это возможно и сохраняем изменения
                    db.Publisher.Remove(del);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нельзя удалить издательство");
            }
        }
 
// метод корректного ввода числа в БД 
        public static int GetIntData(string text)
        {
            int result;
            if (Int32.TryParse(text, out result))
            {
                if (result > 0)
                {
                    return result;
                }
            }
            return 1;
        }
    
// метод добавления новой книги в БД
        public static void addBook(Book obj)
        {
            using(LibraryEntities db = new LibraryEntities())
            {
// проверяем книгу на присутствие в БД
                Book book = db.Book.Where(b => b.Title == obj.Title)
                    .Where(b => b.IdAuthor == obj.IdAuthor)
                    .Where(b => b.IdPublisher == obj.IdPublisher)
                    .Where(b => b.Pages == obj.Pages)
                    .FirstOrDefault();
// если отсутствует, то добавляем в БД и сохраняем изменения
                if(book == null)
                {
                    db.Book.Add(obj);
                    db.SaveChanges();
                }
            }
        }
       
// метод изменения существующей книги в БД        
        public static void updBook(Book obj)
        {
            using (LibraryEntities db = new LibraryEntities())
            {
// находим книгу по id 
                Book upd = db.Book.Find(obj.Id);
// изменяем все поля сущности и сохраняем изменения в БД
                upd.Title = obj.Title;
                upd.IdAuthor = obj.IdAuthor;
                upd.IdPublisher = obj.IdPublisher; 
                upd.Pages = obj.Pages;
                upd.Price = obj.Price;
                db.SaveChanges();
            }
        }
        
// метод удаления книги из БД        
        public static void delBook(Book obj)
        {
            try
            {
                using (LibraryEntities db = new LibraryEntities())
                {
// удаляем книгу по id и сохраняем изменения в БД
                    Book del = db.Book.Find(obj.Id);
                    db.Book.Remove(del);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нельзя удалить книгу");
            }
        }
    }     
}
