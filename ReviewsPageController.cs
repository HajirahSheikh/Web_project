using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using myproject.Models;

public class ReviewsPageController : Controller
{
    private ReviewRepository repository = new ReviewRepository();

    [HttpGet]
    public ActionResult AddReview()
    {
        return View();
    }

    [HttpPost]
    public ActionResult AddReviewData(string title, string author, string userId, int rating, string comment)
    {
        Reviews review = new Reviews
        {
            Title = title,
            Author = author,
            UserId = userId,
            Rating = rating,
            Comment = comment,
            DatePosted = DateTime.Now
        };

        repository.AddReview(review);

        // Optionally, you can pass a success message to the view
        ViewBag.SuccessMessage = "Review added successfully.";

        return View();
    }

    

    [HttpGet]
    public ActionResult DeleteReview(int reviewId)
    {
        //var review = repository.GetReviewById(reviewId);
        //if (review == null)
        //{
        //    return NotFound();
        //}

        return View();
    }

    [HttpPost]
    public ActionResult DeleteReviewData(int reviewId)
    {
        try
        {
            //var review = repository.GetReviewById(reviewId);

            //if (review == null)
            //{
            //    ViewBag.ErrorMessage = "Review not found.";
            //    return View();
            //}

            repository.DeleteReview(reviewId);
            ViewBag.SuccessMessage = "Review deleted successfully.";

            return View();
    }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = "An error occurred while deleting the review: " + ex.Message;
            return View();
}
    }




    [HttpGet]
    public ActionResult ReviewList()
    {
        var reviews = repository.GetReviews();
        return View(reviews);
    }
}
