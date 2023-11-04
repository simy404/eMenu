namespace eMenu.Models.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Persistent { get; set; } //If a cookie is set as 'Persistent,' it means that this cookie will be stored in the browser for a certain period of time 
		public bool Lock { get; set; }
    }
}
