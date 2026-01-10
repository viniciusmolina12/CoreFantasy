using System.Runtime.CompilerServices;
using CourseEntity = CoreFantasy.Domain.Course.Course;

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
        public CourseEntity Course { get; private set; }
        public int Progress { get; private set; }

        private Education(CourseEntity course)
        {
            this.Course = course;
            this.Progress = 0;
        }

        private Education(CourseEntity course, int progress)
        {
            this.Course = course;
            this.Progress = progress;
        }

        public static Education Create(CourseEntity course)
        {
            return new(course);
        }

        internal static Education Rehydrate(CourseEntity course, int progress)
        {
            return new(course, progress);
        }

        public decimal CalculateEducationCost(int studyHours)
        {
            return this.Course.StudyCostPerHour * studyHours;
        }
        public void UpdateCourseProgress(int progress)
        {
            if(progress < EducationRules.MIN_COURSE_PROGRESS) return;
            this.Progress = (this.Progress + progress) > EducationRules.MAX_COURSE_PROGRESS ? EducationRules.MAX_COURSE_PROGRESS : this.Progress + progress;
        }

    }
}
