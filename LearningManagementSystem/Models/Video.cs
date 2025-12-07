using System;
using System.Collections.Generic;

namespace LearningManagementSystem.Models
{
    // Represents CourseMedia table row (your CourseMedia)
    public class CourseMedia
    {
        public int CourseMediaId { get; set; }
        public int CourseDetailsId { get; set; }
        public string Title { get; set; }
        public string PosterLink { get; set; }
        public string VideoLink { get; set; }
        public bool IsDisabled { get; set; }
        public int CompletionPercentage { get; set; }
        public int SortOrder { get; set; }
    }

    // Per-user progress
    public class UserVideoProgress
    {
        public int UserVideoProgressId { get; set; }
        public string UserId { get; set; }
        public int CourseMediaId { get; set; }
        public bool IsChecked { get; set; }
        public DateTime? CheckedOn { get; set; }
    }

    // Per-user note
    public class UserVideoNote
    {
        public int UserVideoNotesId { get; set; }
        public string UserId { get; set; }
        public int CourseMediaId { get; set; }
        public string NoteText { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }

    // View model passed to CoursePlayer.cshtml
    public class CourseVideoDetails
    {
        public int CourseMediaId { get; set; }
        public string Title { get; set; }
        public string Poster { get; set; }    // thumbnail / poster link
        public string Source { get; set; }    // video link
        public bool IsDisabled { get; set; }  // locked for user if true
        public int CompletionPercentage { get; set; }
        public string UserNote { get; set; }  // per-user note text
        public bool IsChecked { get; set; }   // per-user checked flag
    }

    public class CourseMediaList
    {
        public int CourseId { get; set; }
        public List<CourseVideoDetails> Videos { get; set; } = new List<CourseVideoDetails>();
    }
}
