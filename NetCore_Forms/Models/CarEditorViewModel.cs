using NetCore_Forms.Code;
using NetCore_Forms.Data;
using NetCore_Forms.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NetCore_Forms.Models
{
	/// <summary>
	/// View model pro editaci auta
	/// </summary>
	public class CarEditorViewModel : IValidatableObject
	{
		/// <summary>
		/// Konstruktor pro modelbinder
		/// </summary>
		public CarEditorViewModel()
		{
		}

		/// <summary>
		/// Konstruktor pro naplnění view modelu daty
		/// </summary>
		/// <param name="car"></param>
		public CarEditorViewModel(Car car)
		{
			this.Name = car.Name;
			this.Type = car.Type;
			this.ReleaseDate = car.ReleaseDate;
			this.Power = car.Power;
		}

		/// <summary>
		/// Id právě upravovaného auta
		/// </summary>
		public int Id
		{
			get;
			set;
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
		/// Aktualizuje předanou entitu
		/// </summary>
		internal void UpdateEntity(Car car)
		{
			car.Name = this.Name;
			car.ReleaseDate = this.ReleaseDate;
			car.Type = this.Type;
			car.Power = this.Power;
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var context = (EntitiesContext)validationContext.GetService(typeof(EntitiesContext));

			var car = context.Cars.FirstOrDefault(c => c.Id == this.Id);

			if (car == null)
				yield return new ValidationResult("Nehraj si se stránkou", new string[] { nameof(this.Id) });

			if (car != null && car.Brand.FoundedAt > this.ReleaseDate)
				yield return new ValidationResult("Auto nemůže vzniknout před značkou", new string[] { nameof(this.ReleaseDate) });
		}
	}
}
