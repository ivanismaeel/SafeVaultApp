using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using SafeVaultApp;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace SafeVaultApp.Tests
{
    [TestFixture]
    public class AuthTests
    {
        private WebApplicationFactory<Program> _factory;

        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseStartup<Startup>();
                    builder.UseSetting(WebHostDefaults.ContentRootKey, System.IO.Directory.GetCurrentDirectory());
                    builder.UseEnvironment("Testing");
                });
        }

        [Test]
        public async Task TestInvalidLogin()
        {
            var client = _factory.CreateClient();
            var response = await client.PostAsync("/Account/Login", new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "Username", "invaliduser" },
                { "Password", "invalidpassword" }
            }));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task TestUserRegistrationProcess()
        {
            var client = _factory.CreateClient();
            var response = await client.PostAsync("/Account/Register", new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "Username", "newuser" },
                { "Email", "newuser@example.com" },
                { "Password", "P@ssw0rd!" },
                { "ConfirmPassword", "P@ssw0rd!" }
            }));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task TestAccessControl()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/Admin/Index");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Redirect));
        }
    }
}
