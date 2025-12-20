using CoreFantasy.Domain.Course;
using CoreFantasy.Domain.Shared;
using CoreFantasy.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFantasy.Domain.Player.ValueObjects
{
    file record EducationRules
    {
        public static int MIN_COURSE_PROGRESS = 0;
        public static int MAX_COURSE_PROGRESS = 100;
    }
    public class Education
    {
        public CourseId CurrentCourseId { get; private set; }
        public int CurrentCourseProgress { get; private set; }

        private Education(CourseId currentCourseId)
        {
            this.CurrentCourseId = currentCourseId;
            this.CurrentCourseProgress = 0;
        }

        private Education(CourseId currentCourseId, int currentCourseProgress)
        {
            this.CurrentCourseId = currentCourseId;
            this.CurrentCourseProgress = currentCourseProgress;
        }

        public static Education Create(CourseId currentCourseId)
        {
            return new Education(currentCourseId);
        }

        internal static Education Rehydrate(CourseId currentCourseId, int currentCourseProgress)
        {
            return new Education(currentCourseId, currentCourseProgress);
        }


        public Notification UpdateCourseProgress(int progress)
        {
            Notification notification = new();
            if (this.CurrentCourseProgress > progress)
            {
                notification.AddError(typeof(Education).Name, EducationErrors.COURSE_PROGRESS_CANNOT_BE_DECREASED);
            }
            if (progress < EducationRules.MIN_COURSE_PROGRESS) { 
                notification.AddError(typeof(Education).Name, EducationErrors.COURSE_PROGRESS_CANNOT_BE_NEGATIVE);
            }else if (progress > EducationRules.MAX_COURSE_PROGRESS) { 
                notification.AddError(typeof(Education).Name, EducationErrors.COURSE_PROGRESS_EXCEED_MAXIMUM);
            }

            if (notification.HasErrors()) return notification;
            this.CurrentCourseProgress = progress;
            return notification;
        }

    }

    public record EducationErrors
    {
        public static readonly string COURSE_PROGRESS_CANNOT_BE_NEGATIVE = "Course progress cannot be negative.";

        public static readonly string COURSE_PROGRESS_CANNOT_BE_DECREASED = "Course progress cannot be decreased.";

        public static readonly string COURSE_PROGRESS_EXCEED_MAXIMUM = $"Course progress cannot exceed maximum limit of {EducationRules.MAX_COURSE_PROGRESS}.";
    }
}
