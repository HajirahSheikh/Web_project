using System;

namespace myproject.Models
{
    public class Book
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; } // Foreign key
        public string? Description { get; set; }
        public string? CoverImageUrl { get; set; }
        public string? ISBN { get; set; }
        public string? Publisher { get; set; }
        public string? Language { get; set; }
        public string? BookExcerpts { get; set; }
        public string? AboutAuthor { get; set; }
    }

}


