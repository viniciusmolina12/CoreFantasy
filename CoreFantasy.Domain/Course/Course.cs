using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFantasy.Domain.Course
{

    public class CourseId(string Value)
    {
        private string Value { get; } = Value;

        public static CourseId Create(string Value)
        {
            return new CourseId(Value);
        }

    }
    class Course
    { }
}
