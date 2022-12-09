using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        Task <IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<PagedList<Employee>> GetEmployeesPerCompanyAsync(int companyId, EmployeeParameters employeeParameters);
        Task <Employee> GetEmployeeAsync(int id);
        Task <Employee> GetEmployeeForCompanyAsync(int companyid, int id);
        //void CreateEmployeeForCompany(Guid companyId, Employee employee);
        void DeleteEmployee(Employee employee);
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
    }
}
