using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Data.Interfaces;
using LibraryManagement.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data.Repository
{
    public class AuthorRepository: Repository<Author>, IAuthorRepository
    {
        public AuthorRepository (LibraryDbContext context) : base(context)
        {
            
        }

        public IEnumerable<Author> GetAllWithBooks()
        {
            return context.Authors.Include(a => a.Books);
        }
        
        public Author GetWithBooks(int id)
        {
              return context.Authors
                                    .Where(a => a.AuthorId == id)
                                    .Include(a => a.Books)
                                    .FirstOrDefault();
        }
    }
}