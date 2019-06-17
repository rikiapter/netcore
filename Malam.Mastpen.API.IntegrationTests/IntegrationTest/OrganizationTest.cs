using System;
using System.Net.Http;
using System.Threading.Tasks;
using Malam.Mastpen.Core.BL.Responses;
using Newtonsoft.Json;
using Xunit;
using Malam.Mastpen.Core.DAL.Entities;
using Malam.Mastpen.API.IntegrationTests.Helper;
using Malam.Mastpen.API.IntegrationTests;
using Malam.Mastpen.API;

namespace Malam.Mastpen.HR.IntegrationTests.IntegrationTest
{

        public class OrganizationTest : IClassFixture<TestFixture<Startup>>
        {
            private HttpClient Client;

            public OrganizationTest(TestFixture<Startup> fixture)
            {
                Client = fixture.Client;
            }

            [Fact]
        public async Task TestPostOrganizationAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/v1/Organization/Organization",
                Body = new
                {
                    OrganizationID = 1,
                  
                }
            };

            // Act
            var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
