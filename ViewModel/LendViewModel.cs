using System.Collections.Generic;
using LibraryManagement.Data.Model;

namespace LibraryManagement.ViewModel
{
    public class LendViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
    }
}