using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using MySql.Data.MySqlClient;

namespace myproject.Models
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private readonly string connectionString;

        public GenericRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Add(TEntity entity)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    var tableName = typeof(TEntity).Name;
                    var properties = typeof(TEntity).GetProperties().Where(p => p.Name != "Id");

                    var columnNames = string.Join(",", properties.Select(p => p.Name));
                    var parameterNames = string.Join(",", properties.Select(p => "@" + p.Name));

                    var query = $"INSERT INTO {tableName} ({columnNames}) VALUES ({parameterNames})";

                    connection.Execute(query, entity);
                    Console.WriteLine("Book added successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }
        }


        public bool EntityExists(string title, string author)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    var tableName = typeof(TEntity).Name;
                    var query = $"SELECT COUNT(*) FROM {tableName} WHERE Title=@Title AND Author=@Author";

                    var count = connection.ExecuteScalar<int>(query, new { Title = title, Author = author });
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
                return false;
            }
        }


        public void Update(TEntity entity)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    var tableName = typeof(TEntity).Name;
                    var properties = typeof(TEntity).GetProperties().Where(p => p.Name != "Id");

                    var setClause = string.Join(",", properties.Select(p => $"{p.Name}=@{p.Name}"));
                    var query = $"UPDATE {tableName} SET {setClause} WHERE Title=@Title AND Author=@Author";

                    connection.Execute(query, entity);
                    Console.WriteLine("Book updated successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }
        }


        public List<TEntity> GetBooks()
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    var tableName = typeof(TEntity).Name;
                    var query = $"SELECT * FROM {tableName}";

                    var entities = connection.Query<TEntity>(query).ToList();
                    return entities;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
                return new List<TEntity>();
            }
        }


        public TEntity GetBookByTitleAndAuthor(string title, string author)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    var tableName = typeof(TEntity).Name;
                    var query = $"SELECT * FROM {tableName} WHERE Title=@Title AND Author=@Author";

                    var entity = connection.QuerySingleOrDefault<TEntity>(query, new { Title = title, Author = author });
                    return entity;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
                return null;
            }
        }


        public void Delete(string title, string author)
        {
            try
            {
                if (!EntityExists(title, author))
                {
                    Console.WriteLine("Error: Book with the provided title and author does not exist.");
                    return;
                }

                using (var connection = new MySqlConnection(connectionString))
                {
                    var tableName = typeof(TEntity).Name;
                    var query = $"DELETE FROM {tableName} WHERE Title=@Title AND Author=@Author";

                    connection.Execute(query, new { Title = title, Author = author });
                    Console.WriteLine("Book deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }
        }

        

 
    }
}

