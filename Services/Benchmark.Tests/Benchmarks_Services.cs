using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using Nakshatra.Services.Api.Model.Blog;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Nakshatra.Benchmark.Tests
{
    public class Benchmarks_Services : IClassFixture<TestBase>
    {
        private readonly ITestOutputHelper _output;
        public Benchmarks_Services(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact(Skip = "Only for becnhmarking")]
        public void PerformanceBloggerBlogs()
        {
            var logger = new AccumulationLogger();

            var config = ManualConfig.Create(DefaultConfig.Instance)
              .AddLogger(logger)
              .WithOptions(ConfigOptions.DisableOptimizationsValidator);

            BenchmarkRunner.Run<MockBloggerService>(config);

            // write benchmark summary
            var log = logger.GetLog();
            _output.WriteLine(log);
        }
    }

    [MinColumn, MaxColumn]
    [MemoryDiagnoser]
    [SimpleJob(RunStrategy.ColdStart, launchCount: 3, warmupCount: 0, iterationCount: 3)]
    public class MockBloggerService
    {
        public string? Url { get; set; }
        [GlobalSetup]
        public void GlobalSetup()
        {
            Url = $"https://www.googleapis.com/blogger/v3/blogs/" +
               $"/" +
               $"posts?key=" +
               $"&maxResults=200";
        }

        [Benchmark]
        public BlogDetails? BlogDetails()
        {
            BlogDetails? blogDetails = null;
            using (var httpClient = new HttpClient())
            {
                using var response = httpClient.GetAsync(Url);
                string apiResponse = response.Result.Content.ReadAsStringAsync().Result;
                blogDetails = JsonConvert.DeserializeObject<BlogDetails>(apiResponse);
            }
            return blogDetails;
        }
    }
}
