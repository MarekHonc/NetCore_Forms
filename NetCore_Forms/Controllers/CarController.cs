using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCore_Forms.Data;
using NetCore_Forms.Models;

namespace NetCore_Forms.Controllers
{
	[Authorize]
	public class CarController : Controller
	{
		private readonly EntitiesContext context;

		public CarController(EntitiesContext context)
		{
			this.context = context;
		}

		/// <summary>
		/// Navrátí formulář
		/// </summary>
		public IActionResult Create(int? preselectedBrand = null)
		{
			var model = new CarCreatorViewModel();
			model.Brands = context.Brands.ToDictionary(b => b.Id, b => b.Name);

			// Pokud je předem vybraná značka
			if(preselectedBrand != null)
			{
				var brand = this.context.Brands.FirstOrDefault(b => b.Id == preselectedBrand.Value);
				// a existuje
				if (brand != null)
				{
					// přidám ji do modelu
					model.BrandId = preselectedBrand.Value;
				}
			}

			return View(model);
		}

		/// <summary>
		/// Post pro akci <see cref="Create(int?)"/>
		/// </summary>
		[HttpPost]
		public IActionResult Create(CarCreatorViewModel postedModel)
		{
			if (ModelState.IsValid)
			{
				var entity = postedModel.CreateEntity();
				this.context.Cars.Add(entity);
				this.context.SaveChanges();

				return RedirectToAction("Index", "Home");
			}

			postedModel.Brands = context.Brands.ToDictionary(b => b.Id, b => b.Name);
			return View(postedModel);
		}

		/// <summary>
		/// Vrací formulář, pro editaci auto podle předaného id
		/// </summary>
		/// <remarks>místo id se může předat cokoliv, podle čeho se dá z databáze jednoznačně určit entitu</remarks>
		public IActionResult Edit(int id)
		{
			var car = this.context.Cars.FirstOrDefault(c => c.Id == id);

			// Může mi do akce přijít jakékoliv id, tímpádem nemusí značka existovat.
			if (car == null)
				return NotFound();

			var model = new CarEditorViewModel(car);

			return View(model);
		}

		/// <summary>
		/// Post pro akci <see cref="Edit(int)"></see>.
		/// </summary>
		[HttpPost]
		public IActionResult Edit(CarEditorViewModel postedModel)
		{
			// Pokud je model validní, vytvořím entitu a navrátím redirect.
			if (ModelState.IsValid)
			{
				var car = context.Cars.FirstOrDefault(c => c.Id == postedModel.Id);
				postedModel.UpdateEntity(car);
				this.context.SaveChanges();

				return RedirectToAction("Index", "Home");
			}

			return View(postedModel);
		}

		/// <summary>
		/// Post pro akci <see cref="Edit(int)"></see>.
		/// </summary>
		[HttpPost]
		public IActionResult Delete(CarEditorViewModel postedModel)
		{
			// Pokud je model validní, vytvořím entitu a navrátím redirect.
			if (ModelState.IsValid)
			{
				var car = context.Cars.FirstOrDefault(c => c.Id == postedModel.Id);
				this.context.Cars.Remove(car);
				this.context.SaveChanges();

				return RedirectToAction("Index", "Home");
			}

			return View(postedModel);
		}
	}
}