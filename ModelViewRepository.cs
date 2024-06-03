using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace myproject.Models
{
    public class ModelViewRepository
    {
        private readonly string _connectionString;

        public ModelViewRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Book GetBookByGenre(string genre)
        {
            Book book = null;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT 
                                    b.Title, 
                                    b.Author, 
                                    b.Genre, 
                                    b.Description, 
                                    b.CoverImageUrl, 
                                    b.ISBN, 
                                    b.Publisher, 
                                    b.Language, 
                                    b.BookExcerpts, 
                                    b.AboutAuthor 
                                 FROM 
                                    Book b
                                 INNER JOIN 
                                    GenreTypes g ON b.Genre = g.Genre
                                 WHERE 
                                    b.Genre = @Genre";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Genre", genre);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                book = new Book
                                {
                                    Title = reader["Title"].ToString(),
                                    Author = reader["Author"].ToString(),
                                    Genre = reader["Genre"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    CoverImageUrl = reader["CoverImageUrl"].ToString(),
                                    ISBN = reader["ISBN"].ToString(),
                                    Publisher = reader["Publisher"].ToString(),
                                    Language = reader["Language"].ToString(),
                                    BookExcerpts = reader["BookExcerpts"].ToString(),
                                    AboutAuthor = reader["AboutAuthor"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    Console.WriteLine("Inner Exception: " + innerException.Message);
                    innerException = innerException.InnerException;
                }
                throw; // Rethrow the exception to propagate it further if needed
            }

            return book;
        }

        public List<GenreType> GetAllGenresWithBooks()
        {
            List<GenreType> genres = new List<GenreType>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT 
                                    g.GenreId, 
                                    g.Genre, 
                                    g.Description,
                                    b.Title, 
                                    b.Author, 
                                    b.Genre AS BookGenre, 
                                    b.Description AS BookDescription, 
                                    b.CoverImageUrl, 
                                    b.ISBN, 
                                    b.Publisher, 
                                    b.Language, 
                                    b.BookExcerpts, 
                                    b.AboutAuthor
                                 FROM 
                                    GenreTypes g
                                 LEFT JOIN 
                                    Book b ON g.Genre = b.Genre
                                 ORDER BY 
                                    g.GenreId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            Dictionary<int, GenreType> genreDict = new Dictionary<int, GenreType>();

                            while (reader.Read())
                            {
                                int genreId = Convert.ToInt32(reader["GenreId"]);
                                if (!genreDict.ContainsKey(genreId))
                                {
                                    genreDict[genreId] = new GenreType
                                    {
                                        GenreId = genreId,
                                        Genre = reader["Genre"].ToString(),
                                        Description = reader["Description"].ToString(),
                                        Books = new List<Book>()
                                    };
                                }

                                if (!reader.IsDBNull(reader.GetOrdinal("Title")))
                                {
                                    Book book = new Book
                                    {
                                        Title = reader["Title"].ToString(),
                                        Author = reader["Author"].ToString(),
                                        Genre = reader["BookGenre"].ToString(),
                                        Description = reader["BookDescription"].ToString(),
                                        CoverImageUrl = reader["CoverImageUrl"].ToString(),
                                        ISBN = reader["ISBN"].ToString(),
                                        Publisher = reader["Publisher"].ToString(),
                                        Language = reader["Language"].ToString(),
                                        BookExcerpts = reader["BookExcerpts"].ToString(),
                                        AboutAuthor = reader["AboutAuthor"].ToString()
                                    };

                                    genreDict[genreId].Books.Add(book);
                                }
                            }

                            genres.AddRange(genreDict.Values);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    Console.WriteLine("Inner Exception: " + innerException.Message);
                    innerException = innerException.InnerException;
                }
                throw; // Rethrow the exception to propagate it further if needed
            }

            return genres;
        }
    }
}


