
using Malam.Mastpen.API.Commom;
using Malam.Mastpen.API.Controllers;
using Malam.Mastpen.API.XUnitTest.Mocks.Identity;
using Malam.Mastpen.API.XUnitTest.Mocks;
using Malam.Mastpen.API.XUnitTest.Gateway;

namespace Malam.Mastpen.API.XUnitTest.Mocks
{
    public static class ControllerMocker
    {
        public static EmployeeController GetEmployeeController(string name)
        {
           // var logger = LoggingHelper.GetLogger<EmployeeController>();
            var identityClient = new MockedRothschildHouseIdentityClient();
   
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetEmployeeService(userInfo, name);

            return new EmployeeController( identityClient, service,null);
        }
        public static OrganizationController GetOrganizationController(string name)
        {
            // var logger = LoggingHelper.GetLogger<EmployeeController>();
            var identityClient = new MockedRothschildHouseIdentityClient();

            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetOrganizationService(userInfo, name);

            return new OrganizationController(identityClient, service);
        }

    }
}
