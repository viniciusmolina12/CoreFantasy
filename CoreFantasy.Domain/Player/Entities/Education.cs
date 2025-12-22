using System.Runtime.CompilerServices;
using CourseId = CoreFantasy.Domain.Course.CourseId;
using Notification = CoreFantasy.Domain.Shared.Notification;

[assembly: InternalsVisibleTo("CoreFantasy.Infrastructure")]
namespace CoreFantasy.Domain.Player.Entities
{
    file record EducationRules
    {
        public static int MIN_COURSE_PROGRESS = 0;
        public static int MAX_COURSE_PROGRESS = 100;
    }
    public class Education
    {
        public CourseId CourseId { get; private set; }
        public int Progress { get; private set; }

        private Education(CourseId courseId)
        {
            this.CourseId = courseId;
            this.Progress = 0;
        }

        private Education(CourseId courseId, int progress)
        {
            this.CourseId = courseId;
            this.Progress = progress;
        }

        public static Education Create(CourseId courseId)
        {
            return new(courseId);
        }

        internal static Education Rehydrate(CourseId courseId, int progress)
        {
            return new(courseId, progress);
        }


        public Notification UpdateCourseProgress(int progress)
        {
            Notification notification = new();
            
            if(progress < EducationRules.MIN_COURSE_PROGRESS)
            {
                notification.AddError(typeof(Education).Name, EducationErrors.COURSE_PROGRESS_CANNOT_BE_NEGATIVE);
                return notification;
            }

            int newProgress = this.Progress + progress;

            if (newProgress > EducationRules.MAX_COURSE_PROGRESS)
            {
                notification.AddError(typeof(Education).Name, EducationErrors.COURSE_PROGRESS_EXCEED_MAXIMUM);
            }

            if (notification.HasErrors()) return notification;
            this.Progress = newProgress;
            return notification;
        }

    }

    public record EducationErrors
    {
        public static readonly string COURSE_PROGRESS_CANNOT_BE_NEGATIVE = "Course progress cannot be negative.";
        public static readonly string COURSE_PROGRESS_EXCEED_MAXIMUM = $"Course progress cannot exceed maximum limit of {EducationRules.MAX_COURSE_PROGRESS}.";
    }
}
