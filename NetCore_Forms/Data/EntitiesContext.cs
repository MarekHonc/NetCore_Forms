using Microsoft.EntityFrameworkCore;
using NetCore_Forms.Entities;

namespace NetCore_Forms.Data
{
	/// <summary>
	/// Databázový kontext entit
	/// </summary>
	public class EntitiesContext : DbContext
	{
		public EntitiesContext(DbContextOptions<EntitiesContext> options) : base(options)
		{
		}

		/// <summary>
		/// Tabulka se značkami aut
		/// </summary>
		public DbSet<Brand> Brands
		{
			get;
			set;
		}

		/// <summary>
		/// Tabulka s auty
		/// </summary>
		public DbSet<Car> Cars
		{
			get;
			set;
		}
	}
}
