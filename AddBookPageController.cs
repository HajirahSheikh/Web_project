using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


[Authorize(Policy="AdminPolicy")]

public class AddBookPage : Controller
{
    private readonly string connectionString= "server=localhost;user=root;password=Allahis1!;database=dataa";
    //private BookRepository repository = new BookRepository();
    private readonly IWebHostEnvironment Env;
    private readonly ILogger<AddBookPage> _logger;

    public AddBookPage(ILogger<AddBookPage> logger, IWebHostEnvironment en)
    {
        _logger = logger;
        Env = en;
    }

    [HttpGet]
    public ActionResult AddBook()
    {

        return View();
    }

    [HttpPost]
    public IActionResult AddBookData(string title, string author, string genre, string description,
                                 string coverImageUrl, IFormFile ImageFile, string isbn,
                                 string publisher, string language, string bookExcerpts,
                                 string aboutAuthor)
    {
        if (ImageFile == null || ImageFile.Length == 0)
        {
            // Handle the case where no file is uploaded or file is empty
            ModelState.AddModelError("ImageFile", "Please upload a valid image file.");
            return View();
        }

        // Get the dynamic path of the wwwRoot first
        string wwwRoot = Env.WebRootPath;
        string path = Path.Combine(wwwRoot, "UploadedFiles"); // Add the path till UploadedFiles dynamically

        // If directory does not exist, then create it
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        // Get the file name
        string fileName = Path.GetFileName(ImageFile.FileName);
        // Combine the path till the image
        string Comp_path = Path.Combine(path, fileName);

        // Save the file
        using (var stream = new FileStream(Comp_path, FileMode.Create))
        {
            ImageFile.CopyTo(stream);
        }

        // Generate the relative path for the image
        int index = Comp_path.IndexOf("UploadedFiles");
        Comp_path = Comp_path.Substring(index);
        Comp_path = Comp_path.Replace('\\', '/');
        Comp_path = "/" + Comp_path;

        // Create the book object
        Book book = new Book
        {
            Title = title,
            Author = author,
            Genre = genre,
            Description = description,
            CoverImageUrl = Comp_path,
            ISBN = isbn,
            Publisher = publisher,
            Language = language,
            BookExcerpts = bookExcerpts,
            AboutAuthor = aboutAuthor
        };
        IRepository<Book> repository = new GenericRepository<Book>(connectionString);
        // Save the book data to the database
        repository.Add(book);

        // Optionally, return a success message or redirect to another page
        ViewBag.SuccessMessage = "Book added successfully.";
        return View();
    }


    [HttpGet]
    public ActionResult UpdateBook(string title, string author)
    {
        return View();
    }

    [HttpPost]
    public ActionResult UpdateBookData(string title, string author, string genre, string description,
                                       string coverImageUrl, string isbn, string publisher, string language,
                                       string bookExcerpts, string aboutAuthor)
    {
        IRepository<Book> repository = new GenericRepository<Book>(connectionString);

        var book = repository.GetBookByTitleAndAuthor(title, author);
        if (book == null)
        {
            return NotFound();
        }

        book.Genre = genre;
        book.Description = description;
        book.CoverImageUrl = coverImageUrl;
        book.ISBN = isbn;
        book.Publisher = publisher;
        book.Language = language;
        book.BookExcerpts = bookExcerpts;
        book.AboutAuthor = aboutAuthor;


        repository.Update(book);
        // Optionally, you can pass a success message to the view
        ViewBag.SuccessMessage = "Book Updated successfully.";

        return View();
    }

    [HttpGet]
    public ActionResult DeleteBook(string title, string author)
    {
        return View();
    }

    [HttpPost]
    public ActionResult DeleteBookData(string title, string author)
    {
        IRepository<Book> repository = new GenericRepository<Book>(connectionString);

        var book = repository.GetBookByTitleAndAuthor(title, author);
        if (book == null)
        {
            return NotFound();
        }

        repository.Delete(title, author);

        // Optionally, you can pass a success message to the view
        ViewBag.SuccessMessage = "Book deleted successfully.";

        return View();
    }


    [HttpGet]
    public ActionResult BookList()
    {
        IRepository<Book> repository = new GenericRepository<Book>(connectionString);

        var books = repository.GetBooks();
        return View(books);
    }

    //public ActionResult ShowViewModel()
    //{

    //    return View();
    //}
}
