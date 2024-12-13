using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_Persistencia.Application.Mapper
{
    public class MapperConfiguration
    {
        public static IMapper RegisterMapping()
        {
            return new AutoMapper.MapperConfiguration(mc =>
            {
                mc.AddProfile<EntityToModel>();
                mc.AddProfile<ModelToEntity>();
            }).CreateMapper();
        }
    }
}
