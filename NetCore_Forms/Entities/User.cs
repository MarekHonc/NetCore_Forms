using Microsoft.AspNetCore.Identity;

namespace NetCore_Forms.Entities
{
	/// <summary>
	/// Rozšíření základního uživatele (to samé lze udělat i s rolí, stačí zdědit IdentityRole a zaregistrovat v startUpu + EntitiesContextu)
	/// </summary>
	public class User : IdentityUser
	{
		public string FullName
		{
			get;
			set;
		}

		public string Gender
		{
			get;
			set;
		}
	}
}
