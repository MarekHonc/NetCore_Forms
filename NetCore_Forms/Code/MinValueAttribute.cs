using System.ComponentModel.DataAnnotations;

namespace NetCore_Forms.Code
{
	/// <inheritdoc />
	/// <summary>
	/// Atribut pro validaci minimální hodnoty intu.
	/// </summary>
	public sealed class MinValueAttribute : ValidationAttribute
	{
		private readonly int minValue;

		public MinValueAttribute(int minValue)
		{
			this.minValue = minValue;
		}

		public override bool IsValid(object value)
		{
			return (int) value <= this.minValue;
		}
	}
}
