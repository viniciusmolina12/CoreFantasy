using CoreFantasy.Domain.Job;
using CoreFantasy.Domain.Shared;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CoreFantasy.Infrastructure")]
// TODO - TEST
namespace CoreFantasy.Domain.Player.Entities
{
    file record CareerRules
    {
        public static int MIN_WORKED_HOURS = 0;
    }
    public class Career: ValueObject
    {
        public JobId JobId { get; private set; }
        public JobPositionId JobPositionId { get; private set; }
        public int WorkedHours { get; private set; }

        private Career(JobId jobId, JobPositionId jobPositionId)
        {
            this.JobId = jobId;
            this.JobPositionId = jobPositionId;
            this.WorkedHours = 0;
        }

        private Career(JobId jobId, JobPositionId jobPositionId, int workedHours)
        {
            this.JobId = jobId;
            this.JobPositionId = jobPositionId;
            this.WorkedHours = workedHours;
        }

        public static Career Create(JobId jobId, JobPositionId jobPositionId)
        {
            return new(jobId, jobPositionId);
        }

        internal static Career Rehydrate(JobId jobId, JobPositionId jobPositionId, int workedHours)
        {
            return new(jobId, jobPositionId, workedHours);
        }

   
        internal Notification AddWorkedHours(int hours)
        {
            Notification notification = new();
            if(hours < 0)
            {
               notification.AddError("Career", CareerErrors.WORKED_HOURS_CANNOT_BE_NEGATIVE);
            }

            int newWorkedHours = WorkedHours + hours;
            if (notification.HasErrors()) return notification;
            WorkedHours = newWorkedHours;
            return notification;
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return JobId;
            yield return JobPositionId;
            yield return WorkedHours;
        }
    }

    public record CareerErrors
    {
        public static readonly string WORKED_HOURS_CANNOT_BE_NEGATIVE = "Worked hours cannot be negative.";
    }
}
