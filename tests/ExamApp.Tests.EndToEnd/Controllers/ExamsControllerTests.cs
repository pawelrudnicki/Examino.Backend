using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ExamApp.Api;
using ExamApp.Infrastructure.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace ExamApp.Tests.EndToEnd.Controllers
{
    public class ExamsControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public ExamsControllerTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task fetching_exams_should_return_not_empty_collection()
        {
            var response = await _client.GetAsync("exams");
            var content = await response.Content.ReadAsStringAsync();
            var exams = JsonConvert.DeserializeObject<IEnumerable<ExamDto>>(content);

            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
            exams.Should().NotBeEmpty();
        }
    }
}