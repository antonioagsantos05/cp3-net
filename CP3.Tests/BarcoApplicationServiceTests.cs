using CP3.Application.Services;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CP3.Tests
{
    public class BarcoApplicationServiceTests
    {
        private readonly Mock<IBarcoRepository> _repositoryMock;
        private readonly BarcoApplicationService _barcoService;

        public BarcoApplicationServiceTests()
        {
            _repositoryMock = new Mock<IBarcoRepository>();
            _barcoService = new BarcoApplicationService(_repositoryMock.Object);
        }

        [Fact]
        public void ObterTodosBarcos_DeveRetornarTodosOsBarcos()
        {
            _repositoryMock.Setup(repo => repo.ObterTodos()).Returns(new List<BarcoEntity>
            {
                new BarcoEntity { Nome = "Barco1", Modelo = "ModeloA", Ano = 2020, Tamanho = 15.5 },
                new BarcoEntity { Nome = "Barco2", Modelo = "ModeloB", Ano = 2019, Tamanho = 10.0 }
            });

            var result = _barcoService.ObterTodosBarcos();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void ObterBarcoPorId_DeveRetornarBarcoQuandoExistir()
        {
            var barco = new BarcoEntity { Id = 1, Nome = "Barco1", Modelo = "ModeloA", Ano = 2020, Tamanho = 15.5 };
            _repositoryMock.Setup(repo => repo.ObterPorId(1)).Returns(barco);

            var result = _barcoService.ObterBarcoPorId(1);

            Assert.NotNull(result);
            Assert.Equal("Barco1", result.Nome);
        }

        [Fact]
        public void AdicionarBarco_DeveAdicionarENaoSerNulo()
        {
            var barcoDto = new Mock<IBarcoDto>();
            barcoDto.Setup(b => b.Nome).Returns("Barco1");
            barcoDto.Setup(b => b.Modelo).Returns("ModeloA");
            barcoDto.Setup(b => b.Ano).Returns(2020);
            barcoDto.Setup(b => b.Tamanho).Returns(15.5);

            var barco = new BarcoEntity { Nome = "Barco1", Modelo = "ModeloA", Ano = 2020, Tamanho = 15.5 };
            _repositoryMock.Setup(repo => repo.Adicionar(It.IsAny<BarcoEntity>())).Returns(barco);

            var result = _barcoService.AdicionarBarco(barcoDto.Object);

            Assert.NotNull(result);
            Assert.Equal("Barco1", result.Nome);
        }

        [Fact]
        public void EditarBarco_DeveAtualizarBarco()
        {
            var barcoDto = new Mock<IBarcoDto>();
            barcoDto.Setup(b => b.Nome).Returns("Barco1 Atualizado");
            barcoDto.Setup(b => b.Modelo).Returns("ModeloA");
            barcoDto.Setup(b => b.Ano).Returns(2020);
            barcoDto.Setup(b => b.Tamanho).Returns(15.5);

            var barcoExistente = new BarcoEntity { Id = 1, Nome = "Barco1", Modelo = "ModeloA", Ano = 2020, Tamanho = 15.5 };
            _repositoryMock.Setup(repo => repo.ObterPorId(1)).Returns(barcoExistente);
            _repositoryMock.Setup(repo => repo.Editar(It.IsAny<BarcoEntity>())).Returns(barcoExistente);

            var result = _barcoService.EditarBarco(1, barcoDto.Object);

            Assert.NotNull(result);
            Assert.Equal("Barco1 Atualizado", result.Nome);
        }

        [Fact]
        public void RemoverBarco_DeveRemoverBarco()
        {
            var barco = new BarcoEntity { Id = 1, Nome = "Barco1", Modelo = "ModeloA", Ano = 2020, Tamanho = 15.5 };
            _repositoryMock.Setup(repo => repo.ObterPorId(1)).Returns(barco);
            _repositoryMock.Setup(repo => repo.Remover(1)).Returns(barco);

            var result = _barcoService.RemoverBarco(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }
    }
}
