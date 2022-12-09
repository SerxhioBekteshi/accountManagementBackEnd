using AutoMapper;
using Contracts;
using Entities.DTO;
using SendGrid.Helpers.Errors.Model;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IDapperRepository _dapperRepository;
        public UserService(ILoggerManager logger,
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
        public async Task<IEnumerable<UserDtoManagerList>> GetAllManagersAsync(AutocompleteDto autocomplete)
        {
            try 
            {
                var managers = await _dapperRepository.GetAllManagers(autocomplete);
                return managers;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0}: {1}", nameof(GetAllManagersAsync), ex.Message));
                throw new BadRequestException(ex.Message);
            }
        }
    }
}
