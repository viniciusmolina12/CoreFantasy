using CoreFantasy.Domain.Job;
using CoreFantasy.Domain.Shared;
using System.Runtime.CompilerServices;
using JobEntity = CoreFantasy.Domain.Job.Job;

[assembly: InternalsVisibleTo("CoreFantasy.Infrastructure")]
[assembly: InternalsVisibleTo("CoreFantasy.Domain.Tests")]
// TODO - TEST
namespace CoreFantasy.Domain.Player.Entities
{
    public record CareerRules
    {
        public readonly static int MIN_WORKED_HOURS = 0;
    }
    public class Career
    {
        public JobEntity Job { get; private set; }
        public JobPositionId JobPositionId { get; private set; }
        public int WorkedHours { get; private set; }

        private Career(JobEntity job, JobPositionId jobPositionId)
        {
            this.Job = job;
            this.JobPositionId = jobPositionId;
            this.WorkedHours = 0;
        }

        private Career(JobEntity job, JobPositionId jobPositionId, int workedHours)
        {
            this.Job = job;
            this.JobPositionId = jobPositionId;
            this.WorkedHours = workedHours;
        }

        public static Career Create(JobEntity job, JobPositionId jobPositionId)
        {
            return new(job, jobPositionId);
        }

        internal static Career Rehydrate(JobEntity job, JobPositionId jobPositionId, int workedHours)
        {
            return new(job, jobPositionId, workedHours);
        }


        public decimal CalculateEarnings(int workedHours)
        {
            return this.Job.BaseSalaryPerHour * workedHours;
        }

        internal Notification AddWorkedHours(int hours)
        {
            Notification notification = new();
            if(hours < 0)
            {
               notification.AddError(typeof(Career).Name, CareerErrors.WORKED_HOURS_CANNOT_BE_NEGATIVE);
            }

            int newWorkedHours = WorkedHours + hours;
            if (notification.HasErrors()) return notification;
            WorkedHours = newWorkedHours;
            return notification;
        }

    }

    public record CareerErrors
    {
        public static readonly string WORKED_HOURS_CANNOT_BE_NEGATIVE = "Worked hours cannot be negative.";
    }
}
