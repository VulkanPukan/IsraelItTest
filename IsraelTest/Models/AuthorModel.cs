using System;
using System.Threading.Tasks;


namespace IsraelTest.Models
{
    public class AuthorModel
    {
        public int Id;
        public string Name { get; set; }
        public string DOB { get; set; }


        public Author GetAuthor()
        {
            return new Author() { Name = this.Name, DOB = DateTime.Parse(this.DOB) };
        }
    }
}
