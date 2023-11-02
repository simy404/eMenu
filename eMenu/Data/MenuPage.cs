using eMenu.Models.Authentication;
using System.ComponentModel.DataAnnotations.Schema;

namespace eMenu.Data
{
	public class MenuPage
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		
		[ForeignKey(nameof(User))]
		public string UserId { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime? UpdateDate { get; set; }
		public string ImageUrl { get; set; }
		public ICollection<MenuCategory> MenuCategories { get; set; }

		public ICollection<MenuItem> MenuItems { get; set; }
		public AppUser User { get; set; }

		
	}
}
