using System;
using System.ComponentModel.DataAnnotations;
namespace myproject.Models

{
	public class MyAppUserRegister : Microsoft.AspNetCore.Identity.IdentityUser
	{
        [Required]
		public string? Name { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        public string City { get; set; }

    }
}

  