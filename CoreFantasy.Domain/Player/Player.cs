using CoreFantasy.Domain.Course;
using CoreFantasy.Domain.Job;
using CoreFantasy.Domain.Player.Entities;
using CoreFantasy.Domain.Player.ValueObjects;
using CoreFantasy.Domain.Player.ValueObjects.Agenda;
using CoreFantasy.Domain.User.ValueObjects;
using CoreFantasy.Domain.Shared;

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
        public Career? Career { get; private set; }
        public Education? Education { get; private set; }
        public bool Alive { get; private set; }


        private Player(
            PlayerId playerId,
            Name name,
            Age age,
            Status status,
            Agenda agenda,
            JobId jobId,
            JobPositionId jobPositionId,
            CourseId courseId
            )
        {
            Id = playerId;
            Name = name;
            Age = age;
            Status = status;
            Agenda = agenda;
            Career = Career.Create(jobId, jobPositionId);
            Education = Education.Create(courseId);
            Alive = true;
        }

        public static Player Create(
            Name name,
            Age age,
            Status status,
            Agenda agenda,
            JobId jobId,
            JobPositionId jobPositionId,
            CourseId courseId
            )
        {
            return new Player(
                PlayerId.Create(),
                name,
                age,
                status,
                agenda,
                jobId,
                jobPositionId,
                courseId
            );
        }

        private Player() { }

        internal static Player Rehydrate(
            PlayerId playerId,
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

        public void ChangeCareer(JobId jobId, JobPositionId jobPositionId)
        {
            if (this.Alive)
            {
                this.Career = Career.Create(jobId, jobPositionId);
                Touch();
            }
        }

        public void ChangeEducation(CourseId courseId)
        {
            if (this.Alive) {
                Education = Education.Create(courseId);
                Touch();
            }
        }

        public void MarkAsDeceased()
        {
            this.Career = null;
            this.Education = null;
            Alive = false;
            Touch();
        }
    }
}
