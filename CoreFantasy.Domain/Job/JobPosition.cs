using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFantasy.Domain.Job
{
    public class JobPositionId(Guid Value)
    {
        private Guid Value { get; } = Value;

        public static JobPositionId Create()
        {
            return new JobPositionId(Guid.NewGuid());
        }

    }
    public class JobPosition
    {
    }
}
