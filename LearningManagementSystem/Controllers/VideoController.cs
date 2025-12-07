using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Controllers
{
    public class VideoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult CoursePlayer()
        //{
        //    CourseMediaList list = new CourseMediaList();

        //    return View(list);
        //}
        public IActionResult CoursePlayer(int courseId = 1)
        {
            // --------- HARD CODED DUMMY DATA (NO DATABASE NEEDED) ----------
            var dummyMedia = new List<CourseVideoDetails>
            {
                new CourseVideoDetails
                {
                    CourseMediaId = 1,
                    Title = "Introduction to C",
                    Poster = "https://media.licdn.com/dms/image/v2/D4D12AQEmzusF9C5JfA/article-cover_image-shrink_600_2000/article-cover_image-shrink_600_2000/0/1683178954137?e=2147483647&v=beta&t=nsmAzz64DpJaO7qloxqYXjv4Wgk3dXZdzUw1_KQvyLE",
                    Source = "https://www.youtube.com/watch?v=EjavYOFoJJ0&list=PLdo5W4Nhv31a8UcMN9-35ghv8qyFWD9_S&index=1",
                    CompletionPercentage = 10,
                    IsDisabled = false,   // first video unlocked
                    IsChecked = false,
                    UserNote = ""
                },

                new CourseVideoDetails
                {
                    CourseMediaId = 2,
                    Title = "Features of C",
                    Poster = "https://media.licdn.com/dms/image/v2/D4D12AQEmzusF9C5JfA/article-cover_image-shrink_600_2000/article-cover_image-shrink_600_2000/0/1683178954137?e=2147483647&v=beta&t=nsmAzz64DpJaO7qloxqYXjv4Wgk3dXZdzUw1_KQvyLE",
                    Source = "https://www.youtube.com/watch?v=i3SWaOhjPCY&list=PLdo5W4Nhv31a8UcMN9-35ghv8qyFWD9_S&index=4",
                    CompletionPercentage = 20,
                    IsDisabled = true,
                    IsChecked = false,
                    UserNote = ""
                },

                new CourseVideoDetails
                {
                    CourseMediaId = 3,
                    Title = "Variables in C",
                    Poster = "https://media.licdn.com/dms/image/v2/D4D12AQEmzusF9C5JfA/article-cover_image-shrink_600_2000/article-cover_image-shrink_600_2000/0/1683178954137?e=2147483647&v=beta&t=nsmAzz64DpJaO7qloxqYXjv4Wgk3dXZdzUw1_KQvyLE",
                    Source = "https://www.youtube.com/watch?v=dhh5lrXXXYw&list=PLdo5W4Nhv31a8UcMN9-35ghv8qyFWD9_S&index=8",
                    CompletionPercentage = 20,
                    IsDisabled = true,
                    IsChecked = false,
                    UserNote = ""
                },

                new CourseVideoDetails
                {
                    CourseMediaId = 4,
                    Title = "Keywords and Identifiers in C",
                    Poster = "https://media.licdn.com/dms/image/v2/D4D12AQEmzusF9C5JfA/article-cover_image-shrink_600_2000/article-cover_image-shrink_600_2000/0/1683178954137?e=2147483647&v=beta&t=nsmAzz64DpJaO7qloxqYXjv4Wgk3dXZdzUw1_KQvyLE",
                    Source = "https://www.youtube.com/watch?v=Ywnv78X7TAg&list=PLdo5W4Nhv31a8UcMN9-35ghv8qyFWD9_S&index=9",
                    CompletionPercentage = 20,
                    IsDisabled = true,
                    IsChecked = false,
                    UserNote = ""
                },

                new CourseVideoDetails
                {
                    CourseMediaId = 5,
                    Title = "C Zero to Hero",
                    Poster = "https://i.ytimg.com/vi/YXcgD8hRHYY/hq720.jpg?sqp=-oaymwEnCNAFEJQDSFryq4qpAxkIARUAAIhCGAHYAQHiAQoIGBACGAY4AUAB&rs=AOn4CLBs-YBQRPvWj_BXEFTnpyn_Lxpfvg",
                    Source = "https://www.youtube.com/watch?v=YXcgD8hRHYY",
                    CompletionPercentage = 30,
                    IsDisabled = true,
                    IsChecked = false,
                    UserNote = ""
                }
            };

            // Put dummy videos inside model
            var model = new CourseMediaList
            {
                CourseId = courseId,
                Videos = dummyMedia
            };

            return View(model);
        }

    }
}
