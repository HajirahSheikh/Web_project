
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using myproject.Models;

namespace MVC_FOR_PROJECT.Controllers
{
    public class LibraryController : Controller
    {
        private readonly UserManager<myproject.Models.MyAppUserRegister> _userManager;

        private readonly ModelViewRepository _repository;

        public LibraryController(UserManager<myproject.Models.MyAppUserRegister> userManager, ModelViewRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        //public LibraryController(ModelViewRepository repository)
        //{
        //    _repository = repository;
        //}


        [Authorize(Policy = "RequireAuthenticatedUser")]
        public async Task<IActionResult> Lib()
        {
            var user = await _userManager.GetUserAsync(User);
            var userEmail = user?.Email;
            var colorPreference = Request.Cookies["ColorPreference"];

            ViewData["UserEmail"] = userEmail;
            ViewData["ColorPreference"] = colorPreference;

            return View();
            //var userEmail = Request.Cookies["UserEmail"];
            //var colorPreference = Request.Cookies["ColorPreference"];

            //ViewData["UserEmail"] = userEmail;
            //ViewData["ColorPreference"] = colorPreference;

            //return View();
        }

        [HttpPost]
        public IActionResult SetColorPreference(string colorPreference)
        {
            //var userEmail = Request.Cookies["UserEmail"];

            //if (userEmail == null)
            //{
            //    // Handle the case where userEmail is null
            //    return BadRequest("User email is required.");
            //}

            //HttpContext.Response.Cookies.Append("UserEmail", userEmail, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) });
            //HttpContext.Response.Cookies.Append("ColorPreference", colorPreference, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(365) });

            //return RedirectToAction("Lib");

            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("User email is required.");
            }

            HttpContext.Response.Cookies.Append("UserEmail", userEmail, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) });
            HttpContext.Response.Cookies.Append("ColorPreference", colorPreference, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(365) });

            return RedirectToAction("Lib");
        }

        public ViewResult Categories()
        {
            //var colorPreference = Request.Cookies["ColorPreference"];
            //ViewData["ColorPreference"] = colorPreference;
            //return View();
            // Fetch the data from the repository
            var genres = _repository.GetAllGenresWithBooks();
            return View(genres);
        }

        public IActionResult BookDetails(string genre)
        {
            // Fetch the book details from the repository
            //var book = _repository.GetBookByGenre(genre);
            //return View(book);

            // Fetch the book details from the repository
            var book = _repository.GetBookByGenre(genre);

            // Check if the book is found
            if (book != null)
            {
                return View(book);
            }
            else
            {
                // If no book is found, you can redirect the user to a different page
                // or display a custom error message
                return RedirectToAction("Index", "Home"); // Redirect to the home page
                                                          // Or return a view with a custom error message
                                                          // ViewBag.ErrorMessage = "No book found for the specified genre.";
                                                          // return View("Error");
            }
        }

    }
}