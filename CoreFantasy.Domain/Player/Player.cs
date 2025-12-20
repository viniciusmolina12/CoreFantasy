using CoreFantasy.Domain.Player.ValueObjects;
using CoreFantasy.Domain.Player.ValueObjects.Agenda;
using CoreFantasy.Domain.Shared;
using CoreFantasy.Domain.User.ValueObjects;

namespace CoreFantasy.Domain.Player
{
    public class PlayerId(Guid Value)
    {
        private Guid Value { get; } = Value;

        public static PlayerId Create()
        {
            return new PlayerId(Guid.NewGuid());
        }

    }

    public class Player(PlayerId playerId, Name name, Age age, Status status, Agenda agenda) : AggregateRoot
    {
        public PlayerId Id { get; private set; } = playerId ?? PlayerId.Create();

        public Name Name { get; private set; } = name;

        public Age Age { get; private set; } = age;

        public Status Status { get; private set; } = status;

        public Agenda Agenda { get; private set; } = agenda;

        public static Player Create(Name name, Age age, Status status, Agenda agenda)
        {
            return new Player(PlayerId.Create(), name, age, status, agenda);
        }

        public void ChangeAge(Age Age)
        {
            this.Age = Age;
        }

        public void ChangeStatus(Status Status)
        {
            this.Status = Status;
        }   
    }
}
