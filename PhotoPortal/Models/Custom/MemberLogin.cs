using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoPortal.Models.Custom
{
	public class MemberLogin
	{
		[Required]
		[MinLength(3)]
		public string Username { get; set; }
        [Required]
        [MinLength(3)]
        public string Password { get; set; }
	}
}

