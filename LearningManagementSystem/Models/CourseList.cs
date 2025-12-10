using System.Collections.Generic;

namespace LearningManagementSystem.Models
{
    public class CourseList
    {
    }

    public class CourseInfo
    {
        public int CourseDetailsId { get; set; }
        public string CourseName { get; set; }
        public string Duration { get; set; }
        public string CourseProvider { get; set; }
        public decimal CourseFees { get; set; }
        public int NoOfEnrollment { get; set; }
        public string CourseStatusName { get; set; }
        public string CourseImage { get; set; }
    }

    //Model class for PageLoad in course list
    public class CourseMaster
    {
        public int CourseMasterId { get; set; }
        public string CourseMasterType { get; set; }
    }
    public class Skills
    {
        public int SkillId { get; set; }
        public string SkillName { get; set;}
    }
    public class CourseListPageLoad
    {
        public List<CourseMaster> courseMasterList { get; set; }
        public List<Skills> skillsList { get; set; }
    }


    // course categorization
    public class CourseCategoryList
    {
        public int CourseMasterId { get; set; }
        public string CourseMasterType { get; set; }
        public int NoOfCourses { get; set; }

    }

    public class CourseCategory
    {
        public List<CourseCategoryList> CourseCategoryList { get; set; }
        public List<CourseInfo>  popularOrDemandingCourseList { get; set; }
    }

    public class CourseSpecialization
    {
        public int SpecializationId { get; set; }
        public string Specialization { get; set; }
    }

    public class RelatedCourses
    {
        public int CourseDetailsId { get; set; }
        public string CourseName { get; set; }
        public string CourseImage { get; set; }
        public string CourseProvider { get; set; }

    }

    public class CourseDetailsByID
    {
        public string email { get; set; }
        public int CourseDetailsId { get; set; }
        public string CourseName { get; set; }
        public string CourseProvider { get; set; }
        public bool IsFree { get; set; }
        public decimal CourseFees { get; set; }
        public int Duration { get; set; }

        public List<Skills> Skills { get; set; }
        public List<CourseSpecialization> CourseSpecializationList { get; set; }

        public List<RelatedCourses> RelatedCourses { get; set; }
    }

}
