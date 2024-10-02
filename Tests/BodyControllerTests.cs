using API;
using API.Data;
using API.Handlers.BodyWeight.AddBodyWeightRecord;
using API.Handlers.BodyWeight.SetHeight;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace IntegrationTests
{
    public class BodyControllerTests(WebApplicationFactory<Program> factory) : BaseControllerTest(factory)
    {

        [Fact]
        public async Task UserShouldSetHeightProperly()
        {
            await InitializeUsers();
            var user = _context.Users.Include(x => x.BodyWeight).First(x => x.UserName == "TestBodyWeight");
            var query = new SetHeightCommand { Height = 110 };
            var content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");
            var token = GenerateJwtToken(user.Id.ToString());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PutAsync("/Body", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var userAfterChanges = GetNewContext().Users.Include(x => x.BodyWeight).First(x => x.UserName == "TestBodyWeight");

            Assert.Equal(110, userAfterChanges.BodyWeight.Height);
        }

        [Fact]
        public async Task UserShouldAddBodyWeightWithOnlyWeight()
        {
            await InitializeUsers();
            var user = _context.Users.Include(x => x.BodyWeight).First(x => x.UserName == "TestBodyWeight");
            var query = new AddBodyWeightRecordCommand { Weight = 110 };
            var content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");
            var token = GenerateJwtToken(user.Id.ToString());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PostAsync("/Body", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var userAfterChanges = GetNewContext()
                .Users
                .Include(x => x.BodyWeight)
                .ThenInclude(x => x.WeightRecords)
                .First(x => x.UserName == "TestBodyWeight");

            var weightRecord = userAfterChanges.BodyWeight.WeightRecords.Last();
            Assert.Equal(110, weightRecord.Weight);
        }

        [Fact]
        public async Task UserShouldAddBodyWeightWithFullData()
        {
            await InitializeUsers();
            var user = _context.Users.Include(x => x.BodyWeight).First(x => x.UserName == "TestBodyWeight");
            var query = new AddBodyWeightRecordCommand
            {
                Weight = 110,
                Neck = 10,
                Chest = 10,
                Arm = 10,
                Forearm = 10,
                Waist = 10,
                Hip = 10,
                Thigh = 10,
                Calf = 10
            };
            var content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");
            var token = GenerateJwtToken(user.Id.ToString());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PostAsync("/Body", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var userAfterChanges = GetNewContext()
                .Users
                .Include(x => x.BodyWeight)
                .ThenInclude(x => x.WeightRecords)
                .First(x => x.UserName == "TestBodyWeight");

            var weightRecord = userAfterChanges.BodyWeight.WeightRecords.Last();
            Assert.Equal(110, weightRecord.Weight);
            Assert.Equal(10, weightRecord.Neck);
            Assert.Equal(10, weightRecord.Chest);
            Assert.Equal(10, weightRecord.Arm);
            Assert.Equal(10, weightRecord.Forearm);
            Assert.Equal(10, weightRecord.Waist);
            Assert.Equal(10, weightRecord.Hip);
            Assert.Equal(10, weightRecord.Thigh);
            Assert.Equal(10, weightRecord.Calf);
        }

        [Fact]
        public async Task TestShouldFailWithNoWeight()
        {
            await InitializeUsers();
            var user = _context.Users.Include(x => x.BodyWeight).First(x => x.UserName == "TestBodyWeight");
            var query = new AddBodyWeightRecordCommand();
            var content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");
            var token = GenerateJwtToken(user.Id.ToString());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PostAsync("/Body", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        private async Task InitializeUsers()
        {
            using var scope = _factory.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            if (await userManager.FindByNameAsync("TestBodyWeight") == null)
            {
                var user = new AppUser
                {
                    UserName = "TestBodyWeight",
                    Email = "TestBodyWeight@test.com",
                    Gender = "Male",
                    DateOfBirth = DateTime.Now.AddYears(-20),
                };
                user.BodyWeight.Height = 100;
                await userManager.CreateAsync(user, "Test.123");
            }
        }
    }
}
