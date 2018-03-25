using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Data.Interfaces;
using LibraryManagement.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository (LibraryDbContext context) : base(context)
        {
            
        }

        IEnumerable<Book> IBookRepository.FindWithAuthor(Func<Book, bool> predicate)
        {
            return context.Books
                                .Include(a => a.Author)
                                .Where(predicate);
        }

        IEnumerable<Book> IBookRepository.FindWithAuthorAndBorrower(Func<Book, bool> predicate)
        {
            return context.Books
                                .Include(a => a.Author)
                                .Include(a => a.Borrower)
                                .Where(predicate);
        }

        IEnumerable<Book> IBookRepository.GetAllWithAuthor()
        {
            return context.Books.Include(a => a.Author);
        }
    }
}