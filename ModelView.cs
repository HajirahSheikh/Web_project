using System.Collections.Generic;

namespace myproject.Models
{
    public class ModelView
    {
        public List<GenreType> GenreCategories { get; set; }
        public List<Book> BookList { get; set; }

        public ModelView()
        {
            GenreCategories = new List<GenreType>();
            BookList = new List<Book>();
        }
    }
}


