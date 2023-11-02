using System.ComponentModel.DataAnnotations.Schema;

namespace eMenu.Data
{
	public class MenuCategory
	{
		public int Id { get; set; }
		public string Name { get; set; }
		
		[ForeignKey(nameof(MenuPage))]
		public int? MenuPageId { get; set; }
		public MenuPage MenuPage { get; set; }

		public ICollection<MenuItem> MenuItems;
	}
}
