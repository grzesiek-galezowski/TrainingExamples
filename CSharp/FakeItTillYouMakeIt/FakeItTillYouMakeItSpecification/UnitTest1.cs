using System.Threading.Tasks;
using FluentAssertions;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace FakeItTillYouMakeItSpecification
{
    public class Tests
    {
        [Test]
        public async Task Test1()
        {
            await using var app = new WebApplicationFactory<Program>();
            using var flurlClient = new FlurlClient(app.CreateClient());

            var s = await flurlClient.Request("/hello")
                .SetQueryParam("sentence", "Good morning!")
                .GetStringAsync();

            s.Should().Be("Dzień Dobry!");
        }
    }
}