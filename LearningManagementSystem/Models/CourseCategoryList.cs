public class CourseCategoryList
{
     public int CourseMasterId { get; set; }
     public string CourseMasterType { get; set; }

}
public class CourseCategory
{
    public List<CourseCategoryList> CourseCategoryList { get; set; }    
}
