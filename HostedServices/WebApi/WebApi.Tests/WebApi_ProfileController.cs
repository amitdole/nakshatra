using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Nakshatra.HostedServices.WebApi.Api.Entities.Profile;
using Nakshatra.HostedServices.WebApi.Tests;
using Nakshatra.HostedServices.WebApi.Web.Controllers;
using Xunit;

namespace Nakshatra.WebApi.HostedServices.Tests;

public class WebApi_Profile : IClassFixture<TestBase>
{
    private ServiceProvider _serviceProvider;
    private ProfileController? _profileController;

    public WebApi_Profile(TestBase testBase)
    {
        _serviceProvider = testBase.ServiceProvider;
        _profileController = _serviceProvider.GetService<ProfileController>();
    }

    [Fact]
    public void ListAllProfiles()
    {
        //Arange

        var expectedMinimumCount = 1;

        //Act

        var response = _profileController?.List().Result;

        //Assert

        var okObjectResult = response.Should().BeOfType<OkObjectResult>().Subject;

        var result = okObjectResult.Value.Should().BeAssignableTo<IReadOnlyList<Profile>>();

        var actualProfiles = result.Subject;

        actualProfiles.Count().Should().BeGreaterThanOrEqualTo(expectedMinimumCount);
    }

    [Fact]
    public void GetProfileById()
    {
        //Arange

        var expectedProfileId = 10001;

        //Act

        var response = _profileController?.Get(expectedProfileId).Result;

        //Assert

        var okObjectResult = response.Should().BeOfType<OkObjectResult>().Subject;

        var result = okObjectResult.Value.Should().BeAssignableTo<Profile>();

        var actualProfileId = result.Subject.Id;

        actualProfileId.Should().Be(expectedProfileId);
    }
}