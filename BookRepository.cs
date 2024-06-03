//using System;
//using System.Collections.Generic;
//using MySql.Data.MySqlClient;

//namespace myproject.Models
//{
//    public class BookRepository
//    {
//        string connectionString = "server=localhost;user=root;password=Allahis1!;database=dataa";

//        public void AddBook(Book book)
//        {
//            try
//            {
//                using (MySqlConnection connection = new MySqlConnection(connectionString))
//                {
//                    connection.Open();
//                    string query = "INSERT INTO Book (Title, Author, Genre, Description, CoverImageUrl, ISBN, Publisher, Language, BookExcerpts, AboutAuthor) " +
//                                   "VALUES (@Title, @Author, @Genre, @Description, @CoverImageUrl, @ISBN, @Publisher, @Language, @BookExcerpts, @AboutAuthor)";

//                    using (MySqlCommand command = new MySqlCommand(query, connection))
//                    {
//                        command.Parameters.AddWithValue("@Title", book.Title);
//                        command.Parameters.AddWithValue("@Author", book.Author);
//                        command.Parameters.AddWithValue("@Genre", book.Genre);
//                        command.Parameters.AddWithValue("@Description", book.Description);
//                        command.Parameters.AddWithValue("@CoverImageUrl", book.CoverImageUrl);
//                        command.Parameters.AddWithValue("@ISBN", book.ISBN);
//                        command.Parameters.AddWithValue("@Publisher", book.Publisher);
//                        command.Parameters.AddWithValue("@Language", book.Language);
//                        command.Parameters.AddWithValue("@BookExcerpts", book.BookExcerpts);
//                        command.Parameters.AddWithValue("@AboutAuthor", book.AboutAuthor);
//                        command.ExecuteNonQuery();

//                        //Console.WriteLine("great!!");
//                        Console.WriteLine("Executing query: " + command.CommandText);

//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                // Log or display the error message
//                Console.WriteLine("Error occurred: " + ex.Message);
//            }
//        }

//        public List<Book> GetBooks()
//        {
//            List<Book> bookList = new List<Book>();

//            try
//            {
//                using (MySqlConnection connection = new MySqlConnection(connectionString))
//                {
//                    string query = "SELECT Title, Author, Genre, Description, CoverImageUrl, ISBN, Publisher, Language, BookExcerpts, AboutAuthor FROM Book";

//                    using (MySqlCommand command = new MySqlCommand(query, connection))
//                    {
//                        connection.Open();
//                        using (MySqlDataReader reader = command.ExecuteReader())
//                        {
//                            while (reader.Read())
//                            {
//                                Book book = new Book
//                                {
//                                    Title = reader["Title"].ToString(),
//                                    Author = reader["Author"].ToString(),
//                                    Genre = reader["Genre"].ToString(),
//                                    Description = reader["Description"].ToString(),
//                                    CoverImageUrl = reader["CoverImageUrl"].ToString(),
//                                    ISBN = reader["ISBN"].ToString(),
//                                    Publisher = reader["Publisher"].ToString(),
//                                    Language = reader["Language"].ToString(),
//                                    BookExcerpts = reader["BookExcerpts"].ToString(),
//                                    AboutAuthor = reader["AboutAuthor"].ToString()
//                                };
//                                bookList.Add(book);
//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                // Log or display the error message
//                Console.WriteLine("Error occurred: " + ex.Message);
//            }

//            return bookList;
//        }

//        public Book GetBookByTitleAndAuthor(string title, string author)
//        {
//            Book book = null;

//            try
//            {
//                using (MySqlConnection connection = new MySqlConnection(connectionString))
//                {
//                    string query = "SELECT Title, Author, Genre, Description, CoverImageUrl, ISBN, Publisher, Language, BookExcerpts, AboutAuthor " +
//                                   "FROM Book WHERE Title = @Title AND Author = @Author";

//                    using (MySqlCommand command = new MySqlCommand(query, connection))
//                    {
//                        command.Parameters.AddWithValue("@Title", title);
//                        command.Parameters.AddWithValue("@Author", author);
//                        connection.Open();
//                        using (MySqlDataReader reader = command.ExecuteReader())
//                        {
//                            if (reader.Read())
//                            {
//                                book = new Book
//                                {
//                                    Title = reader["Title"].ToString(),
//                                    Author = reader["Author"].ToString(),
//                                    Genre = reader["Genre"].ToString(),
//                                    Description = reader["Description"].ToString(),
//                                    CoverImageUrl = reader["CoverImageUrl"].ToString(),
//                                    ISBN = reader["ISBN"].ToString(),
//                                    Publisher = reader["Publisher"].ToString(),
//                                    Language = reader["Language"].ToString(),
//                                    BookExcerpts = reader["BookExcerpts"].ToString(),
//                                    AboutAuthor = reader["AboutAuthor"].ToString()
//                                };
//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                // Log or display the error message
//                Console.WriteLine("Error occurred: " + ex.Message);
//            }

//            return book;
//        }

