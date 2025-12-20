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

    class Job
    {
    }
}
