using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Exchange.WebServices.Data;

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
            var userProfile = new UserProfile();
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (!userId.HasValue || userId.Value == 0)
                return RedirectToAction("Index"); // or login

            try
            {
                var ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand command = conn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "LmsUserProfileSummery";
                    command.Parameters.AddWithValue("@IUserId", userId.Value);
                    da.Fill(ds);
                }

                if (ds != null && ds.Tables.Count > 0)
                {
                    // Table 0 - basic profile (if any)
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        var row = ds.Tables[0].Rows[0];
                        userProfile.UserId = row.Table.Columns.Contains("UserId") && row["UserId"] != DBNull.Value ? Convert.ToInt32(row["UserId"]) : 0;
                        userProfile.UserName = row.Table.Columns.Contains("FullName") && row["FullName"] != DBNull.Value ? row["FullName"].ToString() : "";
                        userProfile.Email = row.Table.Columns.Contains("Email") && row["Email"] != DBNull.Value
                            ? row["Email"].ToString() : "";

                        userProfile.Age = row.Table.Columns.Contains("Age") && row["Age"] != DBNull.Value ? row["Age"].ToString() : "";

                        userProfile.Contact = row.Table.Columns.Contains("Contact") && row["Contact"] != DBNull.Value
                            ? row["Contact"].ToString() : string.Empty; // keep as string to avoid leading zeros

                        // About (string)
                        userProfile.About = row.Table.Columns.Contains("About") && row["About"] != DBNull.Value
                            ? row["About"].ToString() : string.Empty;

                        // State (string) - Note: column name in SELECT was [State]
                        userProfile.State = row.Table.Columns.Contains("State") && row["State"] != DBNull.Value
                            ? row["State"].ToString() : string.Empty;

                        // PinCode (string) - keep as string to preserve leading zeros if any
                        userProfile.PinCode = row.Table.Columns.Contains("PinCode") && row["PinCode"] != DBNull.Value
                            ? Convert.ToInt32(row["PinCode"]) : 0;

                        // Country (string)
                        userProfile.Country = row.Table.Columns.Contains("Country") && row["Country"] != DBNull.Value
                            ? row["Country"].ToString() : string.Empty;

                        if (row.Table.Columns.Contains("ModifiedOn") && row["ModifiedOn"] != DBNull.Value)
                            userProfile.ModifiedOn = Convert.ToDateTime(row["ModifiedOn"]);
                    }

                    // Table 1 - education
                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        var edlist = new List<UserEducationDetails>();
                        foreach (DataRow r in ds.Tables[1].Rows) // start at 0
                        {
                            var ed = new UserEducationDetails
                            {
                                Degree = r.Table.Columns.Contains("Degree") && r["Degree"] != DBNull.Value ? r["Degree"].ToString() : "",
                                NameOftheInstitution = r.Table.Columns.Contains("NameOftheInstitution") && r["NameOftheInstitution"] != DBNull.Value ? r["NameOftheInstitution"].ToString() : "",
                                IsPursuing = r.Table.Columns.Contains("IsYearGap") && r["IsYearGap"] != DBNull.Value ? Convert.ToBoolean(r["IsYearGap"]) : false,
                                StartDate = r.Table.Columns.Contains("StartDate") && r["StartDate"] != DBNull.Value ? r["StartDate"].ToString() : null,
                                PassoutYear = r.Table.Columns.Contains("PassoutYear") && r["PassoutYear"] != DBNull.Value ? r["PassoutYear"].ToString() : null
                            };
                            edlist.Add(ed);
                        }
                        userProfile.EducationDetails = edlist;
                    }

                    // Table 2 - courses (if any)
                    if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                    {
                        var clist = new List<UserCourses>();
                        foreach (DataRow r in ds.Tables[2].Rows)
                        {
                            var c = new UserCourses
                            {
                                CourseDetailsId = r.Table.Columns.Contains("CourseDetailsId") && r["CourseDetailsId"] != DBNull.Value ? Convert.ToInt32(r["CourseDetailsId"]) : 0,
                                CourseName = r.Table.Columns.Contains("CourseName") && r["CourseName"] != DBNull.Value ? r["CourseName"].ToString() : "",
                                CourseFees = r.Table.Columns.Contains("CourseFees") && r["CourseFees"] != DBNull.Value ? Convert.ToDecimal(r["CourseFees"]) : 0,
                                CompletionPercentage = r.Table.Columns.Contains("CompletionPercentage") && r["CompletionPercentage"] != DBNull.Value ? r["CompletionPercentage"].ToString() : ""
                            };
                            if (r.Table.Columns.Contains("EnrollOn") && r["EnrollOn"] != DBNull.Value)
                                c.EnrollOn = Convert.ToDateTime(r["EnrollOn"]);
                            clist.Add(c);
                        }
                        userProfile.UserCourses = clist;
                    }

                    // Table 3 - experience
                    if (ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
                    {
                        var elist = new List<UserExperience>();
                        foreach (DataRow r in ds.Tables[3].Rows)
                        {
                            var e = new UserExperience
                            {
                                CompanyName = r.Table.Columns.Contains("CompanyName") && r["CompanyName"] != DBNull.Value ? r["CompanyName"].ToString() : "",
                                WorkType = r.Table.Columns.Contains("WorkType") && r["WorkType"] != DBNull.Value ? r["WorkType"].ToString() : "",
                                Designation = r.Table.Columns.Contains("Designation") && r["Designation"] != DBNull.Value ? r["Designation"].ToString() : "",
                                Description = r.Table.Columns.Contains("Description") && r["Description"] != DBNull.Value ? r["Description"].ToString() : ""
                            };
                            if (r.Table.Columns.Contains("DateOfJoining") && r["DateOfJoining"] != DBNull.Value)
                                e.DateOfJoing = Convert.ToDateTime(r["DateOfJoining"]);
                            if (r.Table.Columns.Contains("LastWorkingDay") && r["LastWorkingDay"] != DBNull.Value)
                                e.LastWorkingDay = Convert.ToDateTime(r["LastWorkingDay"]);
                            if (r.Table.Columns.Contains("IsCurrentCompany") && r["IsCurrentCompany"] != DBNull.Value)
                                e.IsCurrentCompany = Convert.ToBoolean(r["IsCurrentCompany"]);

                            elist.Add(e);
                        }
                        userProfile.UserExperience = elist;
                    }
                }

                return View(userProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Oops something went wrong!!");
                // show friendly message or redirect
                TempData["ErrorMessage"] = "Unable to load profile at the moment.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveProfileData()
        {
            try
            {
                int userId = 0;
                var userIdFromSession = HttpContext.Session.GetInt32("UserId");
                if (!userIdFromSession.HasValue)
                    return RedirectToAction("Index");

                userId = userIdFromSession.Value;

                // Read primary fields
                var name = Request.Form["name"].ToString();
                var contact = Request.Form["contact"].ToString(); // keep as string
                var email = Request.Form["email"].ToString();
                var age = int.TryParse(Request.Form["age"], out var tmpAge) ? tmpAge : (int?)null;
                var about = Request.Form["about"].ToString();
                var country = Request.Form["country"].ToString();
                var state = Request.Form["state"].ToString();
                var pincode = Request.Form["pincode"].ToString();

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Save main profile
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.Transaction = transaction;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "lmsUserProfileSave";

                                cmd.Parameters.AddWithValue("@IUserId", userId);
                                cmd.Parameters.AddWithValue("@IAge", (object?)age ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@IAbout", (object?)about ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@ICountry", (object?)country ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@IState", (object?)state ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@IPinCode", (object?)pincode ?? DBNull.Value);

                                cmd.Parameters.Add("OId", SqlDbType.Int).Direction = ParameterDirection.Output;
                                cmd.Parameters.Add("OMsg", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                                cmd.ExecuteNonQuery();

                                int profileId = Convert.ToInt32(cmd.Parameters["OId"].Value);
                                string message = cmd.Parameters["OMsg"].Value.ToString();

                                // Education
                                int educationCount = int.TryParse(Request.Form["educationCount"], out var eCnt) ? eCnt : 0;
                                var educationList = new List<UserEducationDetails>();
                                for (int i = 1; i <= educationCount; i++)
                                {
                                    var degree = Request.Form[$"degree_{i}"].ToString();
                                    var college = Request.Form[$"college_{i}"].ToString();
                                    var percentage = Convert.ToDecimal(Request.Form[$"percentage_{i}"]);
                                    var endDateStr = Request.Form[$"end_date_{i}"].ToString();
                                    var pursuingVal = Request.Form[$"currently_pursuing_{i}"];
                                    if (string.IsNullOrWhiteSpace(degree) && string.IsNullOrWhiteSpace(college))
                                        continue;

                                    bool isPursuing = (pursuingVal == "on" || pursuingVal == "true");

                                    string? passoutYear = null;
                                    if (DateTime.TryParse(endDateStr, out var endDate))
                                        passoutYear = endDate.Year.ToString();

                                    educationList.Add(new UserEducationDetails
                                    {
                                        Degree = degree,
                                        NameOftheInstitution = college,
                                        Percentage = percentage,
                                        PassoutYear = passoutYear,
                                        IsPursuing = isPursuing
                                    });
                                }

                                // Save educations (use same transaction)
                                if (educationList.Count > 0)
                                {
                                    foreach (var edu in educationList)
                                    {
                                        using (SqlCommand educmd = conn.CreateCommand())
                                        {
                                            educmd.Transaction = transaction;
                                            educmd.CommandType = CommandType.StoredProcedure;
                                            educmd.CommandText = "lmsUserEducationSave";

                                            educmd.Parameters.AddWithValue("@IUserProfileId", profileId);
                                            educmd.Parameters.AddWithValue("@IDegree", (object?)edu.Degree ?? DBNull.Value);
                                            educmd.Parameters.AddWithValue("@IInstitution", (object?)edu.NameOftheInstitution ?? DBNull.Value);
                                            educmd.Parameters.AddWithValue("@IMarks", (object?)edu.Percentage ?? DBNull.Value);
                                            educmd.Parameters.AddWithValue("@IPassoutYear", (object?)edu.PassoutYear ?? DBNull.Value);
                                            educmd.Parameters.AddWithValue("@IIsPursuing", edu.IsPursuing);

                                            educmd.ExecuteNonQuery();
                                        }
                                    }
                                }

                                // Experience
                                int experienceCount = int.TryParse(Request.Form["experienceCount"], out var exCnt) ? exCnt : 0;
                                var experienceList = new List<UserExperience>();
                                for (int i = 1; i <= experienceCount; i++)
                                {
                                    var org = Request.Form[$"organization_{i}"].ToString();
                                    var designation = Request.Form[$"designation_{i}"].ToString();
                                    var dojStr = Request.Form[$"doj_{i}"].ToString();
                                    var dorStr = Request.Form[$"dor_{i}"].ToString();
                                    var currentlyWorking = Request.Form[$"currently_working_{i}"];

                                    if (string.IsNullOrWhiteSpace(org) && string.IsNullOrWhiteSpace(designation))
                                        continue;

                                    DateTime.TryParse(dojStr, out DateTime doj);
                                    DateTime.TryParse(dorStr, out DateTime dor);

                                    var e = new UserExperience
                                    {
                                        CompanyName = org,
                                        Designation = designation,
                                        WorkType = "",
                                        DateOfJoing = doj,
                                        LastWorkingDay = dor,
                                        IsCurrentCompany = (currentlyWorking == "on" || currentlyWorking == "true"),
                                        Description = null,
                                        ModifiedOn = DateTime.Now
                                    };
                                    experienceList.Add(e);
                                }

                                if (experienceList.Count > 0)
                                {
                                    foreach (var ex in experienceList)
                                    {
                                        using (SqlCommand expCmd = conn.CreateCommand())
                                        {
                                            expCmd.Transaction = transaction;
                                            expCmd.CommandType = CommandType.StoredProcedure;
                                            expCmd.CommandText = "lmsUserExperienceSave";

                                            expCmd.Parameters.AddWithValue("@IUserProfileId", profileId);
                                            expCmd.Parameters.AddWithValue("@ICompanyName", (object?)ex.CompanyName ?? DBNull.Value);
                                            expCmd.Parameters.AddWithValue("@IDesignation", (object?)ex.Designation ?? DBNull.Value);
                                            expCmd.Parameters.AddWithValue("@IDateOfJoining", ex.DateOfJoing == default ? (object)DBNull.Value : ex.DateOfJoing);
                                            expCmd.Parameters.AddWithValue("@ILastWorkingDay", ex.LastWorkingDay == default ? (object)DBNull.Value : ex.LastWorkingDay);
                                            expCmd.Parameters.AddWithValue("@IIsCurrentCompany", ex.IsCurrentCompany);

                                            expCmd.ExecuteNonQuery();
                                        }
                                    }
                                }

                                // Commit all
                                transaction.Commit();
                                TempData["SuccessMessage"] = "Profile updated successfully!";
                                return RedirectToAction("GetProfile");
                            }
                        }
                        catch (Exception ex)
                        {
                            try { transaction.Rollback(); } catch { /* log */ }
                            _logger.LogError(ex, "Error while saving profile data");
                            TempData["ErrorMessage"] = "Oops! Something went wrong while saving your profile.";
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while saving profile data outer");
                TempData["ErrorMessage"] = "Oops! Something went wrong while saving your profile.";
                return RedirectToAction("Index");
            }
        }

    }
}