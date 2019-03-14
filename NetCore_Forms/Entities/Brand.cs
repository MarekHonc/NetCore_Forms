using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetCore_Forms.Entities
{
	/// <summary>
	/// Třída reprezentující záznam značky auta
	/// </summary>
	/// <remarks>Vazby musí být virtual, kvůlu lazy loading z EF Core 2.1 ve 2.2 už by třeba být nemělo</remarks>
	public class Brand
	{
		public Brand()
		{
			this.Cars = new List<Car>();
		}

		/// <summary>
		/// Id značky
		/// </summary>
		[Key]
		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Název značky
		/// </summary>
		[Required]
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Datum založení značky
		/// </summary>
		[Required]
		public DateTime FoundedAt
		{
			get;
			set;
		}

		/// <summary>
		/// Kontaktní email (asi na tu značku ne ?)
		/// </summary>
		public string ContactEmail
		{
			get;
			set;
		}

		/// <summary>
		/// Tzv. navigation property, obsahuje všechny auta této značky
		/// data se stáhnou až při zavolání této vlastnosti (tzv. Lazy loading)
		/// lazy loading lze obejít pomocí .Include("název tabulky")
		/// </summary>
		public virtual ICollection<Car> Cars
		{
			get;
			set;
		}

		public static Brand Create(string name, DateTime foundedAt, string email)
		{
			var model = new Brand()
			{
				Name = name,
				FoundedAt = foundedAt,
				ContactEmail = email,
			};

			return model;
		}
	}
}
