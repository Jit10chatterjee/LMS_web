using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using LearningManagementSystem.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

namespace LearningManagementSystem.Controllers
{
    public class LandingPageController : Controller
    {
        private readonly SignInManager<LMSUser> _signInManager;
        private readonly string _connectionString;
        private readonly ILogger<LandingPageController> _logger;
        public LandingPageController
            (SignInManager<LMSUser> signInManager
            ,IConfiguration configuration,
             ILogger<LandingPageController> logger
            )
        {
            _signInManager = signInManager;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LandingPage()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var email = HttpContext.Session.GetString("Email");
                if (email != null)
                {
                    GetUserDataByEmail(email);
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LandingPage", "LandingPage");
        }

        [HttpGet]
        public IActionResult IsUserLoggedIn()
        {
            if(User.Identity != null && User.Identity.IsAuthenticated)
            {
                return Json(new { loggedIn = true });
            }
            return Json(new { loggedIn = false });
        }

        public void GetUserDataByEmail(string email)
        {
            try
            {
                DataTable dt = new DataTable(); 
                if (email != null && email != "")
                {
                    using (SqlConnection con = new SqlConnection(_connectionString))
                    {
                        con.Open();
                        SqlCommand command = con.CreateCommand();
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "lmsGetUserDetailsByEmailId";
                        command.Parameters.AddWithValue("@IEmailId", email);
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        da.Fill(dt);
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        var userId = Convert.ToInt32(dt.Rows[0]["UserId"]);
                        HttpContext.Session.SetInt32("UserId", userId);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in GetUserDataByEmail for email: {Email}", email);
                throw;
            }
        }
    }
}
