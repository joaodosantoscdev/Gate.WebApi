using AutoMapper;
using Gate.Application.DTOs.Request;
using Gate.Application.DTOs.Response;
using Gate.Application.Services;
using Gate.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceService _placeService;
        private readonly IMapper _mapper;
        
        public PlaceController(IPlaceService placeService, IMapper mapper)
        {
            _placeService = placeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém todos os contatos.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Retorna todos os contatos cadastrados</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(IEnumerable<PlaceResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaceResponse>>> ObterTodos()
        {
            var result = await _placeService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Obtém contato por Id.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Id do contato</param>
        /// <returns></returns>
        /// <response code="200">Retorna os dados do contato</response>
        /// <response code="404">Retorno caso o contato não seja encontrado</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(IEnumerable<PlaceInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaceInfo>> ObterPorId(int id)
        {
            var result = await _placeService.GetByIdAsync(id);
            if (result is null)
                return NotFound();
                
            return Ok(result);
        }

        /// <summary>
        /// Insere um contato.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param param name="AddPlaceRequest">Dados do contato</param>
        /// <returns></returns>
        /// <response code="201">Retorna o Id do contato criado</response>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<int>> Insert(AddPlaceRequest request)
        {
            //realizar automapper
            var place = _mapper.Map<PlaceInfo>(request);
            var id = await _placeService.AddAsync(place);
            return Ok(id);
        }

        /// <summary>
        /// Atualiza um contato.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="UpdatePlaceRequest">Dados do contato</param>
        /// <returns></returns>
        /// <response code="200">Sucesso ao atualizar contato</response>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<ActionResult> Update(UpdatePlaceRequest request)
        {
            var place = _mapper.Map<PlaceInfo>(request);
            await _placeService.UpdateAsync(place);
            return Ok();
        }

        /// <summary>
        /// Exclui um contato.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Id do contato</param>
        /// <returns></returns>
        /// <response code="200">Sucesso ao excluir contato</response>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _placeService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}