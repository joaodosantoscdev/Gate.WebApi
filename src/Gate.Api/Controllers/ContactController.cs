using AutoMapper;
using Gate.Application.DTOs.Request;
using Gate.Application.Services;
using Gate.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;
        
        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
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
        [ProducesResponseType(typeof(IEnumerable<ContactInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactInfo>>> ObterTodos()
        {
            var result = await _contactService.GetAllAsync();
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
        [ProducesResponseType(typeof(IEnumerable<ContactInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactInfo>> ObterPorId(int id)
        {
            var result = await _contactService.GetByIdAsync(id);
            if (result is null)
                return NotFound();
                
            return Ok(result);
        }

        /// <summary>
        /// Insere um contato.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param param name="AddContactRequest">Dados do contato</param>
        /// <returns></returns>
        /// <response code="201">Retorna o Id do contato criado</response>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<int>> Insert(AddContactRequest request)
        {
            //realizar automapper
            var contact = _mapper.Map<ContactInfo>(request);
            var id = (int)await _contactService.AddAsync(contact);
            return CreatedAtAction($"Inserido contato #{id}", new { id = id }, id);
        }

        /// <summary>
        /// Atualiza um contato.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="UpdateContactRequest">Dados do contato</param>
        /// <returns></returns>
        /// <response code="200">Sucesso ao atualizar contato</response>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<ActionResult> Update(UpdateContactRequest request)
        {
            var contact = _mapper.Map<ContactInfo>(request);
            await _contactService.UpdateAsync(contact);
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
            await _contactService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}