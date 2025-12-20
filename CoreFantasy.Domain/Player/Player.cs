using CoreFantasy.Domain.Job;
using CoreFantasy.Domain.Player.ValueObjects;
using CoreFantasy.Domain.Player.ValueObjects.Agenda;
using CoreFantasy.Domain.Shared;
using CoreFantasy.Domain.User.ValueObjects;

namespace CoreFantasy.Domain.Player
{
    public class PlayerId
    {
        public Guid Value { get; }

        private PlayerId(Guid value)
        {
            Value = value;
        }

        public static PlayerId Create()
        {
            return new PlayerId(Guid.NewGuid());
        }

        public static PlayerId From(Guid value)
        {
            return new PlayerId(value);
        }
    }

    public class Player : AggregateRoot
    {
        public PlayerId Id { get; private set; }
        public Name Name { get; private set; }
        public Age Age { get; private set; }
        public Status Status { get; private set; }
        public Agenda Agenda { get; private set; }
        public Career Career { get; private set; }


        private Player(
            PlayerId playerId,
            Name name,
            Age age,
            Status status,
            Agenda agenda,
            JobId jobId,
            JobPositionId jobPositionId
            )
        {
            Id = playerId;
            Name = name;
            Age = age;
            Status = status;
            Agenda = agenda;
            Career = Career.Create(jobId, jobPositionId); 
        }

        public static Player Create(
            Name name,
            Age age,
            Status status,
            Agenda agenda,
            JobId jobId,
            JobPositionId jobPositionId
            )
        {
            return new Player(
                PlayerId.Create(),
                name,
                age,
                status,
                agenda,
                jobId,
                jobPositionId
            );
        }

        private Player(){}

        internal static Player Rehydrate(
            PlayerId playerId,
            Name name,
            Age age,
            Status status,
            Agenda agenda,
            Career career)
        {
            return new Player
            {
                Id = playerId,
                Name = name,
                Age = age,
                Status = status,
                Agenda = agenda,
                Career = career
            };
        }

        public void ChangeAge(Age age)
        {
            Age = age;
        }

        public void ChangeStatus(Status status)
        {
            Status = status;
        }

        public void ChangeCareer(JobId jobId, JobPositionId jobPositionId)
        {
            Career = Career.Create(jobId, jobPositionId); 
        }
    }
}
