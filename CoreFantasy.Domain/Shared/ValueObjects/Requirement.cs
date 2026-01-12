using CoreFantasy.Domain.Course;
using CoreFantasy.Domain.Player.ValueObjects;

namespace CoreFantasy.Domain.Shared.ValueObjects
{
    sealed public class Requirement : ValueObject
    {
        public CourseId[] CoursesCompleted { get; }
        public Age MinAge { get; }
        private Requirement(CourseId[] coursesCompleted, Age MinAge)
        {
          this.CoursesCompleted = coursesCompleted;
          this.MinAge = MinAge;
        }
        public static Requirement Create(CourseId[] coursesCompleted, Age MinAge)
        {
            return new(coursesCompleted, MinAge);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CoursesCompleted;
            yield return MinAge;
        }
    }
}
