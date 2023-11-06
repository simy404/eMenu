using eMenu.AutoMappers;
using eMenu.Models.Authentication;
using eMenu.Models.Context;
using eMenu.Models.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace eMenu
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddDbContext<eMenuDBContext>(_ => _.UseSqlServer(builder.Configuration.GetValue<string>("ConnectionStrings:SqlServerConnectionString")));
			builder.Services.AddIdentity<AppUser, IdentityRole>(_ => {
				
				_.User.RequireUniqueEmail = true;
				_.User.AllowedUserNameCharacters = "abcçdefghiýjklmnoöpqrsþtuüvwxyzABCÇDEFGHIÝJKLMNOÖPQRSÞTUÜVWXYZ0123456789-._@+";	

			}).AddEntityFrameworkStores<eMenuDBContext>();

			builder.Services.ConfigureApplicationCookie(_ =>
			{
				_.LoginPath = new PathString("/User/Login");
				_.Cookie = new CookieBuilder
				{
					Name = "AspNetCoreIdentityExampleCookie", // Names the Cookie to be created.
					HttpOnly = false, //Prevents client-side access to the Cookie to enhance security.
					MaxAge = TimeSpan.FromMinutes(10), //Specifies the expiration time for the Cookie to be created.
					SameSite = SameSiteMode.Lax, //Specifies that the Cookie should not be sent for requests that do not result in top-level navigation.
					SecurePolicy = CookieSecurePolicy.Always //Specifies that the Cookie should be accessible via HTTPS.
				};
				_.SlidingExpiration = true; //If a request is made within half of the expiration time, it will reset the remaining half, effectively refreshing the initial expiration time.
				_.ExpireTimeSpan = TimeSpan.FromMinutes(10); // Specifies the Cookie's expiration time to ensure it matches the value set in the CookieBuilder, as a precaution against potential overriding of default values..
			});

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<AppUserValidator>());

			builder.Services.AddAutoMapper(typeof(MappingProfile));

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}