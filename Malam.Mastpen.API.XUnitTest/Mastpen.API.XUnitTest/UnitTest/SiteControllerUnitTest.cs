
using System;
using System.Threading.Tasks;
using Malam.Mastpen.API.Controllers;
using Malam.Mastpen.API.XUnitTest.Gateway;
using Malam.Mastpen.API.XUnitTest.Mocks;
using Malam.Mastpen.API.XUnitTest.Mocks.Identity;
using Malam.Mastpen.Core.BL.Requests;
using Malam.Mastpen.Core.BL.Responses;
using Malam.Mastpen.Core.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Xunit;


namespace Malam.Mastpen.API.XUnitTest.UnitTest
{
    public class SiteControllerUnitTest
    {
        [Fact]
        public async Task TestGetSiteAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetSiteController(nameof(TestGetSiteAsync));

            var id = 1;

            // Act
            var response = await controller.GetSiteAsync(id) as ObjectResult;
            var value = response.Value as ISingleResponse<SiteResponse>;

            // Assert
            Assert.False(value.DIdError);
        }

        [Fact]
        public async Task TestGetSitesByEmployeeIdAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetSiteController(nameof(TestGetSiteAsync));

            var id = 1;

            // Act
            var response = await controller.GetSitesByEmployeeIdAsync(id) as ObjectResult;
            var value = response.Value as ISingleResponse<SiteEmployee>;

            // Assert
            Assert.False(value.DIdError);
        }
    }
}