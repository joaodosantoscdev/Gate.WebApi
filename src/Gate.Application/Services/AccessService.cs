using AutoMapper;
using Gate.Application.DTOs.Request.Access;
using Gate.Application.DTOs.Response;
using Gate.Application.Services.Interfaces;
using Gate.Domain.Models;
using Gate.Persistence.Repositories.Interfaces;

namespace Gate.Application.Services
{
    public class AccessService : BaseService<AccessInfo>, IAccessService
    {
        private readonly IAccessRepository _accessRepository;
        private readonly IResidentService _residentService;
        private readonly IMapper _mapper;
        public AccessService(IAccessRepository accessRepository, IResidentService residentService, IMapper mapper) : base(accessRepository) 
        {
            _residentService = residentService;
            _accessRepository = accessRepository;
            _mapper = mapper;
        }

        public async Task<List<AccessResponse>> GetAllFullAsync()
        {
            var lstAccessInfo = await _accessRepository.GetAllFullAsync();

            var response = _mapper.Map<List<AccessResponse>>(lstAccessInfo);

            return response;
        }

        public async Task<AccessResponse> GetFullByIdAsync(int id)
        {
            var accessInfo = await _accessRepository.GetFullByIdAsync(id);

            var response = _mapper.Map<AccessResponse>(accessInfo);

            return response;
        }

        public async Task<AccessResponse> GetLastAccessByDocumentNumber(string documentNumber)
        {
            var accessInfo = await _accessRepository.GetLastAccessByDocumentNumber(documentNumber);

            var response = _mapper.Map<AccessResponse>(accessInfo);

            return response;
        }

        public async Task<object> InsertFastAccess(FastAccessRequest fastAccess)
        {
            var residentInfo = await _residentService.GetResidentByDocumentNumberAsync(fastAccess.DocumentNumber);
            if (residentInfo == null) { 
                residentInfo = new ResidentInfo 
                {
                    Name = fastAccess.Name,
                    Type = fastAccess.Type,
                    LastName = fastAccess.LastName,
                    BirthDate = fastAccess.BirthDate,
                    DocumentNumber = fastAccess.DocumentNumber,
                    PhotoBase64 = fastAccess.PhotoBase64,
                    Contacts = new()
                 };

                 foreach (var contact in fastAccess.Contacts)  {
                    var contactInfo = _mapper.Map<ContactInfo>(contact);
                    residentInfo.Contacts.Add(contactInfo);
                 }
                 
                var reponse = await _residentService.AddAsync(residentInfo);
            }

            if (string.IsNullOrWhiteSpace(residentInfo.PhotoBase64)) {
                residentInfo.PhotoBase64 = fastAccess.PhotoBase64;
            }

            var accessInfo = new AccessInfo 
            { 
                Date = DateTime.Now,
                AccessType = fastAccess.AccessType,
                PlaceId = fastAccess.PlaceId,
                UnitId = fastAccess.UnitId,
                ResidentId = residentInfo.Id,
            };

            var response =  await _accessRepository.AddAsync(accessInfo);

            return _mapper.Map<AccessResponse>(response);
        }
    }
}