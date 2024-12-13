using AutoMapper;

using FIAP_Persistencia.Application.Interface;
using FIAP_Persistencia.Application.Model;
using FIAP_Persistencia.Dominio.Entity;
using FIAP_Persistencia.Dominio.Repository;



namespace FIAP_Persistencia.Application.Service
{
    public class ContatoApplicationService : IContatoApplicationService
    
    {
        private readonly IContatoDomainService _contatoDomainService;
       // private readonly IContatoProducer _producer;
        private IMapper _mapper;

        public ContatoApplicationService(IContatoDomainService contatoDomainService, IMapper mapper)
        {
            _contatoDomainService = contatoDomainService;
            _mapper = mapper;
        }

        public async Task<string> AtualizarContato(int id, ContatoModel request)
        {
            var req = _mapper.Map<Contato>(request);
            req.Id = id;

            return await _contatoDomainService.AtualizarContato(req);
        }

        public async Task<string> CadastrarContato(ContatoModel request)
        {
            var contato = _mapper.Map<Contato>(request);
            _contatoDomainService.TratarContato(contato);

            var contatoExiste = await _contatoDomainService.VerificarContatoExistente(contato);

            if (contatoExiste)
            {
                throw new InvalidOperationException("Contato já existe!");
            }

            await _contatoDomainService.CadastrarContato(contato);

            return "Contato cadastrado com sucesso!";
        }

        public async Task<string> DeletarContato(int id)
        {
            return await _contatoDomainService.DeletarContato(id);
        }

        public async Task<IEnumerable<ContatoModelResponse>> ObterTodosContatos(string? ddd)
        {
            var res = await _contatoDomainService.ObterTodosContatos(ddd);

            return _mapper.Map<IEnumerable<ContatoModelResponse>>(res);
        }
    }
}
