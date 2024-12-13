using Bogus;

using FIAP_Persistencia.Application.Mapper;
using FIAP_Persistencia.Application.Model;
using FIAP_Persistencia.Application.Service;
using FIAP_Persistencia.Dominio.Entity;
using FIAP_Persistencia.Dominio.Repository;
using FIAP_Persistencia.Dominio.Service;

using Microsoft.AspNetCore.Mvc;

using Moq;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace FIAP_Persistencia.Test.Unit;

public class ContatoController
{
    private readonly Faker<ContatoModel> _faker;
    private readonly Faker<Contato> _fakerEntity;
    private AutoMapper.IMapper _mapper;

    public ContatoController()
    {
        _faker = new Faker<ContatoModel>("pt_BR")
            .RuleFor(f => f.Nome, f => f.Name.FullName())
            .RuleFor(f => f.Telefone, f => f.Phone.PhoneNumberFormat())
            .RuleFor(f => f.Email, f => f.Internet.Email());

        _fakerEntity = new Faker<Contato>("pt_BR")
                .RuleFor(f => f.Id, f => 1)
                .RuleFor(f => f.Nome, f => f.Name.FullName())
                .RuleFor(f => f.Telefone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.DDD, f => f.PickRandom(new[] { "11", "21", "31", "12" }))
                .RuleFor(f => f.Email, f => f.Internet.Email());

        _mapper = MapperConfiguration.RegisterMapping();
    }


    [Fact]
    [Trait("Categoria", "Unit")]
    public void ObterTodosContatos_Sucesso()
    {
        // Arrange
        var mockRepository = new Mock<IContatoRepository>();
        var listaDeEntidades = _fakerEntity.Generate(10);
        var listaDeModelos = _mapper.Map<List<ContatoModelResponse>>(listaDeEntidades);

        mockRepository.Setup(c => c.ObterTodosAsync()).ReturnsAsync(listaDeEntidades);

        var contatoDomain = new ContatoDomainService(mockRepository.Object);
        var contatoService = new ContatoApplicationService(contatoDomain, _mapper);
        var contatoController = new FIAP.Persistencia.Controllers.ContatoController(contatoService);

        // Act
        var response = (ObjectResult)contatoController.ObterTodosContatos(null).Result;
        var responseModel = (List<ContatoModelResponse>)response.Value;

        // Assert
        bool todasEntidadesPresentes = listaDeModelos.All(m => responseModel.Any(r => r.Nome == m.Nome && r.Email == m.Email && r.Telefone == m.Telefone));
        Assert.True(todasEntidadesPresentes);
        Assert.Equal(200, response.StatusCode);
    }

    [Fact]
    [Trait("Categoria", "Unit")]
    public void ObterTodosContatos_Parametro_DDD_Sucesso()
    {
        // Arrange
        var mockRepository = new Mock<IContatoRepository>();
        var listaDeEntidades = _fakerEntity.Generate(10);

        // Defina um DDD para filtrar
        var dddFiltrado = "11";
        var listaDeEntidadesFiltradas = listaDeEntidades.Where(e => e.DDD == dddFiltrado).ToList();
        var listaDeModelosFiltrados = _mapper.Map<List<ContatoModelResponse>>(listaDeEntidadesFiltradas);

        mockRepository.Setup(c => c.ObterTodosAsync()).ReturnsAsync(listaDeEntidades);

        var contatoDomain = new ContatoDomainService(mockRepository.Object);
        var contatoService = new ContatoApplicationService(contatoDomain, _mapper);
        var contatoController = new FIAP.Persistencia.Controllers.ContatoController(contatoService);

        // Act
        var response = (ObjectResult)contatoController.ObterTodosContatos(dddFiltrado).Result;
        var responseModel = (List<ContatoModelResponse>)response.Value;

        // Assert
        bool todasEntidadesFiltradasPresentes = listaDeModelosFiltrados.All(m => responseModel.Any(r => r.Nome == m.Nome && r.Email == m.Email && r.Telefone == m.Telefone));

        Assert.True(todasEntidadesFiltradasPresentes);
        Assert.Equal(200, response.StatusCode);
        Assert.Equal(listaDeModelosFiltrados.Count, responseModel.Count);
    }

    [Fact]
    [Trait("Categoria", "Unit")]
    public void ObterTodosContatos_NenhumContatoEncontrado()
    {
        // Arrange
        var mockRepository = new Mock<IContatoRepository>();
        mockRepository.Setup(c => c.ObterTodosAsync()).ReturnsAsync(new List<Contato>());

        var contatoDomain = new ContatoDomainService(mockRepository.Object);
        var contatoService = new ContatoApplicationService(contatoDomain, _mapper);
        var contatoController = new FIAP.Persistencia.Controllers.ContatoController(contatoService);

        // Act
        var response = contatoController.ObterTodosContatos(null).Result;

        // Assert
        Assert.Equal(404, ((StatusCodeResult)response).StatusCode);
    }

    [Fact]
    [Trait("Categoria", "Unit")]
    public void GetAllContatos_Erro_Retorno_Banco()
    {
        // Arrange
        var mockRepository = new Mock<IContatoRepository>();
        mockRepository.Setup(c => c.ObterTodosAsync()).ThrowsAsync(new Exception("Erro na conexão!"));

        var contatoDomain = new ContatoDomainService(mockRepository.Object);
        var contatoService = new ContatoApplicationService(contatoDomain, _mapper);
        var contatoController = new FIAP.Persistencia.Controllers.ContatoController(contatoService);

        // Act
        var ex = Assert.ThrowsAsync<Exception>(async () => await contatoController.ObterTodosContatos(null));

        // Assert
        Assert.Equal("Erro na conexão!", ex.Result.Message);
    }

}
