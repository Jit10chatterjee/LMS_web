using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
//using Microsoft.Exchange.WebServices.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.Security.Claims;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LearningManagementSystem.Controllers
{
    public class ProfileController : Controller
    {
        private readonly string _connectionString;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(
            IConfiguration configuration
            , ILogger<ProfileController> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetProfile(int UserId)
        {
            Console.WriteLine(HttpContext.Session.GetInt32("UserId"));
            UserProfile userProfile = new UserProfile();
            if (UserId != 0)
            {
                try
                {
                    DataSet ds = new DataSet();
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        conn.Open();
                        SqlCommand command = conn.CreateCommand();
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "LmsUserProfileSummery";
                        command.Parameters.AddWithValue("@IUserId", UserId);
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        da.Fill(ds);
                    }
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            userProfile.UserId = Convert.ToInt32(ds.Tables[0].Rows[0]["UserId"]);
                            userProfile.UserName = (ds.Tables[0].Rows[0]["FullName"]).ToString() ?? "";
                            userProfile.Email = (ds.Tables[0].Rows[0]["Email"]).ToString() ?? "";
                            userProfile.ModifiedOn = Convert.ToDateTime(ds.Tables[0].Rows[0]["ModifiedOn"]);
                        }
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            List<UserEducationDetails> userEducationDetails = new List<UserEducationDetails>();
                            for (int i = 1; i < ds.Tables[1].Rows.Count; i++)
                            {
                                UserEducationDetails Edetails = new UserEducationDetails();
                                Edetails.Degree = (ds.Tables[1].Rows[i]["Degree"]).ToString() ?? "";
                                Edetails.NameOftheInstitution = (ds.Tables[1].Rows[i]["NameOftheInstitution"]).ToString() ?? "";
                                Edetails.IsYearGap = Convert.ToBoolean(ds.Tables[1].Rows[i]["IsYearGap"]);

                                userEducationDetails.Add(Edetails);
                            }
                            userProfile.EducationDetails = userEducationDetails;
                        }
                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            List<UserCourses> userCourseList = new List<UserCourses>();
                            for (int i = 1; i < ds.Tables[2].Rows.Count; i++)
                            {
                                UserCourses course = new UserCourses();
                                course.CourseDetailsId = Convert.ToInt32(ds.Tables[2].Rows[i]["CourseDetailsId"]);
                                course.CourseName = (ds.Tables[2].Rows[i]["CourseName"]).ToString() ?? "";
                                course.CourseFees = Convert.ToDecimal(ds.Tables[2].Rows[i]["CourseFees"]);
                                course.EnrollOn = Convert.ToDateTime(ds.Tables[2].Rows[i]["EnrollOn"]);
                                course.CompletionPercentage = (ds.Tables[2].Rows[i]["CompletionPercentage"]).ToString() ?? "";

                                userCourseList.Add(course);
                            }
                            userProfile.UserCourses = userCourseList;
                        }
                        if (ds.Tables[3].Rows.Count > 0)
                        {
                            List<UserExperience> userExperienceList = new List<UserExperience>();
                            for (int i = 1; i < ds.Tables[3].Rows.Count; i++)
                            {
                                UserExperience exp = new UserExperience();
                                exp.CompanyName = (ds.Tables[3].Rows[i]["CompanyName"]).ToString() ?? "";
                                exp.WorkType = (ds.Tables[3].Rows[i]["WorkType"]).ToString() ?? "";
                                exp.DateOfJoing = Convert.ToDateTime(ds.Tables[3].Rows[i]["DateOfJoining"]);
                                exp.LastWorkingDay = Convert.ToDateTime(ds.Tables[3].Rows[i]["LastWorkingDay"]);
                                exp.IsCurrentCompany = Convert.ToBoolean(ds.Tables[3].Rows[i]["IsCurrentCompany"]);
                                exp.Designation = (ds.Tables[3].Rows[i]["Designation"]).ToString() ?? "";
                                exp.Description = (ds.Tables[3].Rows[i]["Description"]).ToString() ?? "";

                                userExperienceList.Add(exp);
                            }
                            userProfile.UserExperience = userExperienceList;
                        }
                    }
                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "Oops something went wrong!!");
                    throw;
                }
            }
            //System.IO.File.WriteAllText("C:\\Temp\\userProfile.txt", JsonConvert.SerializeObject(userProfile, Formatting.Indented));
            return View(userProfile);
        }

        [HttpGet]
        public IActionResult SaveProfileDetails()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SaveProfileData()
        {
            try
            {
                int UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

                var name = Request.Form["name"].ToString();
                var contact = Convert.ToInt32(Request.Form["contact"].ToString());
                var email = Request.Form["email"].ToString();
                var age = Convert.ToInt32(Request.Form["age"].ToString());
                var about = Request.Form["about"].ToString();
                var country = Request.Form["country"].ToString();
                var state = Request.Form["state"].ToString();
                var pincode = Request.Form["pincode"].ToString();

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Transaction = transaction;

                    cmd.CommandText = "lmsUserProfileSave";

                    cmd.Parameters.AddWithValue("@IUserId", UserId);
                    cmd.Parameters.AddWithValue("@IAge", age);
                    cmd.Parameters.AddWithValue("@IAbout", about);
                    cmd.Parameters.AddWithValue("@ICountry", country);
                    cmd.Parameters.AddWithValue("@IState", state);
                    cmd.Parameters.AddWithValue("@IPinCode", pincode);

                    cmd.Parameters.Add("OId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("OMsg", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    int profileId = Convert.ToInt32(cmd.Parameters["OId"].Value);
                    string message = cmd.Parameters["OMsg"].Value.ToString();

                    if(profileId > 0)
                    {
                        var educationList = new List<UserEducationDetails>();

                        int educationCount = 0;
                        int.TryParse(Request.Form["educationCount"], out educationCount);

                        for (int i = 1; i <= educationCount; i++)
                        {
                            var degree = Request.Form[$"degree_{i}"];
                            var college = Request.Form[$"college_{i}"];
                            var startDateStr = Request.Form[$"start_date_{i}"];
                            var endDateStr = Request.Form[$"end_date_{i}"];
                            var pursuingStr = Request.Form[$"currently_pursuing_{i}"];

                            // Skip empty rows
                            if (string.IsNullOrWhiteSpace(degree) && string.IsNullOrWhiteSpace(college))
                                continue;

                            bool currentlyPursuing = string.Equals(pursuingStr, "true", StringComparison.OrdinalIgnoreCase);

                            string? passoutYear = null;
                            if (DateTime.TryParse(endDateStr, out var endDate))
                            {
                                passoutYear = endDate.Year.ToString();
                            }

                            var edu = new UserEducationDetails
                            {
                                Degree = degree,
                                NameOftheInstitution = college,
                                StartDate = startDateStr,
                                PassoutYear = passoutYear,
                                IsPursuing = currentlyPursuing
                            };

                            educationList.Add(edu);
                        }


                        if (educationList.Count > 0)
                            {
                            for (int j = 1; j < educationList.Count; j++)
                            {

                                using (SqlCommand educmd = conn.CreateCommand())
                                {
                                    SqlTransaction sqlTransaction = conn.BeginTransaction();
                                    educmd.Transaction = sqlTransaction;
                                    educmd.CommandType = CommandType.StoredProcedure;

                                    educmd.CommandText = "lmsUserEducationSave";

                                    educmd.Parameters.AddWithValue("@IUserProfileId", profileId);
                                    educmd.Parameters.AddWithValue("@IDegree", educationList[j].Degree ?? (object)DBNull.Value);
                                    educmd.Parameters.AddWithValue("@IInstitution", educationList[j].NameOftheInstitution ?? (object)DBNull.Value);
                                    educmd.Parameters.AddWithValue("@IStartDate", educationList[j].StartDate ?? (object)DBNull.Value);
                                    educmd.Parameters.AddWithValue("@IPassoutYear", (object?)educationList[j].PassoutYear ?? DBNull.Value);
                                    educmd.Parameters.AddWithValue("@IIsPursuing", educationList[j].IsPursuing);

                                    educmd.ExecuteNonQuery();
                                }
                            }

                        }


                        var experienceList = new List<UserExperience>();
                        int experienceCount = 0;
                        int.TryParse(Request.Form["experienceCount"], out experienceCount);

                        for (int i = 1; i <= experienceCount; i++)
                        {
                            var org = Request.Form[$"organization_{i}"].ToString();
                            var designation = Request.Form[$"designation_{i}"].ToString();
                            var dojStr = Request.Form[$"doj_{i}"].ToString();
                            var dorStr = Request.Form[$"dor_{i}"].ToString();
                            var currentlyWorking = Request.Form[$"currently_working_{i}"].ToString();

                            if (string.IsNullOrWhiteSpace(org) && string.IsNullOrWhiteSpace(designation))
                                continue;

                            DateTime doj, dor;
                            DateTime.TryParse(dojStr, out doj);
                            DateTime.TryParse(dorStr, out dor);

                            var exp = new UserExperience
                            {
                                CompanyName = org,
                                Designation = designation,
                                WorkType = "",
                                DateOfJoing = doj,
                                LastWorkingDay = dor,
                                IsCurrentCompany = (currentlyWorking == "on"),
                                Description = null,
                                ModifiedOn = DateTime.Now
                            };

                            experienceList.Add(exp);
                        }

                        if (experienceList.Count > 0)
                        {
                            for (int j = 1; j < educationList.Count; j++)
                            {

                                using (SqlCommand expCmd = conn.CreateCommand())
                                {
                                    SqlTransaction sqlTrans = conn.BeginTransaction();
                                    //expCmd.CommandType = CommandType.StoredProcedure;
                                    //expCmd.Transaction = sqlTrans;
                                    //expCmd.CommandText = "lmsUserExperienceSave"; // create this SP in DB

                                    //expCmd.Parameters.AddWithValue("@IUserProfileId", profileId);
                                    //expCmd.Parameters.AddWithValue("@ICompanyName", (object?)exp.CompanyName ?? DBNull.Value);
                                    //expCmd.Parameters.AddWithValue("@IDesignation", (object?)exp.Designation ?? DBNull.Value);
                                    //expCmd.Parameters.AddWithValue("@IDateOfJoining", (object?)exp.DateOfJoining ?? DBNull.Value);
                                    //expCmd.Parameters.AddWithValue("@ILastWorkingDay", (object?)exp.LastWorkingDay ?? DBNull.Value);
                                    //expCmd.Parameters.AddWithValue("@IIsCurrentCompany", exp.IsCurrentCompany);
                                }
                            }
                        }
                    }
                }

                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToAction("Index"); // or GetProfile with userId
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while saving profile data");
                TempData["ErrorMessage"] = "Oops! Something went wrong while saving your profile.";
                return RedirectToAction("Index");
            }
        }
    }
}