using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraelTest.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public  DateTime ReleaseDate { get; set; }
        public  int PageCount { get; set; }
        public ICollection<BookAuthor> BookAuthor { get; set; } = new List<BookAuthor>();

    }
}