//        public void UpdateBook(Book book)
//        {
//            try
//            {
//                using (MySqlConnection connection = new MySqlConnection(connectionString))
//                {
//                    connection.Open();

//                    // Check if the book with the provided title and author exists
//                    string checkQuery = "SELECT COUNT(*) FROM Book WHERE Title = @Title AND Author = @Author";
//                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
//                    {
//                        checkCommand.Parameters.AddWithValue("@Title", book.Title);
//                        checkCommand.Parameters.AddWithValue("@Author", book.Author);

//                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

//                        if (count == 0)
//                        {
//                            Console.WriteLine("Error: Book with the provided title and author does not exist.");
//                            return; // Exit the method if the book doesn't exist
//                        }
//                    }

//                    // Check if the combination of title and author remains unique after the update
//                    string duplicateCheckQuery = "SELECT COUNT(*) FROM Book WHERE Title = @Title AND Author = @Author AND (Title <> @OldTitle OR Author <> @OldAuthor)";
//                    using (MySqlCommand duplicateCheckCommand = new MySqlCommand(duplicateCheckQuery, connection))
//                    {
//                        duplicateCheckCommand.Parameters.AddWithValue("@Title", book.Title);
//                        duplicateCheckCommand.Parameters.AddWithValue("@Author", book.Author);
//                        duplicateCheckCommand.Parameters.AddWithValue("@OldTitle", book.Title); // The old title before update
//                        duplicateCheckCommand.Parameters.AddWithValue("@OldAuthor", book.Author); // The old author before update

//                        int duplicateCount = Convert.ToInt32(duplicateCheckCommand.ExecuteScalar());

//                        if (duplicateCount > 0)
//                        {
//                            Console.WriteLine("Error: Updating this book would result in a duplicate entry. Please ensure the combination of title and author remains unique.");
//                            return; // Exit the method if the update would result in a duplicate entry
//                        }
//                    }

//                    // Perform the update if no issues were found
//                    string updateQuery = "UPDATE Book SET Genre = @Genre, Description = @Description, CoverImageUrl = @CoverImageUrl, ISBN = @ISBN, " +
//                                         "Publisher = @Publisher, Language = @Language, BookExcerpts = @BookExcerpts, AboutAuthor = @AboutAuthor " +
//                                         "WHERE Title = @Title AND Author = @Author";

//                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
//                    {
//                        command.Parameters.AddWithValue("@Title", book.Title);
//                        command.Parameters.AddWithValue("@Author", book.Author);
//                        command.Parameters.AddWithValue("@Genre", book.Genre);
//                        command.Parameters.AddWithValue("@Description", book.Description);
//                        command.Parameters.AddWithValue("@CoverImageUrl", book.CoverImageUrl);
//                        command.Parameters.AddWithValue("@ISBN", book.ISBN);
//                        command.Parameters.AddWithValue("@Publisher", book.Publisher);
//                        command.Parameters.AddWithValue("@Language", book.Language);
//                        command.Parameters.AddWithValue("@BookExcerpts", book.BookExcerpts);
//                        command.Parameters.AddWithValue("@AboutAuthor", book.AboutAuthor);

//                        command.ExecuteNonQuery();
//                    }

//                    Console.WriteLine("Book updated successfully.");
//                }
//            }
//            catch (Exception ex)
//            {
//                // Log or display the error message
//                Console.WriteLine("Error occurred: " + ex.Message);
//            }
//        }


//        public void DeleteBook(string title, string author)
//        {
//            try
//            {
//                // Check if the book exists before attempting to delete it
//                if (!BookExists(title, author))
//                {
//                    Console.WriteLine("Error: Book with the provided title and author does not exist.");
//                    return;
//                }

//                using (MySqlConnection connection = new MySqlConnection(connectionString))
//                {
//                    string query = "DELETE FROM Book WHERE Title = @Title AND Author = @Author";

//                    using (MySqlCommand command = new MySqlCommand(query, connection))
//                    {
//                        connection.Open();

//                        command.Parameters.AddWithValue("@Title", title);
//                        command.Parameters.AddWithValue("@Author", author);
//                        command.ExecuteNonQuery();
//                    }

//                    Console.WriteLine("Book deleted successfully.");
//                }
//            }
//            catch (Exception ex)
//            {
//                // Log or display the error message
//                Console.WriteLine("Error occurred: " + ex.Message);
//            }
//        }

//        // Helper method to check if the book exists
//        private bool BookExists(string title, string author)
//        {
//            using (MySqlConnection connection = new MySqlConnection(connectionString))
//            {
//                string query = "SELECT COUNT(*) FROM Book WHERE Title = @Title AND Author = @Author";
//                using (MySqlCommand command = new MySqlCommand(query, connection))
//                {
//                    connection.Open();

//                    command.Parameters.AddWithValue("@Title", title);
//                    command.Parameters.AddWithValue("@Author", author);
//                    int count = Convert.ToInt32(command.ExecuteScalar());
//                    return count > 0;
//                }
//            }
//        }

//    }
//}
