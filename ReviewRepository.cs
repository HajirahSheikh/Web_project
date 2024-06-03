using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace myproject.Models
{
    public class ReviewRepository
    {
        string connectionString = "server=localhost;user=root;password=Allahis1!;database=dataa";

        public void AddReview(Reviews review)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Reviews (Title, Author, UserId, Rating, Comment, DatePosted) " +
                                   "VALUES (@Title, @Author, @UserId, @Rating, @Comment, @DatePosted)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", review.Title);
                        command.Parameters.AddWithValue("@Author", review.Author);
                        command.Parameters.AddWithValue("@UserId", review.UserId);
                        command.Parameters.AddWithValue("@Rating", review.Rating);
                        command.Parameters.AddWithValue("@Comment", review.Comment);
                        command.Parameters.AddWithValue("@DatePosted", review.DatePosted);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }
        }

        public List<Reviews> GetReviews()
        {
            List<Reviews> reviewList = new List<Reviews>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT ReviewId, Title, Author, UserId, Rating, Comment, DatePosted FROM Reviews";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Reviews review = new Reviews
                                {
                                    ReviewId = Convert.ToInt32(reader["ReviewId"]),
                                    Title = reader["Title"].ToString(),
                                    Author = reader["Author"].ToString(),
                                    UserId = reader["UserId"].ToString(),
                                    Rating = Convert.ToInt32(reader["Rating"]),
                                    Comment = reader["Comment"].ToString(),
                                    DatePosted = Convert.ToDateTime(reader["DatePosted"])
                                };
                                reviewList.Add(review);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }

            return reviewList;
        }



        //public Reviews GetReviewById(int reviewId)
        //{
        //    Reviews review = null;

        //    try
        //    {
        //        using (MySqlConnection connection = new MySqlConnection(connectionString))
        //        {
        //            string query = "SELECT ReviewId, Title, Author, UserId, Rating, Comment, DatePosted " +
        //                           "FROM Reviews WHERE ReviewId = @ReviewId";

        //            using (MySqlCommand command = new MySqlCommand(query, connection))
        //            {
        //                command.Parameters.AddWithValue("@ReviewId", reviewId);
        //                connection.Open();
        //                using (MySqlDataReader reader = command.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        review = new Reviews
        //                        {
        //                            ReviewId = Convert.ToInt32(reader["ReviewId"]),
        //                            Title = reader["Title"].ToString(),
        //                            Author = reader["Author"].ToString(),
        //                            UserId = reader["UserId"].ToString(),
        //                            Rating = Convert.ToInt32(reader["Rating"]),
        //                            Comment = reader["Comment"].ToString(),
        //                            DatePosted = Convert.ToDateTime(reader["DatePosted"])
        //                        };
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error occurred: " + ex.Message);
        //    }

        //    //return review;
        //}

        public void DeleteReview(int reviewId)
        {
            try
            {
                // Check if the review exists before attempting to delete it
                if (!ReviewExists(reviewId))
                {
                    Console.WriteLine("Error: Review with the provided ID does not exist.");
                    return;
                }

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "DELETE FROM Reviews WHERE ReviewId = @ReviewId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue("@ReviewId", reviewId);
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("Review deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }
        }

        // Helper method to check if the review exists
        private bool ReviewExists(int reviewId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Reviews WHERE ReviewId = @ReviewId";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@ReviewId", reviewId);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }
    }
}