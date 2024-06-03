using System;

namespace myproject.Models
{
    public interface IRepository<TEntity>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        List<TEntity> GetBooks();
        TEntity GetBookByTitleAndAuthor(string title, string author);
        void Delete(string title, string author);
        bool EntityExists(string title, string author); // Define the method signature for checking entity existence
        
        
    }
}
