using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace myproject.Models
{
    public class GenreTypeRepository
    {
        string connectionString = "server=localhost;user=root;password=Allahis1!;database=dataa";

        public void AddGenreType(GenreType genreType)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO GenreTypes (Genre,Description, GenreId) VALUES (@Genre, @Description, @GenreId)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Genre", genreType.Genre);
                        command.Parameters.AddWithValue("@Description", genreType.Description);
                        command.Parameters.AddWithValue("@GenreId", genreType.GenreId);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }
        }


        public List<GenreType> GetGenreTypes()
        {
            List<GenreType> genreTypeList = new List<GenreType>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT GenreId, Genre, Description FROM GenreType";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GenreType genreType = new GenreType
                                {
                                    GenreId = Convert.ToInt32(reader["GenreId"]),
                                    Genre = reader["Genre"].ToString(),
                                    Description = reader["Description"].ToString()
                                };
                                genreTypeList.Add(genreType);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }

            return genreTypeList;
        }

        public GenreType GetGenreTypeByName(string genre)
        {
            GenreType genreType = null;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT GenreId, Genre, Description FROM GenreType WHERE Genre = @Genre";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Genre", genre);
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                genreType = new GenreType
                                {
                                    GenreId = Convert.ToInt32(reader["GenreId"]),
                                    Genre = reader["Genre"].ToString(),
                                    Description = reader["Description"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }

            return genreType;
        }

        public void UpdateGenreType(GenreType genreType)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if the genre exists before attempting to update it
                    if (!GenreExists(genreType.GenreId, genreType.Genre))
                    {
                        Console.WriteLine("Error: Genre with the provided ID and name does not exist.");
                        return;
                    }

                    string query = "UPDATE GenreType SET Genre = @Genre, Description = @Description WHERE GenreId = @GenreId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Genre", genreType.Genre);
                        command.Parameters.AddWithValue("@Description", genreType.Description);
                        command.Parameters.AddWithValue("@GenreId", genreType.GenreId);

                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("GenreType updated successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }
        }

        // Helper method to check if the genre exists
        private bool GenreExists(int genreId, string genreName)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM GenreType WHERE GenreId = @GenreId AND Genre = @Genre";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@GenreId", genreId);
                    command.Parameters.AddWithValue("@Genre", genreName);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }


        public void DeleteGenreType(int genreId, string genreName)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if the genre exists before attempting to delete it
                    if (!GenreExists(genreId, genreName))
                    {
                        Console.WriteLine("Error: Genre does not exist in the database.");
                        return;
                    }

                    string query = "DELETE FROM GenreType WHERE GenreId = @GenreId AND Genre = @Genre";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@GenreId", genreId);
                        command.Parameters.AddWithValue("@Genre", genreName);
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("GenreType deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }
        }

    }
}
