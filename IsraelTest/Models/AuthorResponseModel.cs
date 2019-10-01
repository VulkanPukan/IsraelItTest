using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraelTest.Models
{
    public class AuthorResponseModel
    {
        public IEnumerable<Author> authors { get; set; }
        public int Page { get; set; }
    }
}