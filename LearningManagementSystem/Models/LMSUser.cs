using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LearningManagementSystem.Models
{
    public class LMSUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public string? Contact {  get; set; }
        public string Gender { get; set; }
    }

    public class UserDetails
    {
        public int UserId { get; set; }
        public string LMSUserId { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class UserProfile
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<UserEducationDetails>? EducationDetails { get; set; }
        public List<UserCourses>? UserCourses { get; set; }
        public List<UserExperience>? UserExperience { get; set; }
        public DateTime? ModifiedOn { get; set; }
        //public int? ModifiedBy { get; set; }
    }

    public class UserEducationDetails
    {
        //public bool? IsSchool { get; set; }
        //public int? Class { get; set; }
        //public bool? IsCollege { get; set; }
        //public string? NameOfSchool { get; set; }
        //public string? NameOfCollge { get; set; }
       //public decimal? CGPA { get; set; }
        //public int? Percentage { get; set; }
        public string Degree { get; set; }
        public string NameOftheInstitution { get; set; }
        public string? PassoutYear { get; set; }
        public bool? IsYearGap { get; set; }
        //public DateTime? CreatedOn { get; set; }
        //public string? CreatedBy { get; set; }
        //public DateTime? ModifiedOn { get; set; }
        //public string? ModifiedBy { get; set; }
    }

    public class UserCourses
    {
        public int CourseDetailsId { get; set; }
        public string CourseName { get; set; }
        public DateTime EnrollOn { get; set; }
        public string CourseStatus { get; set; }
        public decimal CourseFees { get; set; }
        public string CompletionPercentage { get; set; }

    }
    public class UserExperience
    {
        public string CompanyName { get; set; }
        public string WorkType { get; set; }
        public DateTime DateOfJoing { get; set; }
        public DateTime LastWorkingDay { get; set; } 
        public bool IsCurrentCompany { get; set; }
        public string? Designation {  get; set; }
        public string? Description { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }


    #region POULAMI












    #endregion
}
