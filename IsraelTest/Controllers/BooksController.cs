using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Validation;
using IsraelTest.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace IsraelTest.Controllers
{
    public class BooksController : ApiController
    {
        BookContext bc = new BookContext();
        // GET: api/Book
        public IEnumerable<Book> Get()
        {
            return bc.Books;
        }

        // GET: api/Book/5
        public Book Get(int id)
        {
            Book book = bc.Books.Include(bk => bk.BookAuthor).ThenInclude(ba => ba.Author).FirstOrDefault(x => x.Id == id);
            if (book == null || book.Id < 1)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "This book wasn't found!"));
            return book;
        }

        [Route("api/Books/{page}/{pageSize}/{author}")]
        public IEnumerable<Book> Get(int page, int pageSize,string author)
        {
            List<Book> books = bc.Books.Include(ba => ba.BookAuthor).ThenInclude(au => au.Author).ToList();
            books = books.Where(x => x.BookAuthor.FirstOrDefault(au => au.Author.Name.Contains(author)) != null).ToList();
            int toSkip = (page - 1) * pageSize;
            return books.Skip(toSkip).Take(pageSize);

        }

     

        // POST: api/Book
        public void Post(BookModel model)
        {
            Book book = bc.Books.FirstOrDefault(x => x.Id == model.Id);
            if (book != null && book.Id > 0)
            {
                var modelBook = model.GetBook();
                book.PageCount = modelBook.PageCount;
                book.Description = modelBook.Description;
                book.ReleaseDate = modelBook.ReleaseDate;
                book.Name = modelBook.Name;
                bc.Books.Update(book);
                bc.SaveChanges();
            }
            else
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "This book wasn't found!"));
        }

        // PUT: api/Book/5
        [HttpPut]
        public void Put(BookModel model)
        {
            Book book = model.GetBook();

            bc.Books.Add(book);
            bc.SaveChanges();
            int newBoolId = book.Id;
            foreach (var authorId in model.Authors)
            {
                var author = bc.Authors.FirstOrDefault(x => x.Id == authorId);
                try
                {
                    if (author != null && author.Id > 0)
                    {
                        BookAuthor ba = new BookAuthor()
                        {
                            Author = author,
                            AuthorId = authorId,
                            Book = book,
                            BookId = book.Id
                        };
                        bc.BookAuthor.Add(ba);

                        bc.SaveChanges();
                    }
                }
                catch
                {
                    continue;
                }
            }

        }

        // DELETE: api/Book/5
        public void Delete(int id)
        {
            Book book = bc.Books.FirstOrDefault(x => x.Id == id);
            if (book == null || book.Id < 1)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "This book wasn't found!"));

            bc.Books.Remove(book);
            bc.SaveChanges();
        }
    }
}
