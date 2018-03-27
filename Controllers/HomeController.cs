using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Data.Interfaces;
using LibraryManagement.ViewModel;
using Microsoft.AspNetCore.Identity;
using LibraryManagement.Data.Authentification;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly UserManager<MyIdentityUser> _userManager;

        public HomeController(IBookRepository bookRepository,
                              IAuthorRepository authorRepository,
                              ICustomerRepository customerRepository,
                               UserManager<MyIdentityUser> userManager)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _customerRepository = customerRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            //create home view model and fill with data
            var homeViewModel = new HomeViewModel()
            {
                CustomerCount = _customerRepository.Count(x => true),
                AuthorCount = _authorRepository.Count(x => true),
                BookCount = _bookRepository.Count(x => true),
                LendBookCount = _bookRepository.Count(x => x.Borrower !=  null)
            };

            return View(homeViewModel);
        }

        [Authorize]
        public IActionResult Authorize()
        {
            MyIdentityUser user = _userManager.GetUserAsync
                                (HttpContext.User).Result;

            ViewBag.Message = $"Welcome {user.FullName}!";
            if(_userManager.IsInRoleAsync(user,"NormalUser").Result)
            {
                ViewBag.RoleMessage = "You are a NormalUser.";
            }
            return View();
        }
    }
}
