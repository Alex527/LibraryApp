using System.Linq;
using LibraryManagement.Data.Interfaces;
using LibraryManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class LendController : Controller
    {
        private readonly IBookRepository _bookrepository;
        private readonly ICustomerRepository _customerRepository;

        public LendController(IBookRepository bookRepository, ICustomerRepository customerRepository)
        {
            _bookrepository = bookRepository;
            _customerRepository = customerRepository;
        }

        [Route("Lend")]
        public IActionResult List()
        {
            //load all available books
            var availableBooks = _bookrepository.FindWithAuthor(x => x.BorrowerId == 0);
            //check collection
            if(availableBooks.Count() == 0)
            {
                return View("Empty");
            }
            else 
            {
                return View(availableBooks);
            }
        }

        public IActionResult LendBook(int bookId)
        {
            //load current book and all customers
            var lendViewModel = new LendViewModel()
            {
                Book = _bookrepository.GetById(bookId),
                Customers = _customerRepository.GetAll()
            };
            //send data to the lend view
            return View(lendViewModel);
        }

        [HttpPost]
        public IActionResult LendBook(LendViewModel lendViewModel)
        {
            //update the database
            var book = _bookrepository.GetById(lendViewModel.Book.BookId);
            var customer = _customerRepository.GetById(lendViewModel.Book.BorrowerId);

            book.Borrower = customer;
            book.BorrowerId = customer.CustomerId;
            _bookrepository.Update(book);
            
            return RedirectToAction("List");
            //return View("List");
        }
    }
}