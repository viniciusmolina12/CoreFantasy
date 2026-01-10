using CoreFantasy.Domain.Course;
using CoreFantasy.Domain.Player.Entities;
using CoreFantasy.Domain.Player.ValueObjects;
using CoreFantasy.Domain.Player.ValueObjects.Agenda;
using CoreFantasy.Domain.Shared;
using CoreFantasy.Domain.User;
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
        public UserId UserId { get; private set; }
        public Name Name { get; private set; }
        public Age Age { get; private set; }
        public Status Status { get; private set; }
        public Agenda? Agenda { get; private set; }
        public Career? Career { get; private set; }
        public Education? Education { get; private set; }
        public bool Alive { get; private set; }


        private Player(
            PlayerId playerId,
            UserId userId,
            Name name,
            Age age,
            Status status
            )
        {
            Id = playerId;
            UserId = userId;
            Name = name;
            Age = age;
            Status = status;
            Alive = true;
        }

        public static Player Create(
            Name name,
            UserId userId,
            Age age,
            Status status
            )
        {
            return new Player(
                PlayerId.Create(),
                userId,
                name,
                age,
                status
            );
        }

        private Player() { }

        internal static Player Rehydrate(
            PlayerId playerId,
            UserId userId,
            Name name,
            Age age,
            Status status,
            Agenda agenda,
            Career career,
            Education education,
            bool alive
            )
        {
            return new Player
            {
                Id = playerId,
                UserId = userId,
                Name = name,
                Age = age,
                Status = status,
                Agenda = agenda,
                Career = career,
                Education = education,
                Alive = alive
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

        public void StartCareer(Career career)
        {
            if (this.Alive)
            {
                this.Career = career;
                Touch();
            }
        }

        public void Enroll(Education education)
        {
            if (this.Alive) {
                Education = education;
                Touch();
            }
        }

        public void Work(int workHours)
        {
            //TODO ADD MONEY IMPLEMENTATION
            this.DecreaseHealthStatus(workHours);
            if (this.Alive)
            {
                this.Career?.AddWorkedHours(workHours);
                decimal earnings = this.Career.CalculateEarnings(workHours);
                this.AddMoney(earnings);
                Touch();
            }
        }

        public void Study(int studyHours)
        {
            this.DecreaseHealthStatus(studyHours);
            if (this.Alive)
            {
                decimal educationCost = this.Education.CalculateEducationCost(studyHours);
                this.SubtractMoney(educationCost);
                this.Education?.UpdateCourseProgress(studyHours);
                Touch();
            }
        }

        public void Sleep(int sleepHours)
        {
            this.IncreaseHealthStatus(sleepHours);
            Touch();
        }

        private void DecreaseHealthStatus(int damage)
        {
            this.Status = Status.Create(damage, this.Status.Money);
            TryMarkAsDeceased();
        }

        private void AddMoney(decimal money)
        {
            decimal currentMoney = this.Status.Money;
            decimal totalMoney = currentMoney + money;
            this.Status = Status.Create(this.Status.Health, totalMoney);
        }

        private void SubtractMoney(decimal money)
        {
            decimal currentMoney = this.Status.Money;
            decimal totalMoney = currentMoney - money;
            this.Status = Status.Create(this.Status.Health, totalMoney);
        }

        private void IncreaseHealthStatus(int recovery)
        {
            this.Status = Status.Create(recovery, this.Status.Money);
        }
        private void TryMarkAsDeceased()
        {
            if (this.Status.Health == 0)
            {
                this.MarkAsDeceased();
            }
        }

        private void MarkAsDeceased()
        {
            this.Career = null;
            this.Education = null;
            this.Agenda = null;
            Alive = false;
        }
    }
}
