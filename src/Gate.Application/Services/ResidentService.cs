using AutoMapper;
using Gate.Application.DTOs.Response;
using Gate.Application.Services.Interfaces;
using Gate.Domain.Models;
using Gate.Persistence.Repositories.Interfaces;

namespace Gate.Application.Services
{
    public class ResidentService : BaseService<ResidentInfo>, IResidentService
    {
        private readonly IResidentRepository _residentRepository;
        private readonly IMapper _mapper;
        public ResidentService(IResidentRepository residentRepository, IMapper mapper) : base(residentRepository) 
        {
            _residentRepository = residentRepository;
            _mapper = mapper;
        }

        public async Task<List<ResidentInfo>> GetAllResidentsWithContact()
        {
            var residentInfoList = await _residentRepository.GetAllResidentsWithContact();
            return residentInfoList;
        }

        public async Task<ResidentResponse> GetFullById(int id)
        {
            var residentInfo = await _residentRepository.GetFullById(id);

            var response = _mapper.Map<ResidentResponse>(residentInfo);

            return response;
        }

        public async Task<ResidentInfo> GetResidentByDocumentNumberAsync(string documentNumber)
        {
            var residentInfo = await _residentRepository.GetResidentByDocumentNumber(documentNumber);
            return residentInfo;
        }
    }
}