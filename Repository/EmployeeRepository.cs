using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base (repositoryContext)
        {

        }

        /* MARRIM TE GJITHE PUNONJESIT NGA TABELA */
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync() =>
          FindAll().OrderBy(e => e.Name).ToList();

        /* MARRIM TE GJITHE PUNONJESIT TE NJE KOMPANIE */
        public async Task<PagedList<Employee>> GetEmployeesPerCompanyAsync(int companyId, EmployeeParameters employeeParameters)
        {

            //var employees = await FindByCondition(e => e.CompanyId.Equals(companyId) &&
            //(e.Age >= employeeParameters.MinAge && e.Age <= employeeParameters.MaxAge),
            //trackChanges)
            var employees = await FindByCondition(e => e.CompanyId == companyId) 
                //.FilterEmployees(employeeParameters.MinAge, employeeParameters.MaxAge)
                .Search(employeeParameters.SearchTerm)
                .Sort(employeeParameters.OrderBy)
                .ToListAsync();
            return PagedList<Employee>
            .ToPagedList(employees, employeeParameters.PageNumber,
            employeeParameters.PageSize);
        }

        /*MERR NJE PUNONJES */
        public async Task <Employee> GetEmployeeAsync(int id) =>
               await FindByCondition(e => e.Id.Equals(id)).FirstOrDefaultAsync();

        /*MERR NJE PUNONJES PER NJE KOMPANI*/
        public async Task <Employee> GetEmployeeForCompanyAsync(int companyId, int id) =>
               await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id)).FirstOrDefaultAsync();

        /*FSHI NJE PUNONJES*/
        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }

        public void CreateEmployee(Employee employee)
        {
            Create(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            Update(employee);
        }
    }
}
