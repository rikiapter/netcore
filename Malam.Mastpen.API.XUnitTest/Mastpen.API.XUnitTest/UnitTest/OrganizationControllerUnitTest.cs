using Malam.Mastpen.API.XUnitTest.Mocks;
using Malam.Mastpen.Core.BL.Responses;
using Malam.Mastpen.Core.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
                 OrganizationId=3
            };

            // Act
            var response = await controller.PostOrganizationAsync(request) as ObjectResult;
            var value = response.Value as ISingleResponse<Organization>;

            // Assert
            Assert.False(value.DIdError);
        }
    }
}
