using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    internal class Author
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public List<Book> Books { get; set; }
    }
}
