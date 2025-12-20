using CoreFantasy.Domain.Shared;
//TODO - TEST
namespace CoreFantasy.Domain.Player.ValueObjects.Agenda
{

    file record PlannedActionRules
    {
        public static int MIN_HOURS = 1;
        public static int MAX_HOURS = 24;
    }

    public class PlannedAction : ValueObject
    {
        public ActionType ActionType { get; }
        public int Hours { get; }

        private PlannedAction(ActionType actionType, int hours)
        {
            ActionType = actionType;
            Hours = hours;
        }

        public static (PlannedAction PlannedAction, Notification Notification)Create(ActionType actionType, int hours)
        {
            Notification notification = Validate(hours);
            return (new(actionType, hours), notification);
        }

        private static Notification Validate(int hours)
        {
            Notification notification = new();
            if (hours < PlannedActionRules.MIN_HOURS || hours > PlannedActionRules.MAX_HOURS)
            {
                notification.AddError(typeof(PlannedAction).Name, $"Hours must be between {PlannedActionRules.MIN_HOURS} and {PlannedActionRules.MAX_HOURS}.");
            }
            return notification;

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ActionType;
        }
    }

    public record PlannedActionErrors
    {
        public static readonly string PLANNED_ACTION_HOURS_INVALID = $"Hours must be between {PlannedActionRules.MIN_HOURS} and {PlannedActionRules.MAX_HOURS}.";
    }
}
