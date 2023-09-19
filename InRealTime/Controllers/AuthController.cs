using InRealTime.Data;
using InRealTime.Data.Entities;
using InRealTime.Models.RequestModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InRealTime.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _dbContext;

        public AuthController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost()]
        public async Task<IActionResult> SignIn(LoginUserRequest request)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(
                user => user.Email == request.Email && user.Password == request.Password);

            if (user == null)
            {
                return NotFound(new { message = "Invalid Email or Password" });
            }

            var claims = new HashSet<Claim>
            {
                new("user_id", user.UserId.ToString()),
                new("user_name", user.UserName.ToString())
            };

            var identity = new ClaimsIdentity(claims, "default");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("default", principal, new AuthenticationProperties() { IsPersistent = true });
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost()]
        public async Task<IActionResult> SignUp(CreateUserRequest request)
        {
            var query = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == request.Email);

            if (query != null)
            {
                return NotFound(new { message = "Email is already taken" });
            }

            var user = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                UserName = request.UserName,
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("SignIn");
        }

        public new async Task<ActionResult> SignOut()
        {
            await HttpContext.SignOutAsync("default",
                new AuthenticationProperties() { IsPersistent = true });
            return View("SignIn");
        }
    }
}
