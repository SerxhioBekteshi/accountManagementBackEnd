using Entities.DTO;
using Entities.RequestFeatures;
using Entities.ResponseFeatures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        Task<PagedListResponse<IEnumerable<EmployeeDto>>> GetAllRecords(string userId, int companyId, RequestTableBodyDto request);
        Task<int> CreateRecord(EmployeeForCreationAndUpdateDto createEmployeeDTO, int userId, int companyId); //MANAGER COMPANY CREATES AN EMPLOYEE FOR THE CERTAIN COMPANY
        Task<bool> UpdateRecord(int id, EmployeeForCreationAndUpdateDto updateEmployeeDto, int userId);
        Task<bool> DeleteRecord(int id);
        Task<EmployeeDto> GetRecordById(int id);

    }
}
