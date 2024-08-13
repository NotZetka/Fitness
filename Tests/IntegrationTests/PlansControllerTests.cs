using API;
using API.Data;
using API.Data.Dtos;
using API.Handlers.Plans.AddRecord;
using API.Handlers.Plans.GetPlanTemplates;
using API.Handlers.Plans.Publish;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using static API.Handlers.Plans.AddRecord.AddRecordsQuery;

namespace Tests.IntegrationTests
{
    public class PlansControllerTests(WebApplicationFactory<Program> factory) : BaseControllerTest(factory)
    {
        [Fact]
        public async Task UserShouldGetPlanTemaplate()
        {
            await InitializePlans();
            var user = _context.Users.First();
            var token = GenerateJwtToken(user.Id.ToString());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync("/Plans/Templates");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var plansList = JsonConvert.DeserializeObject<GetPlanTemplatesQueryResult>(stringResponse);

            Assert.NotNull(plansList);
            Assert.Equal(2, plansList.Plans.Count());
        }

        [Fact]
        public async Task UserShouldPublishPlan()
        {
            await InitializePlans();
            var user = _context.Users.First();
            var token = GenerateJwtToken(user.Id.ToString());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var query = new PublishPlanQuery()
            {
                Name = "PublishPlanTest",
                Exercises = new List<ExerciseTemplateDto>()
                {
                    new()
                    {
                        Name = "Test exercise",
                        Sets = 2,
                        Description = "Description"
                    }
                }
            };
            _context.FitnessPlanTemplates.Where(x=>x.Name==query.Name).ExecuteDelete();
            var content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/Plans/Publish", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UserShouldAddPlan()
        {
            await InitializePlans();
            var user = _context.Users.First(x=>x.UserName== "TestUserPlan");
            var token = GenerateJwtToken(user.Id.ToString());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var planId = _context.FitnessPlanTemplates.Where(x=>x.AuthorId==user.Id).First().Id;

            var response = await _client.GetAsync($"/Plans/Add/{planId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var userAfterChanges = GetNewContext().Users.Include(x=>x.FitnessPlans).FirstOrDefault(x=>x.Id == user.Id);
            Assert.Equal(2,userAfterChanges.FitnessPlans.Count);
        }

        [Fact]
        public async Task UserShouldAddRecord()
        {
            await InitializePlans();
            var user = _context.Users
                .Include(x=>x.FitnessPlans)
                .ThenInclude(x=>x.Exercises)
                .First(x => x.UserName == "TestUserPlan");
            var token = GenerateJwtToken(user.Id.ToString());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var query = new AddRecordsQuery
            {
                Records = new List<QueryRecord>
                {
                    new()
                    {
                        ExerciseId = user.FitnessPlans.First().Exercises.First().Id,
                        Weight = 10,
                        Repetitions = 2
                    }
                }
            };
            var content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"/Plans/Add",content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UserShouldGetPlans()
        {
            await InitializePlans();
            var user = _context.Users
                .Include(x => x.FitnessPlans)
                .First(x => x.UserName == "TestUserPlan");
            var token = GenerateJwtToken(user.Id.ToString());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"/Plans");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var userAfterChanges = GetNewContext().Users.Include(x => x.FitnessPlans).FirstOrDefault(x => x.Id == user.Id);
            Assert.Single(userAfterChanges.FitnessPlans);
        }

        [Fact]
        public async Task UserShouldGetSinglePlan()
        {
            await InitializePlans();
            var user = _context.Users
                .Include(x => x.FitnessPlans)
                .First(x => x.UserName == "TestUserPlan");
            var token = GenerateJwtToken(user.Id.ToString());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var planId = user.FitnessPlans.First().Id;

            var response = await _client.GetAsync($"/Plans/{planId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        [Fact]
        public async Task UserShouldArchivePlan()
        {
            await InitializePlans();
            var user = _context.Users
                .Include(x => x.FitnessPlans)
                .First(x => x.UserName == "TestUserPlan");
            var token = GenerateJwtToken(user.Id.ToString());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var plan = user.FitnessPlans.First();

            var response = await _client.PatchAsync($"/Plans/archive/{plan.Id}",null);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var planAfterChanges = GetNewContext().FitnessPlans.First(x=>x.Id==plan.Id);
            Assert.NotEqual(plan.Archived, planAfterChanges.Archived);
        }

        [Fact]
        public async Task UserShouldChangeVisability()
        {
            await InitializePlans();
            var user = _context.Users
                .Include(x => x.FitnessPlansTemplates)
                .First(x => x.UserName == "TestUserPlan");
            var token = GenerateJwtToken(user.Id.ToString());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var plan = user.FitnessPlansTemplates.First();

            var response = await _client.PatchAsync($"/Plans/visibility/{plan.Id}", null);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var planAfterChanges = GetNewContext().FitnessPlanTemplates.First(x => x.Id == plan.Id);
            Assert.NotEqual(plan.Public, planAfterChanges.Public);
        }

        private async Task InitializePlans()
        {
            _context.FitnessPlans.ExecuteDelete();
            _context.FitnessPlanTemplates.ExecuteDelete();
            using var scope = _factory.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            if (await userManager.FindByNameAsync("TestUserPlan") == null)
            {
                var user = new AppUser
                {
                    UserName = "TestUserPlan",
                    Email = "TestUserPlan@test.com",
                    Gender = "Male",
                    DateOfBirth = DateTime.Now.AddYears(-20),
                };
                await userManager.CreateAsync(user, "Test.123");
            }
            if (await userManager.FindByNameAsync("TestUserPlan2") == null)
            {
                var user = new AppUser
                {
                    UserName = "TestUserPlan2",
                    Email = "TestUserPlan2@test.com",
                    Gender = "Male",
                    DateOfBirth = DateTime.Now.AddYears(-20),
                };
                await userManager.CreateAsync(user, "Test.123");
            }
            var planTemplate = new FitnessPlanTemplate
            {
                Name = "TestPlan",
                Exercises = new List<ExerciseTemplate>()
                {
                    new()
                    {
                        Name = "Test exercise",
                        Sets = 2,
                        Description = "Description"
                    }
                }
            };
            var plan = new FitnessPlan
            {
                Name = planTemplate.Name,
                Exercises = new List<Exercise>
                {
                    new()
                    {
                        Name = "Test exercise",
                        Records = new List<API.Data.Record> 
                        {
                            new()
                            {
                                Date = DateTime.Now,
                                Weight = 100,
                                Repetitions = 3,
                            }
                        }
                    }
                }
            };
            var user1 = _context.Users
                .Include(x=>x.FitnessPlansTemplates)
                .Include(x=>x.FitnessPlans)
                .First(x=>x.UserName=="TestUserPlan");
            user1.FitnessPlansTemplates.Add(planTemplate);
            user1.FitnessPlans.Add(plan);
            var planTemplate2 = new FitnessPlanTemplate
            {
                Name = "TestPlan",
                Exercises = new List<ExerciseTemplate>()
                {
                    new()
                    {
                        Name = "Test exercise",
                        Sets = 2,
                        Description = "Description"
                    }
                }
            };
            var user2 = _context.Users.Include(x => x.FitnessPlansTemplates).First(x => x.UserName == "TestUserPlan2");
            user2.FitnessPlansTemplates.Add(planTemplate2);
            _context.SaveChanges();
        }
    }
}
