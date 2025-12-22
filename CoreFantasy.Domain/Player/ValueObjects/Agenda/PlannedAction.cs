using CoreFantasy.Domain.Shared;
//TODO - TEST
namespace CoreFantasy.Domain.Player.ValueObjects.Agenda
{

    public record PlannedActionRules
    {
        public readonly static int MIN_HOURS = 1;
        public readonly static int MAX_HOURS = 24;
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
            Notification notification = Validate(actionType, hours);
            var plannedAction = notification.HasErrors() ? null : new PlannedAction(actionType, hours);
            return (plannedAction, notification);
        }

        private static Notification Validate(ActionType actionType, int hours)
        {
            Notification notification = new();
            if(actionType is null)
            {
                notification.AddError(typeof(PlannedAction).Name, PlannedActionErrors.PLANNED_ACTION_ACTION_TYPE_INVALID);
            }
            if (hours < PlannedActionRules.MIN_HOURS || hours > PlannedActionRules.MAX_HOURS)
            {
                notification.AddError(typeof(PlannedAction).Name, PlannedActionErrors.PLANNED_ACTION_HOURS_INVALID);
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
        public static readonly string PLANNED_ACTION_ACTION_TYPE_INVALID = "Invalid ActionType";
    }
}
