using Bogus;
using CoreFantasy.Domain.Job;
using CoreFantasy.Domain.Shared;
using Errors = CoreFantasy.Domain.Player.Entities.CareerErrors;
using Rules = CoreFantasy.Domain.Player.Entities.CareerRules;
using Sut = CoreFantasy.Domain.Player.Entities.Career;
using JobEntity = CoreFantasy.Domain.Job.Job;
using CoreFantasy.Domain.Job.ValueObjects;
using CoreFantasy.Domain.Player.ValueObjects;

namespace CoreFantasy.Domain.Tests.Player.Entities
{
    public class Career
    {
        public Faker faker = new();
        public readonly JobEntity job = JobEntity.Create("Any Job", "Any Area", 10, 10, [Requirement.Create(["none"], Age.Create(18).Age)], [new JobPosition()]);
        public readonly JobPositionId jobPositionId = JobPositionId.Create();


        [Fact]
        public void Should_Rehydrated_A_Career_Correctly()
        {
            // Arrange
            int workedHours = faker.Random.Int(Rules.MIN_WORKED_HOURS);
            Sut career = Sut.Rehydrate(job, jobPositionId, workedHours);
            Assert.Equal(job, career.Job);
            Assert.Equal(jobPositionId, career.JobPositionId);
            Assert.Equal(workedHours, career.WorkedHours);
        }

        [Fact]
        public void Should_Add_Worked_Hours()
        {
            // Arrange
            int workedHoursAdded = faker.Random.Int(Rules.MIN_WORKED_HOURS);
            Sut career = Sut.Create(job, jobPositionId);
            Assert.Equal(Rules.MIN_WORKED_HOURS, career.WorkedHours);
            Notification notificationErrors = career.AddWorkedHours(workedHoursAdded);
            Assert.False(notificationErrors.HasErrors());
            Assert.Equal(workedHoursAdded, career.WorkedHours);
        }

        [Fact]
        public void Should_Add_Worked_Hours_Rehydrated()
        {
            // Arrange
            int workedHours = faker.Random.Int(0, 100);
            int workedHoursToAdd = faker.Random.Int(1, 100);
            Sut career = Sut.Rehydrate(job, jobPositionId, workedHours);
            Assert.Equal(workedHours, career.WorkedHours);
            Notification notificationErrors = career.AddWorkedHours(workedHoursToAdd);
            Assert.False(notificationErrors.HasErrors());
            Assert.Equal(workedHours + workedHoursToAdd, career.WorkedHours);
        }

        [Fact]
        public void Should_Add_Return_Error_If_WorkHours_Is_Invalid()
        {
            // Arrange
            int invalidWorkHours = faker.Random.Int(-10, -1);
            Sut career = Sut.Create(job, jobPositionId);
            Notification careerErrors = career.AddWorkedHours(invalidWorkHours);
            Assert.True(careerErrors.HasErrors());
            Assert.Equal(Rules.MIN_WORKED_HOURS, career.WorkedHours);
            Assert.Contains(Errors.WORKED_HOURS_CANNOT_BE_NEGATIVE, careerErrors.GetErrorsByContext("Career"));
        }

    }
}
