using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using System.Data;
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
    }
}