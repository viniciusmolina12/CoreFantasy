using System.Collections.ObjectModel;
using CoreFantasy.Domain.Shared.ValueObjects;
using CoreFantasy.Domain.Shared;

namespace CoreFantasy.Domain.Course
{

    public class CourseId : ValueObject
    {
        public string Value { get; private set; }

        private CourseId(string value)
        {
            Value = value;
        }

        public static CourseId Create(string value)
        {
            return new CourseId(value);
        }
        

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

    }
    public class Course
    {
        public CourseId Id { get; private set; }
        public string Name { get; private set; }
        public string Area { get; private set; }
        public int TotalHours { get; private set; }
        public int StudyCostPerHour { get; private set; }
        public int HealthCostPerHour { get; private set; }
        public Collection<Requirement> Requirements { get; private set; }

        private Course(CourseId id, string name, string area, int totalHours, int studyCostPerHour, int healthCostPerHour, Collection<Requirement> requirements)
        {
            Id = id;
            Name = name;
            Area = area;
            TotalHours = totalHours;
            StudyCostPerHour = studyCostPerHour;
            HealthCostPerHour = healthCostPerHour;
            Requirements = requirements;
        }

        public static Course Create(string name, string area, int totalHours, int studyCostPerHour, int healthCostPerHour, Collection<Requirement> requirements)
        {
            return new Course(CourseId.Create(Guid.NewGuid().ToString()), name, area, totalHours, studyCostPerHour, healthCostPerHour, requirements);
        }

        internal static Course Rehydrate(CourseId id, string name, string area, int totalHours, int studyCostPerHour, int healthCostPerHour, Collection<Requirement> requirements)
        {
            return new Course(id, name, area, totalHours, studyCostPerHour, healthCostPerHour, requirements);
        }
        
    }
}
