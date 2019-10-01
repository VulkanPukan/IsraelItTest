using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraelTest.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ReleaseDate { get; set; }
        public int PageCount { get; set; }
        public int [] Authors { get; set; }

        public Book GetBook()
        {
            return new Book() { Name = this.Name, Description = this.Description, ReleaseDate = DateTime.Parse(this.ReleaseDate), PageCount = this.PageCount};
        }
    }
}