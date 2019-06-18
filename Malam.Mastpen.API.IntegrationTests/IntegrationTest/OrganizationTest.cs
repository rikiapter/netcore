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
        [Fact]
        public async Task TestGetOrganizationsAsync()
        {
            var token = await TokenHelper.GetMastpenTokenForWolverineAsync();
            // Arrange
            var request = "/api/v1/Organization/Organization";

            // Act
            Client.SetBearerToken(token.AccessToken);
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }


        [Fact]
        public async Task TestGetOrganizationAsync()
        {
            var token = await TokenHelper.GetMastpenTokenForWolverineAsync();
            // Arrange
            var request = "/api/v1/Organization/Organization/1";

            // Act
            Client.SetBearerToken(token.AccessToken);
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

     
        [Fact]
        public async Task TestPutOrganizationAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/v1/Organization/Organization/1",
                Body = new
                {
                    OrganizationID = 1
                }
            };

            // Act
            var response = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestDeleteOrganizationAsync()
        {
            var deleteResponse = await Client.DeleteAsync(string.Format("/api/v1/Organization/Organization/{0}", 1));
            deleteResponse.EnsureSuccessStatusCode();
        }
    }
}
