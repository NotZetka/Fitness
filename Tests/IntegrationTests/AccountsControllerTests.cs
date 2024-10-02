using API;
using API.Data;
using API.Data.Dtos;
using API.Handlers.Accounts.List;
using API.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Tests.IntegrationTests
{
    public class AccountsControllerTests(WebApplicationFactory<Program> factory) : BaseControllerTest(factory)
    {

        [Fact]
        public async Task UserShouldRegisterSuccessfully()
        {
            var query = new
            {
                UserName = "Test",
                Password = "Test.123",
                Email = "Test@test.pl",
                Gender = "Male",
                DateOfBirth = DateTime.Now.AddYears(-20),
            };
            _context.Users.Where(x=>x.UserName=="Test").ExecuteDelete();

            var content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/Accounts/Register", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UserShouldntRegisterWhenUsernameIsTaken()
        {
            var query = new
            {
                UserName = "TestInvalid",
                Password = "Test.123",
                Email = "TestInvalid@test.pl",
                Gender = "Male",
                DateOfBirth = DateTime.Now.AddYears(-20),
            };
            _context.Users.Where(x => x.UserName == "TestInvalid").ExecuteDelete();

            var content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/Accounts/Register", content);
            var response2 = await _client.PostAsync("/Accounts/Register", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(HttpStatusCode.Forbidden, response2.StatusCode);
        }

        [Fact]
        public async Task UserShouldLogginSuccessfullyByUsername()
        {
            var query = new
            {
                UsernameOrEmail = "TestLogin",
                Password = "Test.123"
            };
            await InitializeUsers();

            var content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/Accounts/Login", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UserShouldLogginSuccessfullyByEmail()
        {
            var query = new
            {
                UsernameOrEmail = "TestLogin@test.com",
                Password = "Test.123"
            };
            await InitializeUsers();

            var content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/Accounts/Login", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UserShouldRetriveListOfUsers()
        {
            await InitializeUsers();
            var scope = _factory.Services.CreateScope();
            
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var user = await userManager.FindByNameAsync("TestList");
            var token = GenerateJwtToken(user.Id.ToString());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
            var response = await _client.GetAsync("/Accounts/List");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var accountsList = JsonConvert.DeserializeObject<PagedResult<UserDto>>(stringResponse);

            Assert.NotNull(accountsList);
            Assert.False(accountsList.Items.Select(x=>x.UserName).Contains("TestList"));
        }

        private async Task InitializeUsers()
        {
            using var scope = _factory.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            if (await userManager.FindByNameAsync("TestLogin") == null)
            {
                var user = new AppUser
                {
                    UserName = "TestLogin",
                    Email = "TestLogin@test.com",
                    Gender = "Male",
                    DateOfBirth = DateTime.Now.AddYears(-20),
                };
                await userManager.CreateAsync(user, "Test.123");
            }
            if (await userManager.FindByNameAsync("TestList") == null)
            {
                var user = new AppUser
                {
                    UserName = "TestList",
                    Email = "TestList@test.com",
                    Gender = "Male",
                    DateOfBirth = DateTime.Now.AddYears(-20),
                };
                await userManager.CreateAsync(user, "Test.123");
            }
        }

    }
}
