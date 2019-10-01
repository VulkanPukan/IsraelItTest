using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IsraelTest.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using WebGrease.Css.Extensions;

namespace IsraelTest.Controllers
{
    public class AuthorsController : ApiController
    {
        BookContext bc = new BookContext();

     
        // GET: api/Author
        public IEnumerable<Author> Get()
        {
            return bc.Authors.Include(ba=>ba.BookAuthor).ThenInclude(bk => bk.Book);
        }

        [Route("api/Authors/{page}/{pageSize}/{author}")]
        public IEnumerable<Author> Get(int page, int pageSize, string author)
        {
            int toSkip = (page - 1) * pageSize;
            return bc.Authors.Where(x=> x.Name.Contains(author)).Skip(toSkip).Take(pageSize).Include(ba => ba.BookAuthor).ThenInclude(bk => bk.Book);

        }

        // GET: api/Author/5
        public Author Get(int id)
        {
            Author au = bc.Authors.Include(bk => bk.BookAuthor).ThenInclude(ba => ba.Book).FirstOrDefault(x => x.Id == id);
            if (au == null || au.Id < 1)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "This author wasn't found!"));
            return au;
        }

        // POST: api/Author
        public void Post(AuthorModel model)
        {
            Author author = bc.Authors.FirstOrDefault(x => x.Id == model.Id);
            if (author != null && author.Id > 0)
            {
                var modelAuthor = model.GetAuthor();
                author.DOB = modelAuthor.DOB;
                author.Name = modelAuthor.Name;
                bc.Authors.Update(author);
                bc.SaveChanges();
            }
            else
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "This author wasn't found!"));
        }

        // PUT: api/Author/5
        [HttpPut]
        public void Put(AuthorModel model)
        {
            Author author = model.GetAuthor();
            bc.Authors.Add(author);
            bc.SaveChanges();
        }

        // DELETE: api/Author/5
        public void Delete(int id)
        {
            Author author = bc.Authors.FirstOrDefault(x => x.Id == id);
            if (author == null || author.Id < 1)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "This author wasn't found!"));
            bc.Authors.Remove(author);
            bc.SaveChanges();
        }
    }
}
