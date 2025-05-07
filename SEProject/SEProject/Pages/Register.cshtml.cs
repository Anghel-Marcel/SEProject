using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SEProject.Data;
using SEProject.Models; 
using System.Security.Cryptography;
using System.Text;

namespace SEProject.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string Message { get; set; }

        public class InputModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (_context.Users.Any(u => u.Username == Input.Username))
            {
                Message = "Username already taken.";
                return Page();
            }

            var hashed = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(Input.Password)));

            var user = new User
            {
                Username = Input.Username,
                Password = hashed
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            Message = "Registration successful! You can now log in.";
            return RedirectToPage("Login");
        }
    }
}
