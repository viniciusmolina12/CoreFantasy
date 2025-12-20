using CoreFantasy.Domain.Shared;

//TODO - TEST
namespace CoreFantasy.Domain.Player.ValueObjects.Agenda
{
    file record AgendaRules
    {
        public static int MAX_TOTAL_HOURS = 24;
    }
    public class Agenda : ValueObject
    {
        public DateTime ValidUntil { get; }
        public List<PlannedAction> PlannedActions { get; }
        private Agenda(DateTime validUntil, List<PlannedAction> plannedActions)
        {
            this.ValidUntil = validUntil;
            this.PlannedActions = plannedActions;
        }
        public static (Agenda Agenda, Notification Notification) Create(DateTime validUntil, List<PlannedAction> plannedActions)
        {
            Notification notification = Validate(validUntil, plannedActions);
            var agenda = notification.HasErrors() ? null : new Agenda(validUntil, plannedActions);
            return (agenda, notification);
        }

        private static Notification Validate(DateTime validUntil, List<PlannedAction> plannedActions)
        {
            Notification notification = new();
            if (validUntil <= DateTime.UtcNow)
            {
                notification.AddError(typeof(Agenda).Name, AgendaErrors.AGENDA_INVALID_VALID_UNTIL);
            }
            if (plannedActions == null || plannedActions.Count == 0)
            {
                notification.AddError(typeof(Agenda).Name, AgendaErrors.AGENDA_PLANNED_ACTIONS_EMPTY);
                return notification;
            }

            int totalHours = plannedActions.Sum(pa => pa.Hours);
            if (totalHours > AgendaRules.MAX_TOTAL_HOURS)
            {
                notification.AddError(typeof(Agenda).Name, AgendaErrors.AGENDA_TOTAL_HOURS_EXCEEDED);
            }

            return notification;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var action in PlannedActions)
            {
                yield return action;
            }
        }
    }

    public record AgendaErrors
    {
        public static readonly string AGENDA_INVALID_VALID_UNTIL = "ValidUntil must be a future date.";
        public static readonly string AGENDA_PLANNED_ACTIONS_EMPTY = "PlannedActions cannot be empty.";
        public static readonly string AGENDA_TOTAL_HOURS_EXCEEDED = $"Total planned action hours cannot exceed {AgendaRules.MAX_TOTAL_HOURS} hours.";
    }


}
