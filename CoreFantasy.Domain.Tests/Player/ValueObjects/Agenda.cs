using Bogus;
using CoreFantasy.Domain.Player.ValueObjects.Agenda;
using CoreFantasy.Domain.Shared;
using Errors = CoreFantasy.Domain.Player.ValueObjects.Agenda.AgendaErrors;
using PlannedActionSut = CoreFantasy.Domain.Player.ValueObjects.Agenda.PlannedAction;
using Sut = CoreFantasy.Domain.Player.ValueObjects.Agenda.Agenda;

namespace CoreFantasy.Domain.Tests.Player
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

            (Sut agenda, Notification notification) = Sut.Create(DateTime.Now.AddDays(1), plannedActions);
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

            (Sut agenda, Notification notification) = Sut.Create(DateTime.Now.AddDays(1), plannedActions);
            Assert.Null(agenda);
            Assert.True(notification.HasErrors());
            Assert.Contains(Errors.AGENDA_TOTAL_HOURS_INVALID, notification.GetErrorsByContext("Agenda"));
        }

        [Fact]
        public void Should_Return_Error_If_PlannedAction_Is_Emtpy()
        {
            // Arrange
            (Sut agenda, Notification notification) = Sut.Create(DateTime.Now.AddDays(1), null);
            Assert.Null(agenda);
            Assert.True(notification.HasErrors());
            Assert.Contains(Errors.AGENDA_PLANNED_ACTIONS_EMPTY, notification.GetErrorsByContext("Agenda"));
        }

        [Fact]
        public void Should_Return_Error_If_Valid_Until_Invalid()
        {
            // Arrange
           List<PlannedActionSut> plannedActions =
           [
               PlannedActionSut.Create(ActionType.Work, 8).PlannedAction,
                PlannedActionSut.Create(ActionType.Study, 8).PlannedAction,
                PlannedActionSut.Create(ActionType.Sleep, 8).PlannedAction,
            ];
            DateTime invalid_valid_until = DateTime.UtcNow.AddHours(-1);
            (Sut agenda, Notification notification) = Sut.Create(invalid_valid_until, plannedActions);
            Assert.Null(agenda);
            Assert.True(notification.HasErrors());
            Assert.Contains(Errors.AGENDA_INVALID_VALID_UNTIL, notification.GetErrorsByContext("Agenda"));
        }


    }
}
