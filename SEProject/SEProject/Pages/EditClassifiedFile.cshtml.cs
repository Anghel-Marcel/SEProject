using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SEProject.Data;
using SEProject.Models;
using System.Linq;

namespace SEProject.Pages
{
    public class EditClassifiedFileModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditClassifiedFileModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public string FileName { get; set; } = string.Empty;

        [BindProperty]
        public string Description { get; set; } = string.Empty;

        [BindProperty]
        public string Category { get; set; } = string.Empty;

        public string? Message { get; set; }

        public IActionResult OnGet(int id)
        {
            var file = _context.ClassifiedFiles.FirstOrDefault(f => f.Id == id);
            if (file == null)
            {
                return NotFound();
            }

            Id = file.Id;
            FileName = file.FileName;
            Description = file.Description;
            Category = file.Category;

            return Page();
        }

        public IActionResult OnPost()
        {
            var file = _context.ClassifiedFiles.FirstOrDefault(f => f.Id == Id);
            if (file == null)
            {
                return NotFound();
            }

            file.FileName = FileName;
            file.Description = Description;
            file.Category = Category;

            _context.SaveChanges();

            Message = "Modificările au fost salvate cu succes!";
            return RedirectToPage("/ViewClassifiedFiles");
        }
    }
}
