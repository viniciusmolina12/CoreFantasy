using System.Runtime.CompilerServices;
using CourseId = CoreFantasy.Domain.Course.CourseId;
using Notification = CoreFantasy.Domain.Shared.Notification;

[assembly: InternalsVisibleTo("CoreFantasy.Infrastructure")]
[assembly: InternalsVisibleTo("CoreFantasy.Domain.Tests")]
namespace CoreFantasy.Domain.Player.Entities
{
    public record EducationRules
    {
        public readonly static int MIN_COURSE_PROGRESS = 0;
        public readonly static int MAX_COURSE_PROGRESS = 100;
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

        public decimal CalculateEducationCost(int studyHours)
        {
            // TODO IMPLEMENT EDUCATION COST CALCULATION
            return 0m;
        }
        public void UpdateCourseProgress(int progress)
        {
            if(progress < EducationRules.MIN_COURSE_PROGRESS) return;
            this.Progress = (this.Progress + progress) > EducationRules.MAX_COURSE_PROGRESS ? EducationRules.MAX_COURSE_PROGRESS : this.Progress + progress;
        }

    }
}
