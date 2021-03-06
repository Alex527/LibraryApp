using System.Linq;
using LibraryManagement.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class ReturnController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICustomerRepository _customerRepository;

        public ReturnController(IBookRepository bookRepository, ICustomerRepository customerRepository)
        {
            _bookRepository = bookRepository;
            _customerRepository = customerRepository;
        }

        [Route("Return")]
        public IActionResult List()
        {
            //load all borrowed books
            var borrowedBooks = _bookRepository.FindWithAuthorAndBorrower(x => x.BorrowerId != 0);
            //check the book collection
            if(borrowedBooks == null || borrowedBooks.Count() == 0)
            {
                return View("Empty");
            }

            return View(borrowedBooks);
        }

        public IActionResult ReturnBook(int bookId)
        {
            //load current book
            var book = _bookRepository.GetById(bookId);
            //remove borrower
            book.Borrower = null;
            book.BorrowerId = 0;
            //update database
            _bookRepository.Update(book);
            //redirect to list method
            return RedirectToAction("List");
        }
    }
}