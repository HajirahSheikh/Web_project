using System;
namespace myproject.Models
{
	public class Reviews
	{
        public int ReviewId { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? UserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime DatePosted { get; set; } = DateTime.Now;


        // Navigation properties (commented out for now)
        // public Book Book { get; set; }
        // public MyAppUserRegister User { get; set; }
    }
}

