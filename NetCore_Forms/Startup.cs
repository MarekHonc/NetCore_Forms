using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore_Forms.Code;
using NetCore_Forms.Data;
using NetCore_Forms.Entities;

namespace NetCore_Forms
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddDbContext<EntitiesContext>(options =>
				options.UseLazyLoadingProxies() // Kvůli EF Core 2.1, ve výchozím stavu nemá lazyloading, což 2.2 už má (aspoň myslím)
					.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
			);

			// Přidání Identity
			services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<EntitiesContext>();

			// Přidání posílače mailů (vygenerovaný Identity kód ho používá)
			services.AddTransient<IEmailSender, EmailSender>();

			// Konfigurace Identity
			services.Configure<IdentityOptions>(options =>
			{
				// Password settings.
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 6;
				options.Password.RequiredUniqueChars = 1;

				// Lockout settings.
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.AllowedForNewUsers = true;

				// User settings.
				options.User.RequireUniqueEmail = false;
			});

			services.ConfigureApplicationCookie(options =>
			{
				// Výhozí akce, na kterou bude uživatel přesměrován, když se snaží dostat někam, kam nemůže.
				options.LoginPath = "/Identity/Account/Login";
			});

			services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
			{
				microsoftOptions.ClientId = "b9f79a92-d1f2-461c-af40-d8665b9e9883";//Configuration["Authentication:Microsoft:ApplicationId"];
				microsoftOptions.ClientSecret = "edbbgA416!puJZFAOQ64-~["; //Configuration["Authentication:Microsoft:Password"];

				microsoftOptions.Scope.Add("people.read");
				microsoftOptions.Scope.Add("profile");
				microsoftOptions.Scope.Add("openid");
			});

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();
			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
