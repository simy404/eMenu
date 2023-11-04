using System.ComponentModel.DataAnnotations;

namespace eMenu.Models.ViewModels
{
	public class AppUserViewModel
	{
		public string UserName { get; set; }
		public string Email { get; set; }		
		public string Password { get; set; }
	}
}
