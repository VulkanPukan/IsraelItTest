using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraelTest.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public ICollection<BookAuthor> BookAuthor { get; set; } = new List<BookAuthor>();

        public override string ToString()
        {
            return "Id: " + Id + " Name: " + Name + " (" + DOB.ToShortDateString() + ")";
        }
    }
}