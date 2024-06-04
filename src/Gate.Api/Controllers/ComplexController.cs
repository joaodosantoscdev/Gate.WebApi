using AutoMapper;
using Gate.Application.DTOs.Request;
using Gate.Application.Services;
using Gate.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplexController : ControllerBase
    {
        private readonly IComplexService _complexService;
        private readonly IMapper _mapper;
        
        public ComplexController(IComplexService complexService, IMapper mapper)
        {
            _complexService = complexService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém todos os complexos.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Retorna todos os complexos cadastrados</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(IEnumerable<ComplexInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComplexInfo>>> ObterTodos()
        {
            var result = await _complexService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Obtém complexo por Id.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Id do complexo</param>
        /// <returns></returns>
        /// <response code="200">Retorna os dados do complexo</response>
        /// <response code="404">Retorno caso o complexo não seja encontrado</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(IEnumerable<ComplexInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ComplexInfo>> ObterPorId(int id)
        {
            var result = await _complexService.GetByIdAsync(id);
            if (result is null)
                return NotFound();
                
            return Ok(result);
        }

        /// <summary>
        /// Insere um complexo.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param param name="AddComplexRequest">Dados do complexo</param>
        /// <returns></returns>
        /// <response code="201">Retorna o Id do complexo criado</response>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<int>> Insert(AddComplexRequest request)
        {
            //realizar automapper
            var complex = _mapper.Map<ComplexInfo>(request);
            var id = (int)await _complexService.AddAsync(complex);
            return Ok($"Inserido complexo #{id}");
        }

        /// <summary>
        /// Atualiza um complexo.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="UpdateComplexRequest">Dados do complexo</param>
        /// <returns></returns>
        /// <response code="200">Sucesso ao atualizar complexo</response>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<ActionResult> Update(UpdateComplexRequest request)
        {
            var complex = _mapper.Map<ComplexInfo>(request);
            await _complexService.UpdateAsync(complex);
            return Ok();
        }

        /// <summary>
        /// Exclui um complexo.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Id do complexo</param>
        /// <returns></returns>
        /// <response code="200">Sucesso ao excluir complexo</response>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _complexService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}