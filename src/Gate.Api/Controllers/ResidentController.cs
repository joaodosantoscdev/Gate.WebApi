using AutoMapper;
using Gate.Application.DTOs.Request;
using Gate.Application.DTOs.Response;
using Gate.Application.Services.Interfaces;
using Gate.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResidentController : ControllerBase
    {
        private readonly IResidentService _residentService;
        private readonly IMapper _mapper;
        
        public ResidentController(IResidentService residentService, IMapper mapper)
        {
            _residentService = residentService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém todos os moradores.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Retorna todos os moradores cadastrados</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(IEnumerable<ResidentInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResidentInfo>>> ObterTodos()
        {
            var result = await _residentService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Obtém todos os moradores.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Retorna todos os moradores cadastrados</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(IEnumerable<ResidentInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("GetAllResidentsWithContact")]
        public async Task<ActionResult<IEnumerable<ResidentInfo>>> GetAllResidentsWithContact()
        {
            var result = await _residentService.GetAllResidentsWithContact();
            return Ok(result);
        }

        /// <summary>
        /// Obtém morador por Id.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Id do morador</param>
        /// <returns></returns>
        /// <response code="200">Retorna os dados do morador</response>
        /// <response code="404">Retorno caso o morador não seja encontrado</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(IEnumerable<ResidentInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ResidentInfo>> ObterPorId(int id)
        {
            var result = await _residentService.GetByIdAsync(id);
            if (result is null)
                return NotFound();
                
            return Ok(result);
        }

                /// <summary>
        /// Obtém morador por Id.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Id do morador</param>
        /// <returns></returns>
        /// <response code="200">Retorna os dados do morador</response>
        /// <response code="404">Retorno caso o morador não seja encontrado</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(IEnumerable<ResidentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("GetFullById")]
        public async Task<ActionResult<ResidentResponse>> GetFullById(int id)
        {
            var result = await _residentService.GetFullById(id);
            return Ok(result);
        }

        /// <summary>
        /// Insere um morador.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param param name="AddResidentRequest">Dados do morador</param>
        /// <returns></returns>
        /// <response code="201">Retorna o Id do morador criado</response>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<int>> Insert(AddResidentRequest request)
        {
            //realizar automapper
            var resident = _mapper.Map<ResidentInfo>(request);
            var id = (int)await _residentService.AddAsync(resident);
            return Ok($"Inserido morador #{id}");
        }

        /// <summary>
        /// Atualiza um morador.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="UpdateResidentRequest">Dados do morador</param>
        /// <returns></returns>
        /// <response code="200">Sucesso ao atualizar morador</response>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<ActionResult> Update(UpdateResidentRequest request)
        {
            var resident = _mapper.Map<ResidentInfo>(request);
            await _residentService.UpdateAsync(resident);
            return Ok();
        }

        /// <summary>
        /// Exclui um morador.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Id do morador</param>
        /// <returns></returns>
        /// <response code="200">Sucesso ao excluir morador</response>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _residentService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}