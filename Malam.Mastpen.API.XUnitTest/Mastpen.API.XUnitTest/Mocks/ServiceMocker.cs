
using Malam.Mastpen.Core;
using Malam.Mastpen.Core.BL.Contracts;
using Malam.Mastpen.Core.BL.Services;
using Malam.Mastpen.Core.DAL;

namespace Malam.Mastpen.API.XUnitTest.Mocks
{
    public static class ServiceMocker
    {
        public static IEmployeeService GetEmployeeService(IUserInfo userInfo, string dbName)
            => new EmployeeService( userInfo,  DbContextMocker.GetMastpenDbContext(dbName));

        public static ISiteService GetSiteService(IUserInfo userInfo, string dbName)
            => new SiteService(userInfo,DbContextMocker.GetMastpenDbContext(dbName));

        public static IGeneralService GetGeneralService(IUserInfo userInfo, string dbName)
            => new GeneralService( userInfo,DbContextMocker.GetMastpenDbContext(dbName));


    }
}
