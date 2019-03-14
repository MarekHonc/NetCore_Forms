using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NetCore_Forms.Code;
using NetCore_Forms.Data;
using NetCore_Forms.Entities;

namespace NetCore_Forms.Models
{
	/// <summary>
	/// Kreační viewmodel pro nové auto.
	/// </summary>
	public class CarCreatorViewModel : IValidatableObject
	{
		public CarCreatorViewModel()
		{
			this.ReleaseDate = DateTime.Today;
		}

		/// <summary>
		/// Název
		/// </summary>
		[Required]
		[DisplayName("Název")]
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Uvedení do provozu
		/// </summary>
		[Required]
		[DisplayName("Datum vydání")]
		public DateTime ReleaseDate
		{
			get;
			set;
		}

		/// <summary>
		/// Typ auto
		/// </summary>
		[Required]
		[DisplayName("Typ auta")]
		public CarType Type
		{
			get;
			set;
		}

		/// <summary>
		/// Výkon
		/// </summary>
		[Required]
		[MinValue(1)]
		[DisplayName("Výkon")]
		public int Power
		{
			get;
			set;
		}

		/// <summary>
		/// Značka
		/// </summary>
		[Required]
		[DisplayName("Značka")]
		public int BrandId
		{
			get;
			set;
		}

		/// <summary>
		/// Dictionary pro zobrazení dropdownu na výběr značky.
		/// </summary>
		public Dictionary<int, string> Brands
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací novou entitu <see cref="Car"/> a navrátí ji.
		/// Tuto entitu lze již přidat do databáze.
		/// </summary>
		/// <returns></returns>
		public Car CreateEntity()
		{
			return Car.Create(this.Name, this.ReleaseDate, this.Type, this.Power, this.BrandId);
		}

		/// <summary>
		/// Dodatečná validace modelu.
		/// </summary>
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var context = (EntitiesContext)validationContext.GetService(typeof(EntitiesContext));

			var brand = context.Brands.FirstOrDefault(b => b.Id == this.BrandId);

			if (brand == null)
				yield return new ValidationResult("Nehraj si se stránkou", new string[] { nameof(this.BrandId)});

			if (brand != null && brand.FoundedAt > this.ReleaseDate)
				yield return new ValidationResult("Auto nemůže vzniknout před značkou", new string[] { nameof(this.ReleaseDate)});
		}
	}
}
