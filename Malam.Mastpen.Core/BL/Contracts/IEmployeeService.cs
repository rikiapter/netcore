
using System;
using System.Threading.Tasks;
using Malam.Mastpen.Core.BL.Requests;
using Malam.Mastpen.Core.BL.Responses;
using Malam.Mastpen.Core.DAL.Dbo;
using Malam.Mastpen.Core.DAL.Entities;


namespace Malam.Mastpen.Core.BL.Contracts
{
    public interface IEmployeeService : IService
    {
        Task<IPagedResponse<Employee>> GetEmployeesAsync(int pageSize = 10, int pageNumber = 1, int? EmployeeId = null, string EmployeeName = null, int? IdentityNumber = null, int? OrganizationId = null, int? PassportCountryId = null, int? ProffesionType = null);
        Task<SingleResponse<EmployeeResponse>> GetEmployeeAsync(int Id);
        Task<SingleResponse<Employee>> GetEmployeeByEmployeeNameAsync(int Id);
        Task<SingleResponse<Employee>> CreateEmployeeAsync(Employee employee);
        Task<Response> UpdateEmployeeAsync(Employee employee);
        Task<Response> DeleteEmployeeAsync(int Id);
        Task<SingleResponse<EmployeeWorkPermit>> CreateEmployeeWorkPermitAsync(EmployeeWorkPermit employeeWorkPermit);
        Task<SingleResponse<EmployeeTraining>> CreateEmployeeTrainingAsync(EmployeeTraining employeeTraining);
        Task<SingleResponse<EmployeeAuthtorization>> CreateEmployeeAuthtorizationAsync(EmployeeAuthtorization employeeAuthtorization);
        Task<ListResponse<EmployeeTraining>> GetEmployeeTrainingByEmployeeIdAsync(int Id);
        Task<ListResponse<EmployeeWorkPermit>> GetEmployeeWorkPermitByEmployeeIdAsync(int Id);
        Task<ListResponse<EmployeeAuthtorization>> GetEmployeeAuthtorizationByEmployeeIdAsync(int Id);




    }
}
