// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.Data.SqlClient;
using System.Data;
using LearningManagementSystem.Models;
using Microsoft.Identity.Client;

namespace LearningManagementSystem.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<LMSUser> _signInManager;
        private readonly UserManager<LMSUser> _userManager;
        private readonly IUserStore<LMSUser> _userStore;
        private readonly IUserEmailStore<LMSUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        //connection string property
        private readonly string _connectionString;

        public RegisterModel(
            UserManager<LMSUser> userManager,
            IUserStore<LMSUser> userStore,
            SignInManager<LMSUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required(ErrorMessage = "Full name is required"), Display(Name = "Full Name")]
            public string FullName { get; set; } = string.Empty;

            [Range(1, 120)]
            public int? Age { get; set; }

            [Required(ErrorMessage = "Please select gender")]
            [Display(Name = "Gender")]
            public string? Gender { get; set; }

            [Required(ErrorMessage ="Please select Date of Birth")]
            [DataType(DataType.Date), Display(Name = "Date of Birth")]
            public DateTime? DateOfBirth { get; set; }

            [Required(ErrorMessage = "Contact number is required")]
            [Phone, Display(Name = "Contact Number")]
            public string Contact { get; set; } = string.Empty;
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Email is required")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Password is required")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            ///
            [Required(ErrorMessage ="Please type password for confirmation")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }

            if (ModelState.IsValid)
            {
                var user = new LMSUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FullName = Input.FullName,
                    Age = Input.Age,
                    Gender = Input.Gender,
                    DateOfBirth = Input.DateOfBirth,
                    Contact = Input.Contact
                };

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                //insert record into userdetails
                int Ostatus = 0;
                if(result.Succeeded)
                {
                    Ostatus = SaveUserDetails(user.Id).Item1;
                }
                
                if (result.Succeeded && Ostatus > 0)
                {


                    //_logger.LogInformation("User created a new account with password.");

                    //var userId = await _userManager.GetUserIdAsync(user);
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    //{
                    //    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    //}
                    //else
                    //{
                    //    await _signInManager.SignInAsync(user, isPersistent: false);
                    //    return LocalRedirect(returnUrl);
                    //}
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("LandingPage", "LandingPage");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private LMSUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<LMSUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(LMSUser)}'. " +
                    $"Ensure that '{nameof(LMSUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<LMSUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<LMSUser>)_userStore;
        }

        private Tuple<int,string> SaveUserDetails(string LMSUserId)
        {
            int oid = 0;
            string msg = "";
            Tuple<int,string> rtn = new Tuple<int,string>(oid,msg);
            //SqlTransaction transaction = null;
            try
            {
                if(LMSUserId != null || LMSUserId != "")
                {
                    using (SqlConnection con = new SqlConnection(_connectionString))
                    {
                        con.Open();
                        //transaction = con.BeginTransaction();
                        SqlCommand command = con.CreateCommand();
                        //transaction = command.Transaction;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "lmsUserDetailsSave";
                        command.Parameters.AddWithValue("@ILMSUserId", LMSUserId);
                        command.Parameters.Add("@OUserId", SqlDbType.Int, 1024).Direction = ParameterDirection.Output;
                        command.Parameters.Add("@OMsg", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();

                        oid = Convert.ToInt32(command.Parameters["@OUserId"].Value);
                        msg = command.Parameters["@OMsg"].Value.ToString();
                    }
                    rtn = new Tuple<int, string>(oid,msg);

                }
                return rtn;
            }
            catch (Exception ex)
            {
                rtn = new Tuple<int,string>(-1,ex.Message);
            }
            return rtn;
        }
    }
}
