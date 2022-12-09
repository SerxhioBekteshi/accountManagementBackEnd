
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System;
using Contracts;
using AutoMapper;
using Entities.ResponseFeatures;
using Entities.DTC;
using Service.Contracts;
using SendGrid.Helpers.Errors.Model;
using Entities.DTO;
using Entities.RequestFeatures;
using Entities.Models;

public class EmployeeService : IEmployeeService
{
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;
    private readonly IDapperRepository _dapperRepository;

    public EmployeeService(ILoggerManager logger,
        IMapper mapper,
        IRepositoryManager repositoryManager,
        IDapperRepository dapperRepository
        )
    {
        _logger = logger;
        _mapper = mapper;
        _repositoryManager = repositoryManager;
        _dapperRepository = dapperRepository;

    }

    public async Task<PagedListResponse<IEnumerable<EmployeeDto>>> GetAllRecords(string userId, int companyId, RequestTableBodyDto request)
    {
        try
        {
            var EmployessWithMetaData = await _dapperRepository.SearchEmployees(userId, companyId, request);
            var employeesDtoList = _mapper.Map<IEnumerable<EmployeeDto>>(EmployessWithMetaData);
            var columns = GetDataTableColumns();

            PagedListResponse<IEnumerable<EmployeeDto>> response = new PagedListResponse<IEnumerable<EmployeeDto>>
            {
                TotalCount = EmployessWithMetaData.MetaData.TotalCount,
                CurrentPage = EmployessWithMetaData.MetaData.CurrentPage,
                PageSize = EmployessWithMetaData.MetaData.PageSize,
                Columns = columns,
                Rows = employeesDtoList,
                Key = EmployessWithMetaData.MetaData.Key,
                hasNext = EmployessWithMetaData.MetaData.hasNext,
                hasPrevious = EmployessWithMetaData.MetaData.hasPrevious,
            };
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(GetAllRecords), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }

    public async Task<int> CreateRecord(EmployeeForCreationAndUpdateDto createEmployeeDto, int userId, int companyId)
    {
        try
        {
            var companyFromDb = await _repositoryManager.Company.GetCompanyAsync(companyId);
            
                var employee = _mapper.Map<Employee>(createEmployeeDto);

                employee.DateCreated = DateTime.UtcNow;
                employee.CreatedBy = userId;
                employee.CompanyId = companyFromDb.Id;

                _repositoryManager.Employee.CreateEmployee(employee);
                await _repositoryManager.SaveAsync();

                return employee.Id;
            
            
  

        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(CreateRecord), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }

    public async Task<bool> DeleteRecord(int id)
    {
        try
        {

            var existingEmployee = await GetEmployeeIdAsync(id);
            _repositoryManager.Employee.DeleteEmployee(existingEmployee);
            await _repositoryManager.SaveAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(DeleteRecord), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }

    public async Task<bool> UpdateRecord(int id, EmployeeForCreationAndUpdateDto updateEmployeeDto, int userId)
    {
        var existingEmployee = await GetEmployeeIdAsync(id);

        await UpdateEmployee(existingEmployee, updateEmployeeDto, userId);

        return true;
    }


    public async Task<EmployeeDto> GetRecordById(int id)
    {
        try
        {
            var existingCEmployee = await _dapperRepository.GetEmployeeById(id);
            if (existingCEmployee is null)
                throw new NotFoundException(string.Format("Employee with Id: {0} was not found!", id));

            //var serviceLocationIds = await _repositoryManager.ServiceLocationRepository.GetServiceIdsForLocationId(id);
            //if (serviceLocationIds.Any())
            //    existingLocation.ServiceIds = serviceLocationIds.ToList();

            return existingCEmployee;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(GetRecordById), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }


    #region Private Methods

    private List<DataTableColumn> GetDataTableColumns()
    {
        // get the columns
        var columns = GenerateDataTableColumn<EmployeeColumn>.GetDataTableColumns();

        // return all columns
        return columns;
    }


    private async Task<Employee> GetEmployeeIdAsync(int id)
    {
        var existingEmployee = await _repositoryManager.Employee.GetEmployeeAsync(id);
        return existingEmployee;
    }

    private async Task UpdateEmployee(Employee existingEmployee, EmployeeForCreationAndUpdateDto updateEmployeeDto, int userId)
    {
        try
        {
            _mapper.Map(updateEmployeeDto, existingEmployee);

            existingEmployee.DateModified = DateTime.UtcNow;
            existingEmployee.ModifiedBy = userId;
            _repositoryManager.Employee.UpdateEmployee(existingEmployee);
            await _repositoryManager.SaveAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(UpdateEmployee), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }

    #endregion
}
