using System.ComponentModel.DataAnnotations.Schema;

namespace eMenu.Data
{
	public class MenuItem
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public int Price { get; set; }
		public string Description { get; set; }

		[ForeignKey(nameof(MenuPage))]
		public int? MenuPageId { get; set; }

		[ForeignKey(nameof(MenuCategory))]
		public int? MenuCatagoryId { get; set; }
		public MenuPage MenuPage { get; set; }

		public MenuCategory MenuCategory { get; set; }

		public string ImageUrl { get; set; }
	}
}
