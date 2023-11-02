using eMenu.Data;
using eMenu.Models.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eMenu.Models.Context
{
    public class eMenuDBContext : IdentityDbContext<AppUser>
	{
		public eMenuDBContext(DbContextOptions<eMenuDBContext> options) : base(options)
		{
		}
		
		public DbSet<MenuItem> MenuItems { get; set; }
		public DbSet<MenuPage> MenuPages { get; set; }
		public DbSet<MenuCategory> MenuCategories { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}


	}
}
