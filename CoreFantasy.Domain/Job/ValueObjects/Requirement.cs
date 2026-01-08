using CoreFantasy.Domain.Player.ValueObjects;
using CoreFantasy.Domain.Shared;

namespace CoreFantasy.Domain.Job.ValueObjects
{
    sealed public class Requirement : ValueObject
    {
        public string[] CoursesCompleted { get; }
        public Age MinAge { get; }
        private Requirement(string[] coursesCompleted, Age MinAge)
        {
          this.CoursesCompleted = coursesCompleted;
          this.MinAge = MinAge;
        }
        public static Requirement Create(string[] coursesCompleted, Age MinAge)
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
