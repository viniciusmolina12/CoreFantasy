using Bogus;
using Rules = CoreFantasy.Domain.Player.ValueObjects.StatusRules;
using Sut = CoreFantasy.Domain.Player.ValueObjects.Status;

namespace CoreFantasy.Domain.Tests.Player.ValueObjects
{
    public class Status
    {
        public Faker faker = new();
        [Fact]
        public void Should_Create_A_Valid_Status()
        {
            // Arrange
            int health = faker.Random.Number(Rules.MIN_HEALTH, Rules.MAX_HEALTH);
            decimal money = faker.Finance.Amount(0, 1000);
            Sut status = Sut.Create(health, money);
            Assert.Equal(health, status.Health);
            Assert.Equal(money, status.Money);
        }
    }
}
