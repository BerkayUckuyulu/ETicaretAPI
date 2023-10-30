using System;
using Microsoft.AspNetCore.Identity;

namespace ETİcaretAPI.Domain
{
	public class AppUser:IdentityUser<string>
	{
		public string NameSurname { get; set; }
		public string? RefreshToken { get; set; }
		public DateTime? RefreshTokenEndDate { get; set; }

	}
}

