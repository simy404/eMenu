using eMenu.Models.Authentication;
using eMenu.Models.Context;
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
			builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<eMenuDBContext>();

			// Add services to the container.
			builder.Services.AddControllersWithViews();

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