using Gate.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplexController : ControllerBase
    {
        private readonly IComplexService _complexService;
        public ComplexController(IComplexService complexService)
        {
            _complexService = complexService; 
        }

        
    }
}