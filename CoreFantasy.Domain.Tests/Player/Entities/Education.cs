using Bogus;
using CoreFantasy.Domain.Course;
using Rules = CoreFantasy.Domain.Player.Entities.EducationRules;
using Sut = CoreFantasy.Domain.Player.Entities.Education;


namespace CoreFantasy.Domain.Tests.Player.Entities
{
    public class Education
    {
        public Faker faker = new();

        public readonly CourseId courseId = CourseId.Create("any_course_id");

        [Fact]
        public void Should_Create_A_Valid_Education()
        {
            // Arrange
            Sut education = Sut.Create(courseId);
            Assert.Equal(courseId, education.CourseId);
            Assert.Equal(Rules.MIN_COURSE_PROGRESS, education.Progress);
        }

        [Fact]
        public void Should_Rehydrated_A_Education_Correctly()
        {
            // Arrange
            int progress = faker.Random.Int(Rules.MIN_COURSE_PROGRESS, Rules.MAX_COURSE_PROGRESS);
            Sut education = Sut.Rehydrate(courseId, progress);
            Assert.Equal(courseId, education.CourseId);
            Assert.Equal(progress, education.Progress);
        }

        [Fact]
        public void Should_Add_Progress()
        {
            // Arrange
            int progress = faker.Random.Int(Rules.MIN_COURSE_PROGRESS, Rules.MAX_COURSE_PROGRESS);
            Sut education = Sut.Create(courseId);
            education.UpdateCourseProgress(progress);
            Assert.Equal(progress, education.Progress);

        }

        [Fact]
        public void Should_Add_Progress_Rehydrated()
        {
            // Arrange
            int progress = faker.Random.Int(Rules.MIN_COURSE_PROGRESS, Rules.MAX_COURSE_PROGRESS);
            int progressToAdd = faker.Random.Int(Rules.MIN_COURSE_PROGRESS, Rules.MAX_COURSE_PROGRESS);
            int totalProgress = progress + progressToAdd;
            Sut education = Sut.Rehydrate(courseId, progress);
            education.UpdateCourseProgress(progressToAdd);
            Assert.Equal(totalProgress, education.Progress);
        }

        [Fact]
        public void Should_Not_Add_Progress_If_Progress_Is_Less_Than_Minimal()
        {
            // Arrange
            int invalid_progress = Rules.MIN_COURSE_PROGRESS - 1;
            Sut education = Sut.Create(courseId);
            education.UpdateCourseProgress(invalid_progress);
            Assert.Equal(Rules.MIN_COURSE_PROGRESS, education.Progress);
        }

        [Fact]
        public void Should_Not_Add_Progress_If_Progress_Exceed_Maximum()
        {
            // Arrange
            int invalid_progress = Rules.MAX_COURSE_PROGRESS + 1;
            Sut education = Sut.Create(courseId);
            education.UpdateCourseProgress(invalid_progress);
            Assert.Equal(Rules.MAX_COURSE_PROGRESS, education.Progress);
        }
    }
}
