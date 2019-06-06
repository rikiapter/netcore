
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

            return new EmployeeController( identityClient, service);
        }


        public static SiteController GetSiteController(string name)
        {
            // var logger = LoggingHelper.GetLogger<SiteController>();
            var identityClient = new MockedRothschildHouseIdentityClient();

            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetSiteService(userInfo, name);
              //var generalService = ServiceMocker.GetGeneralService(userInfo, name);

            return new SiteController(identityClient, service);//, generalService);
        }
    }
}
