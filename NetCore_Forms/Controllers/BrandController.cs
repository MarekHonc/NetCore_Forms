using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NetCore_Forms.Data;
using NetCore_Forms.Models;

namespace NetCore_Forms.Controllers
{
	public class BrandController : Controller
	{
		private readonly EntitiesContext context;

		public BrandController(EntitiesContext context)
		{
			this.context = context;
		}

		/// <summary>
		/// Vrací formulář
		/// </summary>
		public IActionResult Create()
		{
			var model = new BrandCreatorViewModel();

			return View(model);
		}

		/// <summary>
		/// Post pro akci <see cref="Create()"></see>.
		/// </summary>
		[HttpPost]
		public IActionResult Create(BrandCreatorViewModel postedModel)
		{
			// Pokud je model validní, vytvořím entitu a navrátím redirect.
			if (ModelState.IsValid)
			{
				var entity = postedModel.CreateEntity();
				this.context.Brands.Add(entity);
				this.context.SaveChanges();

				return RedirectToAction("Index", "Home");
			}

			return View(postedModel);
		}

		/// <summary>
		/// Vrací formulář, pro editaci značky podle předaného id
		/// </summary>
		/// <remarks>místo id se může předat cokoliv, podle čeho se dá z databáze jednoznačně určit entitu</remarks>
		public IActionResult Edit(int id)
		{
			var brand = this.context.Brands.FirstOrDefault(b => b.Id == id);

			// Může mi do akce přijít jakékoliv id, tímpádem nemusí značka existovat.
			if (brand == null)
				return NotFound();

			var model = new BrandEditorViewModel(brand);

			return View(model);
		}

		/// <summary>
		/// Post pro akci <see cref="Edit(int)"></see>.
		/// </summary>
		[HttpPost]
		public IActionResult Edit(BrandEditorViewModel postedModel)
		{
			// Pokud je model validní, vytvořím entitu a navrátím redirect.
			if (ModelState.IsValid)
			{
				var brand = context.Brands.FirstOrDefault(b => b.Id == postedModel.Id);
				postedModel.UpdateEntity(brand);
				this.context.SaveChanges();

				return RedirectToAction("Index", "Home");
			}

			return View(postedModel);
		}

		/// <summary>
		/// Post pro akci <see cref="Edit(int)"></see>.
		/// </summary>
		[HttpPost]
		public IActionResult Delete(BrandEditorViewModel postedModel)
		{
			// Pokud je model validní, vytvořím entitu a navrátím redirect.
			if (ModelState.IsValid)
			{
				var brand = context.Brands.FirstOrDefault(b => b.Id == postedModel.Id);
				this.context.Brands.Remove(brand);
				this.context.SaveChanges();

				return RedirectToAction("Index", "Home");
			}

			return View(postedModel);
		}
	}
}