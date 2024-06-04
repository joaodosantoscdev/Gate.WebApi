using AutoMapper;
using Gate.Application.DTOs.Request;
using Gate.Application.Services;
using Gate.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;
        private readonly IMapper _mapper;
        
        public UnitController(IUnitService unitService, IMapper mapper)
        {
            _unitService = unitService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém todos os unidades.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Retorna todos os unidades cadastrados</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(IEnumerable<UnitInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitInfo>>> ObterTodos()
        {
            var result = await _unitService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Obtém unidade por Id.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Id do unidade</param>
        /// <returns></returns>
        /// <response code="200">Retorna os dados do unidade</response>
        /// <response code="404">Retorno caso o unidade não seja encontrado</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(IEnumerable<UnitInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<UnitInfo>> ObterPorId(int id)
        {
            var result = await _unitService.GetByIdAsync(id);
            if (result is null)
                return NotFound();
                
            return Ok(result);
        }

        /// <summary>
        /// Insere um unidade.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param param name="AddUnitRequest">Dados do unidade</param>
        /// <returns></returns>
        /// <response code="201">Retorna o Id do unidade criado</response>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<int>> Insert(AddUnitRequest request)
        {
            //realizar automapper
            var complex = _mapper.Map<UnitInfo>(request);
            var id = (int)await _unitService.AddAsync(complex);
            return Ok($"Inserido unidade #{id}");
        }

        /// <summary>
        /// Atualiza um unidade.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="UpdateUnitRequest">Dados do unidade</param>
        /// <returns></returns>
        /// <response code="200">Sucesso ao atualizar unidade</response>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<ActionResult> Update(UpdateUnitRequest request)
        {
            var complex = _mapper.Map<UnitInfo>(request);
            await _unitService.UpdateAsync(complex);
            return Ok();
        }

        /// <summary>
        /// Exclui um unidade.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Id do unidade</param>
        /// <returns></returns>
        /// <response code="200">Sucesso ao excluir unidade</response>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _unitService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}