using System;
using Microsoft.EntityFrameworkCore;
using myproject.Data;

namespace myproject.Models
{
    public class GenreType
    {
        public int GenreId { get; set; } // Primary key
        public string? Genre { get; set; } // Primary key
        public string? Description { get; set; }

        public List<Book> Books { get; set; }

        public GenreType()
        {
            Books = new List<Book>();
        }
    }
}

