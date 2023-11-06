using AutoMapper;
using eMenu.Models.Authentication;
using eMenu.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eMenu.Controllers
{
	public class UserController : Controller
	{
		readonly UserManager<AppUser> _userManager;
		readonly SignInManager<AppUser> _signInManager;
		readonly IMapper _mapper;
		public UserController(UserManager<AppUser> userManager, IMapper mapper, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_mapper = mapper;
			_signInManager = signInManager;
		}

		[Authorize]
		public IActionResult Index()
		{
			return View(_userManager.Users);
		}

		public IActionResult Login(string ReturnUrl)
		{
			TempData["returnUrl"] = ReturnUrl;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				AppUser user = await _userManager.FindByEmailAsync(model.Email);
				if (user != null)
				{
					//İlgili kullanıcıya dair önceden oluşturulmuş bir Cookie varsa siliyoruz.
					await _signInManager.SignOutAsync();
					Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, model.Persistent, model.Lock);

					if (result.Succeeded)
					{
						await _userManager.ResetAccessFailedCountAsync(user); //Önceki hataları girişler neticesinde +1 arttırılmış tüm değerleri 0(sıfır)a çekiyoruz.

						if (string.IsNullOrEmpty(TempData["returnUrl"] != null ? TempData["returnUrl"].ToString() : ""))
							return RedirectToAction("Index");
						return Redirect(TempData["returnUrl"].ToString());
					}
					else
					{
						await _userManager.AccessFailedAsync(user); //Eğer ki başarısız bir account girişi söz konusu ise AccessFailedCount kolonundaki değer +1 arttırılacaktır. 

						int failcount = await _userManager.GetAccessFailedCountAsync(user); //Kullanıcının yapmış olduğu başarısız giriş deneme adedini alıyoruz.
						if (failcount == 3)
						{
							await _userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.Now.AddMinutes(1))); //Eğer ki başarısız giriş denemesi 3'ü bulduysa ilgili kullanıcının hesabını kilitliyoruz.
							ModelState.AddModelError("Locked", "Your account has been locked for 1 minute due to 3 consecutive failed login attempts.");
						}
						else
						{
							if (result.IsLockedOut)
								ModelState.AddModelError("Locked", "Your account has been locked for 1 minute due to 3 consecutive failed login attempts.");
							else
								ModelState.AddModelError("NotUser2", "Email or password incorrect.");	
						}

					}
				}
				else
				{
					ModelState.AddModelError("NotUser", "No such user exists.");
					ModelState.AddModelError("NotUser2", "Incorrect email or password.");
				}
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(AppUserViewModel appUserViewModel)
		{
			if (ModelState.IsValid)
			{
				AppUser appUser = _mapper.Map<AppUser>(appUserViewModel);
				IdentityResult result = await _userManager.CreateAsync(appUser, appUserViewModel.Password);
				
				if (result.Succeeded)
					return RedirectToAction("Index","Home");
			}
			return View();
		}
	}
}
