using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace myproject.Models
{
	public class UserShelvesType
	{
        public class UserShelf
        {
            public int UserShelfId { get; set; }
            public string? UserId { get; set; }
            public string? Title { get; set; }
            public string? Author { get; set; }
            [Column(TypeName = "varchar(50)")]
            public ShelfType ShelfType { get; set; }
        }

        public enum ShelfType
        {
            CurrentlyReading,
            ToRead,
            Completed
        }
    }
}


