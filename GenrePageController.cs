using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myproject.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

public class GenrePage : Controller
{
    private GenreTypeRepository repository = new GenreTypeRepository();
    private readonly string connectionString = "server=localhost;user=root;password=Allahis1!;database=dataa";

    [HttpGet]
    public ActionResult AddGenre()
    {
        return View();
    }

    [HttpPost]
    public ActionResult AddGenreData(string genreName, string genreDescription, int genreId)
    {
        Console.WriteLine(genreName);
        Console.WriteLine(genreDescription);
        Console.WriteLine(genreId);
        // Create a new GenreType object with the provided values
        GenreType genreType = new GenreType
        {
            Genre = genreName,
            Description = genreDescription,
            GenreId = genreId
        };
        //IRepository<GenreType> repository = new GenericRepository<GenreType>(connectionString);
        // Save the book data to the database
        //repository.Add(genreType);
        // Call the repository method to add the genre type to the database
        repository.AddGenreType(genreType);

        // Optionally, you can add a success message to the ViewBag
        ViewBag.SuccessMessage = "Genre added successfully.";

        // Redirect to the appropriate view, for example, the genre list page
        return View();
    }



    [HttpGet]
    public ActionResult UpdateGenre(string genre)
    {
        return View();
    }

    [HttpPost]
    public ActionResult UpdateGenreData(string genre, string description)
    {
        //IRepository<GenreType> repository = new GenericRepository<GenreType>(connectionString);

        var genreType = repository.GetGenreTypeByName(genre);
        if (genreType == null)
        {
            return NotFound();
        }

        genreType.Description = description;

        repository.UpdateGenreType(genreType);
        // Optionally, you can pass a success message to the view
        ViewBag.SuccessMessage = "Genre updated successfully.";

        return View();
    }

    [HttpGet]
    public ActionResult DeleteGenre(int genreId, string genreName)
    {
        return View();
    }

    [HttpPost]
    public ActionResult DeleteGenreData(int genreId, string genreName)
    {
        var genreType = repository.GetGenreTypeByName(genreName);
        if (genreType == null)
        {
            return NotFound();
        }

        repository.DeleteGenreType(genreId, genreName);

        // Optionally, you can pass a success message to the view
        ViewBag.SuccessMessage = "Genre deleted successfully.";

        return View();
    }



    [HttpGet]
    public ActionResult GenreList()
    {
        var genres = repository.GetGenreTypes();
        return View(genres);
    }
}