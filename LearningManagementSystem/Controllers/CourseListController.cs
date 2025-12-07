using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LearningManagementSystem.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LearningManagementSystem.Controllers
{
    [Authorize]
    public class CourseListController : Controller
    {
        private readonly string _connectionString;
        private readonly ILogger<CourseListController> _logger;

        public CourseListController(IConfiguration configuration, ILogger<CourseListController> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(int Id = 0)
        {
            CourseListPageLoad model = new CourseListPageLoad();
            model.courseMasterList = new List<CourseMaster>();
            model.skillsList = new List<Skills>();

            try
            {

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("lmsGetAllCourseType", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);

                    foreach (DataRow row in dt1.Rows)
                    {
                        model.courseMasterList.Add(new CourseMaster
                        {
                            CourseMasterId = Convert.ToInt32(row["CourseMasterId"]),
                            CourseMasterType = row["CourseMasterType"].ToString()
                        });
                    }

                    SqlCommand cmd2 = new SqlCommand("lmsGetAllSkills", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);

                    foreach (DataRow row in dt2.Rows)
                    {
                        model.skillsList.Add(new Skills
                        {
                            SkillId = Convert.ToInt32(row["SkillId"]),
                            SkillName = row["SkillName"].ToString()
                        });
                    }
                }

                var courseList = GetCoursesByMasterId(Id);
                ViewBag.CourseList = courseList; 
                ViewBag.SelectedCategoryId = Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in Index for Id: {Id}", Id);
                throw;
            }
            return View(model);
        }

        public List<CourseInfo> GetCoursesByMasterId(int id)
        {
            List<CourseInfo> courses = new List<CourseInfo>();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand command = con.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "lsmGetCoursesById";
                command.Parameters.AddWithValue("@ICourseMasterId", id);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
            }

            foreach (DataRow row in dt.Rows)
            {
                courses.Add(new CourseInfo()
                {
                    CourseDetailsId = Convert.ToInt32(row["CourseDetailsId"]),
                    CourseName = row["CourseName"].ToString(),
                    Duration = row["Duration"].ToString(),
                    CourseProvider = row["CourseProvider"].ToString(),
                    CourseFees = Convert.ToDecimal(row["CourseFees"]),
                    NoOfEnrollment = Convert.ToInt32(row["NoOfEnrollment"]),
                    CourseStatusName = row["CourseStatusName"].ToString(),
                    CourseImage = row["CourseImage"].ToString()
                });
            }

            return courses;
        }

        private List<CourseInfo> GetFilteredCourses(
            int? categoryId,
            string courseName,
            string provider,
            string status,
            string courseTypesCsv,
            string skillsCsv
        )
        {
            bool hasAnyFilter = !string.IsNullOrWhiteSpace(courseName)
                                || !string.IsNullOrWhiteSpace(provider)
                                || !string.IsNullOrWhiteSpace(status)
                                || !string.IsNullOrWhiteSpace(courseTypesCsv)
                                || !string.IsNullOrWhiteSpace(skillsCsv);

            if (!hasAnyFilter)
            {
                return GetCoursesByMasterId(categoryId ?? 0);
            }
            List<CourseInfo> courses = new List<CourseInfo>();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "lsmFilterCourses";

                cmd.Parameters.AddWithValue("@CourseName", (object)(courseName ?? ""));
                cmd.Parameters.AddWithValue("@Provider", (object)(provider ?? ""));
                cmd.Parameters.AddWithValue("@Status", (object)(status ?? ""));
                cmd.Parameters.AddWithValue("@CourseTypesCSV", (object)(courseTypesCsv ?? ""));
                cmd.Parameters.AddWithValue("@SkillsCSV", (object)(skillsCsv ?? ""));

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            foreach (DataRow row in dt.Rows)
            {
                courses.Add(new CourseInfo()
                {
                    CourseDetailsId = Convert.ToInt32(row["CourseDetailsId"]),
                    CourseName = row["CourseName"].ToString(),
                    Duration = row["Duration"].ToString(),
                    CourseProvider = row["CourseProvider"].ToString(),
                    CourseFees = Convert.ToDecimal(row["CourseFees"]),
                    NoOfEnrollment = Convert.ToInt32(row["NoOfEnrollment"]),
                    CourseStatusName = row["CourseStatusName"].ToString()
                });
            }

            return courses;
        }


        [HttpGet]
        public IActionResult GetPagedCourses(
            int id = 0,
            int page = 1,
            int pageSize = 6,
            string courseName = "",
            string provider = "",
            string status = "",
            string courseTypes = "",
            string skills = ""
        )
        {
            try
            {
                var allCourses = GetFilteredCourses(id, courseName, provider, status, courseTypes, skills);

                int totalCount = allCourses.Count;
                var paged = allCourses
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                ViewBag.TotalCount = totalCount;
                ViewBag.PageSize = pageSize;
                ViewBag.CurrentPage = page;
                ViewBag.SelectedCategoryId = id;
                ViewBag.CourseNameFilter = courseName ?? "";
                ViewBag.ProviderFilter = provider ?? "";
                ViewBag.StatusFilter = status ?? "";
                ViewBag.CourseTypesFilter = courseTypes ?? "";
                ViewBag.SkillsFilter = skills ?? "";

                return PartialView("_CourseListPartial", paged);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetPagedCourses");
                return StatusCode(500, "Internal server error");
            }
        }

        
        [HttpPost]
        public IActionResult FilterCourses(
            string courseName,
            string provider,
            string status,
            string courseTypes,
            string skills,
            int id = 0
        )
        {
            
            return GetPagedCourses(id: id, page: 1, pageSize: 6,
                courseName: courseName ?? "",
                provider: provider ?? "",
                status: status ?? "",
                courseTypes: courseTypes ?? "",
                skills: skills ?? "");
        }


        [HttpGet]
        public IActionResult GetCourseDetails(int id)
        {
            return View();
        }

        [HttpPost]
        private Tuple<int,string> courseEnrollment(string email)
        {
            Tuple<int, string> response = Tuple.Create(-1, "");
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            return response;
        }
    }
}
