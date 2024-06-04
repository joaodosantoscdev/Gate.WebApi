using AutoMapper;
using Gate.Application.DTOs.Request;
using Gate.Application.DTOs.Request.Access;
using Gate.Application.DTOs.Response;
using Gate.Application.Services;
using Gate.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccessController : ControllerBase
    {
        private readonly IAccessService _accessService;
        private readonly IMapper _mapper;
        
        public AccessController(IAccessService accessService, IMapper mapper)
        {
            _accessService = accessService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém todos os acessos.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Retorna todos os acessos cadastrados</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(IEnumerable<AccessInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("GetAccesses")]
        public async Task<ActionResult<AccessResponse>> GetAccesses()
        {
            var result = await _accessService.GetAllFullAsync();
            return Ok(result);
        }

        /// <summary>
        /// Obtém todos os acessos.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Retorna todos os acessos cadastrados</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(IEnumerable<AccessInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccessInfo>>> ObterTodos()
        {
            var result = await _accessService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Obtém acesso por Id.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Id do acesso</param>
        /// <returns></returns>
        /// <response code="200">Retorna os dados do acesso</response>
        /// <response code="404">Retorno caso o acesso não seja encontrado</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(IEnumerable<AccessInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<AccessInfo>> ObterPorId(int id)
        {
            var result = await _accessService.GetByIdAsync(id);
            if (result is null)
                return NotFound();
                
            return Ok(result);
        }

        /// <summary>
        /// Obtém acesso por Id.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Id do acesso</param>
        /// <returns></returns>
        /// <response code="200">Retorna os dados do acesso</response>
        /// <response code="404">Retorno caso o acesso não seja encontrado</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(IEnumerable<AccessInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("GetFullById")]
        public async Task<ActionResult<AccessResponse>> GetFullById(int id)
        {
            var result = await _accessService.GetFullByIdAsync(id);
            return Ok(result);
        }

                /// <summary>
        /// Obtém acesso por Id.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="documentNumber">DOcumento do residente responsável pelo acesso</param>
        /// <returns></returns>
        /// <response code="200">Retorna os dados do acesso</response>
        /// <response code="404">Retorno caso o acesso não seja encontrado</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(IEnumerable<AccessInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("GetLastAccessByDocumentNumber")]
        public async Task<ActionResult<AccessInfo>> GetLastAccessByDocumentNumber(string documentNumber)
        {
            var result = await _accessService.GetLastAccessByDocumentNumber(documentNumber);
            return Ok(result);
        }

        /// <summary>
        /// Insere um acesso.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param param name="AddAccessRequest">Dados do acesso</param>
        /// <returns></returns>
        /// <response code="201">Retorna o Id do acesso criado</response>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<int>> Insert(AddAccessRequest request)
        {
            var access = _mapper.Map<AccessInfo>(request);
            var id = (int)await _accessService.AddAsync(access);
            return Ok($"Inserido acesso #{id}");
        }

                /// <summary>
        /// Insere um acesso.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param param name="AddAccessRequest">Dados do acesso</param>
        /// <returns></returns>
        /// <response code="201">Retorna o Id do acesso criado</response>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("InsertFastAccess")]
        public async Task<ActionResult<int>> InsertFastAccess(FastAccessRequest request)
        {
            var id = await _accessService.InsertFastAccess(request);
            return Ok(id);
        }

        /// <summary>
        /// Atualiza um acesso.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="UpdateAccessRequest">Dados do acesso</param>
        /// <returns></returns>
        /// <response code="200">Sucesso ao atualizar acesso</response>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<ActionResult> Update(UpdateAccessRequest request)
        {
            var complex = _mapper.Map<AccessInfo>(request);
            await _accessService.UpdateAsync(complex);
            return Ok();
        }

        /// <summary>
        /// Exclui um acesso.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Id do acesso</param>
        /// <returns></returns>
        /// <response code="200">Sucesso ao excluir acesso</response>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _accessService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}