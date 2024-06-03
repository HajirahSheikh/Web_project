using Microsoft.AspNetCore.Mvc;
using myproject.Models;

namespace myproject.Controllers
{
    public class BooksController : Controller
    {
        private readonly ModelViewRepository _repository;

        public BooksController()
        {
            // Initialize your repository with the connection string
            _repository = new ModelViewRepository("server=localhost;user=root;password=Allahis1!;database=dataa");
        }

        //public IActionResult ByGenre(string genre)
        //{
        //    // Fetch the data from the repository
        //    //ModelView model = _repository.GetModelView();
        //    //// Pass the data to the view
        //    //return View(model);
        //    // Fetch books by genre from the repository
        //    //List<Book> books = _repository.GetBooksByGenre(genre);

        //    //// Pass the data to the view
        //    //return View(books);
        //}
    }
}
