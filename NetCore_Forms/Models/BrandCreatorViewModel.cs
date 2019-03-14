using NetCore_Forms.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetCore_Forms.Models
{
	/// <summary>
	/// View model používaný pro vytvoření značky.
	/// </summary>
	public class BrandCreatorViewModel
	{
		/// <summary>
		/// Prázdný konstruktor kvůli model binderu
		/// </summary>
		public BrandCreatorViewModel()
		{
			this.FoundedAt = DateTime.Today;
		}

		/// <summary>
		/// Název značky
		/// </summary>
		[Required]
		[DisplayName("Název")]
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Datum založení značky
		/// </summary>
		[Required]
		[DisplayName("Založeno")]
		public DateTime FoundedAt
		{
			get;
			set;
		}

		/// <summary>
		/// Kontaktní email (asi na tu značku ne ?)
		/// </summary>
		[EmailAddress]
		[DisplayName("Kontaktní email")]
		public string ContactEmail
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací novou entitu <see cref="Brand"/>.
		/// Tato entita lze následně přidat do databáze.
		/// </summary>
		public Brand CreateEntity()
		{
			var brand = Brand.Create(this.Name, this.FoundedAt, this.ContactEmail);

			return brand;
		}
	}
}
