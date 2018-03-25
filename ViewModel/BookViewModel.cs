using System.Collections;
using System.Collections.Generic;
using LibraryManagement.Data.Model;

namespace LibraryManagement.ViewModel
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Author> Authors { get; set; }
    }
}