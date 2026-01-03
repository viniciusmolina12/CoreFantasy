using CoreFantasy.Domain.Shared;

namespace CoreFantasy.Domain.Player.ValueObjects.Agenda
{
    public record AgendaRules
    {
        public readonly static int TOTAL_HOURS = 24;
    }
    public class Agenda 
    {
        public List<PlannedAction> PlannedActions { get; }
        public int TotalDaysRemaining { get; private set; }
        public DateTime LastProcessedAt { get; private set; }

     

        private Agenda(List<PlannedAction> plannedActions, int totalDaysRemaining)
        {
            this.PlannedActions = plannedActions;
            TotalDaysRemaining = totalDaysRemaining;
        }

        public static (Agenda Agenda, Notification Notification) Create(List<PlannedAction> plannedActions, int totalDaysRemaining)
        {
            Notification notification = Validate(plannedActions, totalDaysRemaining);
            var agenda = notification.HasErrors() ? null : new Agenda(plannedActions, totalDaysRemaining);
            return (agenda, notification);
        }

        private static Notification Validate(List<PlannedAction> plannedActions, int totalDaysRemaining)
        {
            Notification notification = new();
            if (totalDaysRemaining < 0)
            {
                notification.AddError(typeof(Agenda).Name, AgendaErrors.AGENDA_INVALID_TOTAL_DAYS_REMAINING);
            }
            if (plannedActions == null || plannedActions.Count == 0)
            {
                notification.AddError(typeof(Agenda).Name, AgendaErrors.AGENDA_PLANNED_ACTIONS_EMPTY);
                return notification;
            }

            int totalHours = plannedActions.Sum(pa => pa.Hours);
            if (totalHours != AgendaRules.TOTAL_HOURS)
            {
                notification.AddError(typeof(Agenda).Name, AgendaErrors.AGENDA_TOTAL_HOURS_INVALID);
            }

            return notification;
        }


        public void ProcessDay(Player player, int gameDays)
        {
            if (this.TotalDaysRemaining <= 0) return;

            for(int i = 0; i < gameDays; i++)
            {
                foreach (var plannedAction in PlannedActions)
                {
                    if (_actions.TryGetValue(plannedAction.ActionType, out var action))
                    {
                        action(player, plannedAction.Hours);
                    }
                }
            }
         

            this.TotalDaysRemaining -= 1;
            LastProcessedAt = DateTime.UtcNow;
        }

        private static readonly Dictionary<ActionType, Action<Player, int>> _actions =
         new()
         {
                { ActionType.Work, (player, hours) => player.Work(hours) },
                { ActionType.Study, (player, hours) => player.Study(hours) },
                { ActionType.Sleep, (player, hours) => player.Sleep(hours) }
         };

       


    }

    public record AgendaErrors
    {
        public static readonly string AGENDA_INVALID_TOTAL_DAYS_REMAINING = "Total days remaining must be positive";
        public static readonly string AGENDA_PLANNED_ACTIONS_EMPTY = "PlannedActions cannot be empty.";
        public static readonly string AGENDA_TOTAL_HOURS_INVALID = $"Total planned action hours should be {AgendaRules.TOTAL_HOURS} hours.";
    }


}
