using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SEProject.DataAccess.EF;
using SEProject.DataAccess.Model;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Collections.Generic;

namespace SEProject.Pages
{
    public class AssignPoliceStationModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AssignPoliceStationModel(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<AppUser> Users { get; set; }
        public List<PoliceStation> Stations { get; set; }

        public void OnGet()
        {
            Users = _userManager.Users.ToList();
            Stations = _context.PoliceStations.ToList();
        }

        public IActionResult OnPost(string UserId, int PoliceStationId)
        {
            var user = _userManager.FindByIdAsync(UserId).Result;
            if (user != null)
            {
                user.PoliceStationId = PoliceStationId;
                _userManager.UpdateAsync(user).Wait();
            }

            TempData["SuccessMessage"] = "Utilizatorul a fost asociat cu succes!";
            return RedirectToPage("/AssignPoliceStation");
        }
    }
}
