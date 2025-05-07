using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SEProject.Data;
using System.Security.Cryptography;
using System.Text;

namespace SEProject.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context)
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

        public IActionResult OnPost()
        {
            var hashed = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(Input.Password)));

            var user = _context.Users.FirstOrDefault(u => u.Username == Input.Username && u.Password == hashed);
            if (user == null)
            {
                Message = "Invalid username or password.";
                return Page();
            }

            HttpContext.Session.SetString("Username", user.Username);
            return RedirectToPage("/Index");
        }
    }
}
