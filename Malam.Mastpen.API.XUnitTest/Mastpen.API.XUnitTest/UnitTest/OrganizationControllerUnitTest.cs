
using System;
using System.Threading.Tasks;
using Malam.Mastpen.API.Controllers;
using Malam.Mastpen.API.XUnitTest.Gateway;
using Malam.Mastpen.API.XUnitTest.Mocks;
using Malam.Mastpen.API.XUnitTest.Mocks.Identity;
using Malam.Mastpen.Core.BL.Requests;
using Malam.Mastpen.Core.BL.Responses;
using Malam.Mastpen.Core.DAL.Entities;
using Malam.Mastpen.HR.Core.BL.Requests;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Malam.Mastpen.HR.XUnitTest.UnitTest
{
  public  class OrganizationControllerUnitTest
    {
        [Fact]
        public async Task TestPostOrganizationAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetOrganizationController(nameof(TestPostOrganizationAsync));

            var request = new Organization
            {
                 OrganizationId=3,
                 OrganizationName="קבלן אא",
                 OrganizationExpertiseTypeId=1,
                 OrganizationTypeId=1
            };

            // Act
            var response = await controller.PostOrganizationAsync(request) as ObjectResult;
            var value = response.Value as ISingleResponse<Organization>;

            // Assert
            Assert.False(value.DIdError);
        }

        [Fact]
        public async Task TestGetOrganizationsAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetOrganizationController(nameof(TestGetOrganizationsAsync));

            // Act
            var response = await controller.GetOrganizationsAsync() as ObjectResult;
            var value = response.Value as IPagedResponse<Organization>;

            // Assert
            Assert.False(value.DIdError);
        }

        [Fact]
        public async Task TestGetOrganizationAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetOrganizationController(nameof(TestGetOrganizationAsync));

            var id = 1;

            // Act
            var response = await controller.GetOrganizationAsync(id) as ObjectResult;
            var value = response.Value as ISingleResponse<OrganizationResponse>;

            // Assert
            Assert.False(value.DIdError);
        }


        [Fact]
        public async Task TestPutOrganizationAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetOrganizationController(nameof(TestPutOrganizationAsync));

            var id = 1;
            var request = new OrganizationRequest
            {
                OrganizationId = 1,
      
            };

            // Act
            var response = await controller.PutOrganizationAsync(id, request) as ObjectResult;
            var value = response.Value as IResponse;


            // Assert
            Assert.False(value.DIdError);
        }

        [Fact]
        public async Task TestDeleteOrganizationAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetOrganizationController(nameof(TestDeleteOrganizationAsync));
            var id = 1;

            // Act
            var response = await controller.DeleteOrganizationAsync(id) as ObjectResult;
            var value = response.Value as IResponse;

            // Assert
            Assert.False(value.DIdError);
        }
    }
}
