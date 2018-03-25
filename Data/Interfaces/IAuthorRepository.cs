using System.Collections.Generic;
using LibraryManagement.Data.Model;

namespace LibraryManagement.Data.Interfaces
{
    public interface IAuthorRepository: IRepository<Author>
    {
         IEnumerable<Author> GetAllWithBooks();

         Author GetWithBooks(int id);
    }
}