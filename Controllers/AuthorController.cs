using System;
using System.Linq;
using LibraryManagement.Data.Interfaces;
using LibraryManagement.Data.Model;
using LibraryManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController (IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [Route("Author")]
        public IActionResult List()
        {
            var authors = _authorRepository.GetAllWithBooks();

            if(authors.Count() == 0)
                return View("Empty");

            return View(authors);
        }

        public IActionResult Update(int id)
        {
            var author = _authorRepository.GetById(id);
            if(author == null) 
                return NotFound();
            
            return View(author);
        }

        [HttpPost]
        public IActionResult Update (Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }
            _authorRepository.Update(author);
            return RedirectToAction("List");
        }

        public IActionResult Create()
        {
            var authorViewModel = new CreateAuthorViewModel()
            {
                Referer = Request.Headers["Referer"].ToString()
            };
            return View(authorViewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateAuthorViewModel authorViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(authorViewModel);
            }

            _authorRepository.Create(authorViewModel.Author);

            if(!String.IsNullOrEmpty(authorViewModel.Referer))
            {
                return Redirect(authorViewModel.Referer);
            }
            return RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            var author = _authorRepository.GetById(id);
            _authorRepository.Delete(author);
            return RedirectToAction("List");
        }
    }
}