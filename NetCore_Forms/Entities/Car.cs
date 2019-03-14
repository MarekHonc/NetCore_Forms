using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetCore_Forms.Code;

namespace NetCore_Forms.Entities
{
	/// <summary>
	/// Třída reprezentující jeden záznam auta
	/// </summary>
	/// <remarks>Vazby musí být virtual, kvůlu lazy loading z EF Core 2.1 ve 2.2 už by třeba být nemělo</remarks>
	public class Car
	{
		/// <summary>
		/// Id auta
		/// </summary>
		[Key]
		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Název
		/// </summary>
		[Required]
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Uvedení do provozu
		/// </summary>
		[Required]
		public DateTime ReleaseDate
		{
			get;
			set;
		}

		/// <summary>
		/// Typ auto
		/// </summary>
		[Required]
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
		public int Power
		{
			get;
			set;
		}

		/// <summary>
		/// Značka
		/// </summary>
		[Required]
		public int BrandId
		{
			get;
			set;
		}

		[ForeignKey(nameof(BrandId))]
		public virtual Brand Brand
		{
			get;
			set;
		}

		public static Car Create(string name, DateTime date, CarType type, int power, int brand)
		{
			return new Car()
			{
				Name = name,
				ReleaseDate = date,
				Type = type,
				Power = power,
				BrandId = brand
			};
		}
	}
}
