using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCore_Forms.Data;
using NetCore_Forms.Models;

namespace NetCore_Forms.Controllers
{
	public class HomeController : Controller
	{
		private readonly EntitiesContext context;

		public HomeController(EntitiesContext context)
		{
			this.context = context;
		}

		public IActionResult Index()
		{
			return View(context.Brands.Include("Cars").ToArray());
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
