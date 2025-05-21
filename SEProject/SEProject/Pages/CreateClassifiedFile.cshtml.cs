using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using SEProject.DataAccess.EF;
using SEProject.DataAccess.Model;
using System.IO;
using System.Threading.Tasks;

namespace SEProject.Pages
{
    public class CreateClassifiedFileModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateClassifiedFileModel(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [BindProperty]
        public string FileName { get; set; } = string.Empty;

        [BindProperty]
        public string Description { get; set; } = string.Empty;

        [BindProperty]
        public string Category { get; set; } = string.Empty;

        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (UploadedFile != null)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                // Verifică dacă folderul există, dacă nu îl creează
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var filePath = Path.Combine(folderPath, UploadedFile.FileName);

                // Copierea fișierului
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await UploadedFile.CopyToAsync(stream);
                }

                // Salvare în baza de date
                var classifiedFile = new ClassifiedFile
                {
                    FileName = FileName,
                    Description = Description,
                    Category = Category,
                    FilePath = filePath,
                    UploadDate = DateTime.Now
                };

                _context.ClassifiedFiles.Add(classifiedFile);
                await _context.SaveChangesAsync();
            }

            // Mesaj de confirmare și redirect
            TempData["SuccessMessage"] = "Fișierul a fost creat cu succes!";
            return RedirectToPage("/ViewClassifiedFiles");
        }
    }
}
