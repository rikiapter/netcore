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
    public class SiteTest : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;

        public SiteTest(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }




        [Fact]
        public async Task TestGetSiteAsync()
        {
            var request = "/api/v1/Site/Site/1";
            var response = await Client.GetAsync(request);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetSitesByEmployeeIdAsync()
        {
            var request = "/api/v1/Site/SiteByEmployee/1";
            var response = await Client.GetAsync(request);

            response.EnsureSuccessStatusCode();
        }
  
    }
}