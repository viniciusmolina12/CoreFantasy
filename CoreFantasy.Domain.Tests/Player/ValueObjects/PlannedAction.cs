using Bogus;
using CoreFantasy.Domain.Player.ValueObjects.Agenda;
using CoreFantasy.Domain.Shared;
using Errors = CoreFantasy.Domain.Player.ValueObjects.Agenda.PlannedActionErrors;
using Rules = CoreFantasy.Domain.Player.ValueObjects.Agenda.PlannedActionRules;
using Sut = CoreFantasy.Domain.Player.ValueObjects.Agenda.PlannedAction;

namespace CoreFantasy.Domain.Tests.Player
{
    public class PlannedAction
    {
        public Faker faker = new();
        [Fact]
        public void Should_Create_A_Valid_PlannedAction()
        {
            // Arrange
            ActionType actionType = ActionType.Work;
            (Sut plannedAction, Notification notification) = Sut.Create(ActionType.Work, 5);
            Assert.False(notification.HasErrors());
            Assert.Equal(ActionType.Work, plannedAction.ActionType);
            Assert.Equal(5, plannedAction.Hours);
        }

        [Fact]
        public void Should_Return_Error_If_Hours_Exceed_Maximum()
        {
            // Arrange
            int invalid_hours = Rules.MAX_HOURS + 1;
            (Sut plannedAction, Notification notification) = Sut.Create(ActionType.Work, invalid_hours);
            Assert.Null(plannedAction);
            Assert.True(notification.HasErrors());
            Assert.Contains(Errors.PLANNED_ACTION_HOURS_INVALID, notification.GetErrorsByContext("PlannedAction"));
        }

        [Fact]
        public void Should_Return_Error_If_Hours_Less_Than_Minimal()
        {
            // Arrange
            int invalid_hours = Rules.MIN_HOURS - 1;
            (Sut plannedAction, Notification notification) = Sut.Create(ActionType.Work, invalid_hours);
            Assert.Null(plannedAction);
            Assert.True(notification.HasErrors());
            Assert.Contains(Errors.PLANNED_ACTION_HOURS_INVALID, notification.GetErrorsByContext("PlannedAction"));
        }

        [Fact]
        public void Should_Return_Error_If_ActionType_Is_Invalid()
        {
            // Arrange
            (Sut plannedAction, Notification notification) = Sut.Create(null, 1);
            Assert.Null(plannedAction);
            Assert.True(notification.HasErrors());
            Assert.Contains(Errors.PLANNED_ACTION_ACTION_TYPE_INVALID, notification.GetErrorsByContext("PlannedAction"));
        }

    }
}
