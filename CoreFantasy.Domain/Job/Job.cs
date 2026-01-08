using CoreFantasy.Domain.Job.ValueObjects;
using System.Collections.ObjectModel;

namespace CoreFantasy.Domain.Job
{
    public class JobId(Guid Value)
    {
        private Guid Value { get; } = Value;

        public static JobId Create()
        {
            return new JobId(Guid.NewGuid());
        }

    }

    public class Job
    {
        public JobId Id { get; private set; }
        public string Name { get; private set; }
        public string Area { get; private set; }
        public decimal BaseSalaryPerHour { get; private set; }
        public decimal HealthCostPerHour { get; private set; }
        public Collection<Requirement> Requirements { get; private set; }
        public Collection<JobPosition> JobPositions { get; private set; }

        private Job(
            JobId Id,
            string Name, 
            string Area, 
            decimal BaseSalaryPerHour, 
            decimal HealthCostPerHour,
            Collection<Requirement> Requirements,
            Collection<JobPosition> JobPositions
        ) 
        {
            this.Id = Id;
            this.Name = Name;
            this.Area = Area;
            this.BaseSalaryPerHour = BaseSalaryPerHour;
            this.HealthCostPerHour = HealthCostPerHour;
            this.Requirements = Requirements;
            this.JobPositions = JobPositions;
        }

        public Job(){}

        public static Job Create(
            string Name,
            string Area,
            decimal BaseSalaryPerHour,
            decimal HealthCostPerHour,
            Collection<Requirement> Requirements,
            Collection<JobPosition> JobPositions
            )
        {
            return new Job(
                JobId.Create(),
                Name,
                Area,
                BaseSalaryPerHour,
                HealthCostPerHour,
                Requirements,
                JobPositions
                );
        }
        internal static Job Rehydrate(
            JobId JobId,
            string Name,
            string Area,
            decimal BaseSalaryPerHour,
            decimal HealthCostPerHour,
            Collection<Requirement> Requirements,
            Collection<JobPosition> JobPositions
            )
        {
            return new Job
            {
                Id = JobId,
                Name = Name,
                Area = Area,
                BaseSalaryPerHour = BaseSalaryPerHour,
                HealthCostPerHour = HealthCostPerHour,
                Requirements = Requirements,
                JobPositions = JobPositions
            };
        }


        public decimal CalculateHealthCost(int workedHours)
        {
            return HealthCostPerHour * workedHours;
        }

    }
}
