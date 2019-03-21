using Microsoft.AspNetCore.Identity;

namespace NetCore_Forms.Entities
{
	/// <summary>
	/// Rozšíření základního uživatele
	/// </summary>
	public class User : IdentityUser
	{
		public string FullName
		{
			get;
			set;
		}
	}
}
