using System;
using System.Net.Http;
using System.Threading.Tasks;
using Malam.Mastpen.Core.BL.Responses;
using Newtonsoft.Json;
using Xunit;
using Malam.Mastpen.Core.DAL.Entities;
using Malam.Mastpen.API.IntegrationTests.Helper;

namespace Malam.Mastpen.API.IntegrationTests.IntegrationTest
{
    public class EmployeeTest : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;

        public EmployeeTest(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task TestGetEmployeesAsync()
        {
            var token = await TokenHelper.GetMastpenTokenForWolverineAsync();
            // Arrange
            var request = "/api/v1/Employee/Employee";

            // Act
            Client.SetBearerToken(token.AccessToken);
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }


        [Fact]
        public async Task TestGetEmployeeAsync()
        {        
            //יש להעלות את הפרויקט 
            //identity server
            var token = await TokenHelper.GetMastpenTokenForWolverineAsync();
            // Arrange
            var request = "/api/v1/Employee/Employee/1";

            // Act
            Client.SetBearerToken(token.AccessToken);
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestPostEmployeeAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/v1/Employee/Employee",
                Body = new
                {
                    EmployeeID = 1,
                    IdentificationTypeID = 1,
                    IdentityNumber = 1,
                    PassportCountryID = 1,
                    FirstName = "FirstName",
                    LastName = "",
                    FirstNameEN = "",
                    LastNameEN = "",
                    OrganizationID = 1,
                    BirthDate = new DateTime(2000, 5, 1),
                    GenderID = 1,
                    Citizenship = 1
                }
            };

            // Act
            var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestPutEmployeeAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/v1/Employee/Employee/1",
                Body = new
                {
                    EmployeeID = 1,
                    IdentificationTypeID = 1,
                    IdentityNumber = 1,
                    PassportCountryID = 1,
                    FirstName = "FirstName",
                    LastName = "",
                    FirstNameEN = "",
                    LastNameEN = "",
                    OrganizationID = 1,
                    BirthDate = new DateTime(2000, 5, 1),
                    GenderID = 1,
                    Citizenship = 1
                }
            };

            // Act
            var response = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestDeleteEmployeeAsync()
        {
            var deleteResponse = await Client.DeleteAsync(string.Format("/api/v1/Employee/Employee/{0}", 1));
            deleteResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestPostEmployeeTrainingAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/v1/Employee/EmployeeTraining",
                Body = new
                {
                    EmployeeId = 2,
                    EmployeeTrainingId = 3

                }
            };

            // Act
            var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestPostEmployeeWorkPermitAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/v1/Employee/EmployeeWorkPermit",
                Body = new
                {
                    EmployeeId = 2,
                    EmployeeWorkPermitId = 3

                }
            };

            // Act
            var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task TestPostEmployeeAuthtorizationAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/v1/Employee/EmployeeAuthtorization",
                Body = new
                {
                    EmployeeId = 2,
                    EmployeeAuthorizationId = 3

                }
            };

            // Act
            var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetEmployeeTrainingAsync()
        {
            //יש להעלות את הפרויקט 
            //identity server
            var token = await TokenHelper.GetMastpenTokenForWolverineAsync();
            // Arrange
            var request = "/api/v1/Employee/EmployeeTraining/1";

            // Act
            Client.SetBearerToken(token.AccessToken);
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }


        [Fact]
        public async Task TestGetEmployeeWorkPermitAsync()
        {
            //יש להעלות את הפרויקט 
            //identity server
            var token = await TokenHelper.GetMastpenTokenForWolverineAsync();
            // Arrange
            var request = "/api/v1/Employee/EmployeeWorkPermit/1";

            // Act
            Client.SetBearerToken(token.AccessToken);
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }


        [Fact]
        public async Task TestGetEmployeeAuthtorizationAsync()
        {
            //יש להעלות את הפרויקט 
            //identity server
            var token = await TokenHelper.GetMastpenTokenForWolverineAsync();
            // Arrange
            var request = "/api/v1/Employee/EmployeeAuthtorization/1";

            // Act
            Client.SetBearerToken(token.AccessToken);
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }




    }
}