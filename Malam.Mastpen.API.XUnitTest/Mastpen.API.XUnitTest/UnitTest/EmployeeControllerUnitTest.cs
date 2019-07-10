
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
    public class EmployeeControllerUnitTest
    {
        [Fact]
        public async Task TestGetEmployeesAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetEmployeeController(nameof(TestGetEmployeesAsync));

            // Act
            var response = await controller.GetEmployeesAsync() as ObjectResult;
            var value = response.Value as IPagedResponse<Employee>;

            // Assert
            Assert.False(value.DIdError);
        }

        [Fact]
        public async Task TestGetEmployeeAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetEmployeeController(nameof(TestGetEmployeeAsync));

            var id = 1;

            // Act
            var response = await controller.GetEmployeeAsync(id) as ObjectResult;
            var value = response.Value as ISingleResponse<EmployeeResponse>;

            // Assert
            Assert.False(value.DIdError);
        }

        [Fact]
        public async Task TestPostEmployeeAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetEmployeeController(nameof(TestPostEmployeeAsync));

            var request = new EmployeeRequest
            {
                EmployeeId = 2,
                IdentificationTypeId = 1,
                IdentityNumber = 1,
                PassportCountryId = 1,
                FirstName = "FirstName",
                LastName = "",
                FirstNameEN = "",
                LastNameEN = "",
                OrganizationId = 1,
                BirthDate = new DateTime(2000, 5, 1),
                GenderId = 1,
                Citizenship = 1

            };

            // Act
            var response = await controller.PostEmployeeAsync(request) as ObjectResult;
            var value = response.Value as ISingleResponse<Employee>;

            // Assert
            Assert.False(value.DIdError);
        }

        [Fact]
        public async Task TestPutEmployeeAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetEmployeeController(nameof(TestPutEmployeeAsync));

            var id = 1;
            var request = new EmployeeRequest
            {
                EmployeeId = 1,
                IdentificationTypeId = 2,
                IdentityNumber = 2,
                PassportCountryId = 2,
                FirstName = "FirstName1",
                LastName = "",
                FirstNameEN = "",
                LastNameEN = "",
                OrganizationId = 1,
                BirthDate = new DateTime(2000, 5, 1),
                GenderId = 1,
                Citizenship = 1
            };

            // Act
            var response = await controller.PutEmployeeAsync(id, request) as ObjectResult;
            var value = response.Value as IResponse;


            // Assert
            Assert.False(value.DIdError);
        }

        [Fact]
        public async Task TestDeleteEmployeeAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetEmployeeController(nameof(TestDeleteEmployeeAsync));
            var id = 1;

            // Act
            var response = await controller.DeleteEmployeeAsync(id) as ObjectResult;
            var value = response.Value as IResponse;

            // Assert
            Assert.False(value.DIdError);
        }


        [Fact]
        public async Task TestPostEmployeeTrainingAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetEmployeeController(nameof(TestPostEmployeeTrainingAsync));

            var request = new EmployeeTrainingRequest
            {
                EmployeeId = 2,
                EmployeeTrainingId=3

            };

            // Act
            var response = await controller.PostEmployeeTrainingAsync(request) as ObjectResult;
            var value = response.Value as ISingleResponse<EmployeeTraining>;

            // Assert
            Assert.False(value.DIdError);
        }

        [Fact]
        public async Task TestPostEmployeeWorkPermitAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetEmployeeController(nameof(TestPostEmployeeWorkPermitAsync));

            var request = new EmployeeWorkPermitRequest
            {
                EmployeeId = 2,
                EmployeeWorkPermitId = 3

            };

            // Act
            var response = await controller.PostEmployeeWorkPermitAsync(request) as ObjectResult;
            var value = response.Value as ISingleResponse<EmployeeWorkPermit>;

            // Assert
            Assert.False(value.DIdError);
        }

        [Fact]
        public async Task TestPostEmployeeAuthtorizationAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetEmployeeController(nameof(TestPostEmployeeAuthtorizationAsync));

            var request = new EmployeeAuthtorizationRequest
            {
                EmployeeId = 2,
                EmployeeAuthorizationId = 3

            };

            // Act
            var response = await controller.PostEmployeeAuthtorizationAsync(request) as ObjectResult;
            var value = response.Value as ISingleResponse<EmployeeAuthtorization>;

            // Assert
            Assert.False(value.DIdError);
        }

        [Fact]
        public async Task TestGetEmployeeTrainingAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetEmployeeController(nameof(TestGetEmployeeTrainingAsync));

            var id = 1;

            // Act
            var response = await controller.GetEmployeeTrainingAsync(id) as ObjectResult;
            var value = response.Value as IListResponse<EmployeeTraining>;

            // Assert
            Assert.False(value.DIdError);
        }

        [Fact]
        public async Task TestGetEmployeeWorkPermitAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetEmployeeController(nameof(TestGetEmployeeWorkPermitAsync));

            var id = 1;

            // Act
            var response = await controller.GetEmployeeWorkPermitAsync(id) as ObjectResult;
            var value = response.Value as IListResponse<EmployeeWorkPermit>;

            // Assert
            Assert.False(value.DIdError);
        }

        [Fact]
        public async Task TestGetEmployeeAuthtorizatioAsync()
        {
            // Arrange
            var controller = ControllerMocker.GetEmployeeController(nameof(TestGetEmployeeAuthtorizatioAsync));

            var id = 1;

            // Act
            var response = await controller.GetEmployeeAuthtorizationAsync(id) as ObjectResult;
            var value = response.Value as IListResponse<EmployeeAuthtorization>;

            // Assert
            Assert.False(value.DIdError);
        }
    }
}