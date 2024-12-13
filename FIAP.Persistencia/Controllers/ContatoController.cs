using FIAP_Persistencia.Application.Interface;

using Microsoft.AspNetCore.Mvc;

namespace FIAP.Persistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    
    {

        private IContatoApplicationService _contatoApplicationService;
        public ContatoController(IContatoApplicationService contatoApplicationService) => _contatoApplicationService = contatoApplicationService;



        /// <summary>
        /// Buscar todos contatos ou por DDD
        /// </summary>
        /// <param name="request">Informação do contato</param>
        /// <returns></returns>
        /// <response code="204">Solicitação executada com sucesso</response>
        /// <response code="400">Falha ao processar requisição</response>
        /// <response code="404">Contato não encontrado</response>
        /// <response code="500">Erro do Servidor</response>
        [HttpGet]
        public async Task<IActionResult> ObterTodosContatos(string? ddd)
        {
            var response = await _contatoApplicationService.ObterTodosContatos(ddd);

            return response.Any() ? Ok(response) : NotFound();
        }
    }
}
