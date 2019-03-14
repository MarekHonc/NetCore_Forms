using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NetCore_Forms.Data;
using NetCore_Forms.Entities;

namespace NetCore_Forms.Models
{
	/// <summary>
	/// View model pro editaci značky.
	/// </summary>
	public class BrandEditorViewModel: IValidatableObject
	{
		/// <summary>
		/// Prázdný konstruktor kvůli model binderu.
		/// </summary>
		public BrandEditorViewModel()
		{
		}

		/// <summary>
		/// Naplním model daty z entity, abych je mohl editovat.
		/// </summary>
		public BrandEditorViewModel(Brand brand)
		{
			this.Id = brand.Id;
			this.Name = brand.Name;
			this.FoundedAt = brand.FoundedAt;
			this.ContactEmail = brand.ContactEmail;
		}

		/// <summary>
		/// Id značky, používá se pouze při editaci.
		/// </summary>
		public int Id
		{
			get;
			set;
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
		/// Aktualizuje předanou entitu
		/// </summary>
		public void UpdateEntity(Brand brand)
		{

			brand.Name = this.Name;
			brand.FoundedAt = this.FoundedAt;
			brand.ContactEmail = this.ContactEmail;
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var context = (EntitiesContext)validationContext.GetService(typeof(EntitiesContext));

			var brand = context.Brands.FirstOrDefault(b => b.Id == this.Id);

			if (brand == null)
				yield return new ValidationResult("Nehraj si se stránkou", new string[] { nameof(Id) });

			if (brand != null && brand.Cars.Any(c => c.ReleaseDate < this.FoundedAt))
				yield return new ValidationResult("Značka nemůže vydat auto před založením", new string[] { nameof(FoundedAt) });
		}
	}
}
