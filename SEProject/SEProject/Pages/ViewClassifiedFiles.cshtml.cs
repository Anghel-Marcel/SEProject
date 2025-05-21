using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SEProject.DataAccess.EF;
using SEProject.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SEProject.Pages
{
    public class ViewClassifiedFilesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ViewClassifiedFilesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ClassifiedFile> ClassifiedFiles { get; set; } = new List<ClassifiedFile>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string SearchBy { get; set; } = "FileName"; // Valoare implicită: Nume Fișier

        public void OnGet()
        {
            if (string.IsNullOrEmpty(SearchTerm))
            {
                ClassifiedFiles = _context.ClassifiedFiles.ToList();
            }
            else
            {
                ClassifiedFiles = SearchBy switch
                {
                    "FileName" => _context.ClassifiedFiles.Where(f => f.FileName.Contains(SearchTerm)).ToList(),
                    "Description" => _context.ClassifiedFiles.Where(f => f.Description.Contains(SearchTerm)).ToList(),
                    "Category" => _context.ClassifiedFiles.Where(f => f.Category.Contains(SearchTerm)).ToList(),
                    _ => _context.ClassifiedFiles.ToList()
                };
            }
        }

        public IActionResult OnGetDownload(int id)
        {
            var file = _context.ClassifiedFiles.FirstOrDefault(f => f.Id == id);
            if (file == null)
            {
                return Page();
            }

            var filePath = file.FilePath;
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", file.FileName); 

        }

        public IActionResult OnPostDelete(int id)
        {
            var file = _context.ClassifiedFiles.FirstOrDefault(f => f.Id == id);
            if (file == null)
            {
                return NotFound();
            }

            _context.ClassifiedFiles.Remove(file);
            _context.SaveChanges();

            if (System.IO.File.Exists(file.FilePath))
            {
                System.IO.File.Delete(file.FilePath);
            }

            return RedirectToPage();
        }
    }
}
