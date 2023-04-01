using Moq.AutoMock;
using Nakshatra.HostedServices.Services.Repositories;

namespace Nakshatra.HostedServices.Services.Tests
{
    public class Repository_ProfileRepository
    {
        //[Fact]
        public void GetByIdAsync()
        {
            //Arange

            var id = 10000;

            //Mocks

            var mocker = new AutoMocker();

            //Act

            var repository = mocker.CreateInstance<IProfileRepository>();
            var user = repository.GetByIdAsync(id).Result;

            var actual = user?.PersonalDetails?.Id;

            //Assert

            //actual.Should().Be(id);
        }
    }
}