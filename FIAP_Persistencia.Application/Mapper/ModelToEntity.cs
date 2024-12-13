using FIAP_Persistencia.Application.Model;
using FIAP_Persistencia.Dominio.Entity;


using AutoMapper;

namespace FIAP_Persistencia.Application.Mapper
{
    public class ModelToEntity : Profile
    {
        public ModelToEntity()
        {
            CreateMap<ContatoModel, Contato>();
        }
    }
}
