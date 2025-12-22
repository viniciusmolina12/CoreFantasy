using Bogus;
using CoreFantasy.Domain.Job;
using CoreFantasy.Domain.Shared;
using Errors = CoreFantasy.Domain.Player.Entities.CareerErrors;
using Rules = CoreFantasy.Domain.Player.Entities.CareerRules;
using Sut = CoreFantasy.Domain.Player.Entities.Career;


namespace CoreFantasy.Domain.Tests.Player.Entities
{
    public class Agenda
    {
        public Faker faker = new();

        public readonly JobId jobId = JobId.Create();
        public readonly JobPositionId jobPositionId = JobPositionId.Create();

        [Fact]
        public void Should_Create_A_Valid_Career()
        {
            // Arrange
 
            Sut career = Sut.Create(jobId, jobPositionId);
            Assert.Equal(jobId, career.JobId);
            Assert.Equal(jobPositionId, career.JobPositionId);
            Assert.Equal(Rules.MIN_WORKED_HOURS, career.WorkedHours);
        }

        [Fact]
        public void Should_Rehydrated_A_Career_Correctly()
        {
            // Arrange
            int workedHours = faker.Random.Int(0, 100);
            Sut career = Sut.Rehydrate(jobId, jobPositionId, workedHours);
            Assert.Equal(jobId, career.JobId);
            Assert.Equal(jobPositionId, career.JobPositionId);
            Assert.Equal(workedHours, career.WorkedHours);
        }

        [Fact]
        public void Should_Add_Worked_Hours()
        {
            // Arrange
            int workedHoursAdded = faker.Random.Int(0, 100);
            Sut career = Sut.Create(jobId, jobPositionId);
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
            Sut career = Sut.Rehydrate(jobId, jobPositionId, workedHours);
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
            Sut career = Sut.Create(jobId, jobPositionId);
            Notification careerErrors = career.AddWorkedHours(invalidWorkHours);
            Assert.True(careerErrors.HasErrors());
            Assert.Equal(Rules.MIN_WORKED_HOURS, career.WorkedHours);
            Assert.Contains(Errors.WORKED_HOURS_CANNOT_BE_NEGATIVE, careerErrors.GetErrorsByContext("Career"));
        }

    }
}
