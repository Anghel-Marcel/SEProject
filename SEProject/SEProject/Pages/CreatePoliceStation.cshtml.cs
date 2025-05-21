using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SEProject.DataAccess.EF;
using SEProject.DataAccess.Model;

namespace SEProject.Pages
{
    public class CreatePoliceStationModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreatePoliceStationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Name { get; set; } = string.Empty;

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var station = new PoliceStation
            {
                Name = Name
            };

            _context.PoliceStations.Add(station);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Secția de Poliție a fost adăugată cu succes!";
            return RedirectToPage("/ViewPoliceStations");
        }
    }
}
