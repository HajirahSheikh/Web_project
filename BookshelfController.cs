using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC_FOR_PROJECT.Controllers
{
    public class BookshelfController : Controller
    {
        public ViewResult Bookdetails()
        {
            return View();
        }
        [Authorize(Policy = "RequireAuthenticatedUser")]
        public ViewResult Bookshelves()
        {
            return View();
        }
        
    }
}