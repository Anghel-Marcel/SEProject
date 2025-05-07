using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SEProject.Data;
using SEProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SEProject.Pages.Identity.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public RegisterModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public int PoliceStationId { get; set; }

        public List<PoliceStation> PoliceStations { get; set; }

        public void OnGet()
        {
            PoliceStations = _context.PoliceStations.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = new AppUser
            {
                UserName = Email,
                Email = Email,
                PoliceStationId = PoliceStationId
            };

            var result = await _userManager.CreateAsync(user, Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToPage("/ViewClassifiedFiles");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
