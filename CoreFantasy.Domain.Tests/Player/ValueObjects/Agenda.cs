using Bogus;
using CoreFantasy.Domain.Player.ValueObjects.Agenda;
using CoreFantasy.Domain.Shared;
using Errors = CoreFantasy.Domain.Player.ValueObjects.Agenda.AgendaErrors;
using PlannedActionSut = CoreFantasy.Domain.Player.ValueObjects.Agenda.PlannedAction;
using Sut = CoreFantasy.Domain.Player.ValueObjects.Agenda.Agenda;

//TODO ADD MORE TESTS
namespace CoreFantasy.Domain.Tests.Player.ValueObjects
{
    public class Agenda
    {
        public Faker faker = new();
        [Fact]
        public void Should_Create_A_Valid_Agenda()
        {
            // Arrange
            List<PlannedActionSut> plannedActions =
            [
                PlannedActionSut.Create(ActionType.Work, 8).PlannedAction,
                PlannedActionSut.Create(ActionType.Study, 8).PlannedAction,
                PlannedActionSut.Create(ActionType.Sleep, 8).PlannedAction,
            ];

            (Sut agenda, Notification notification) = Sut.Create(plannedActions, 3);
            Assert.False(notification.HasErrors());
            Assert.All(plannedActions, pa => Assert.Contains(pa, agenda.PlannedActions));
        }

        [Fact]
        public void Should_Return_Error_If_Total_Hours_Invalid()
        {
            // Arrange
            List<PlannedActionSut> plannedActions =
            [
                PlannedActionSut.Create(ActionType.Work, 8).PlannedAction,
                PlannedActionSut.Create(ActionType.Study, 8).PlannedAction,
            ];

            (Sut agenda, Notification notification) = Sut.Create(plannedActions, 3);
            Assert.Null(agenda);
            Assert.True(notification.HasErrors());
            Assert.Contains(Errors.AGENDA_TOTAL_HOURS_INVALID, notification.GetErrorsByContext("Agenda"));
        }

        [Fact]
        public void Should_Return_Error_If_PlannedAction_Is_Emtpy()
        {
            // Arrange
            (Sut agenda, Notification notification) = Sut.Create(null, 3);
            Assert.Null(agenda);
            Assert.True(notification.HasErrors());
            Assert.Contains(Errors.AGENDA_PLANNED_ACTIONS_EMPTY, notification.GetErrorsByContext("Agenda"));
        }



    }
}
