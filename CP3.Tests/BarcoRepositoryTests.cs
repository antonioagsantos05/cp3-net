using CP3.Data.AppData;
using CP3.Data.Repositories;
using CP3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CP3.Tests
{
    public class BarcoRepositoryTests
    {
        private readonly DbContextOptions<ApplicationContext> _options;
        private readonly ApplicationContext _context;
        private readonly BarcoRepository _barcoRepository;

        public BarcoRepositoryTests()
        {
            
            _options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationContext(_options);
            _barcoRepository = new BarcoRepository(_context);
        }

        [Fact]
        public void Adicionar_DeveAdicionarBarco()
        {
            
            var barco = new BarcoEntity { Nome = "Barco1", Modelo = "ModeloA", Ano = 2020, Tamanho = 15.5 };

            
            var result = _barcoRepository.Adicionar(barco);

            
            Assert.NotNull(result);
            Assert.Equal("Barco1", result.Nome);
            Assert.Equal(1, _context.Set<BarcoEntity>().Count());
        }

        [Fact]
        public void ObterPorId_DeveRetornarBarcoQuandoExistir()
        {
            
            var barco = new BarcoEntity { Nome = "Barco2", Modelo = "ModeloB", Ano = 2018, Tamanho = 20.0 };
            _context.Set<BarcoEntity>().Add(barco);
            _context.SaveChanges();

            
            var result = _barcoRepository.ObterPorId(barco.Id);

            
            Assert.NotNull(result);
            Assert.Equal("Barco2", result.Nome);
        }

        [Fact]
        public void ObterTodos_DeveRetornarTodosOsBarcos()
        {
            
            _context.Set<BarcoEntity>().Add(new BarcoEntity { Nome = "Barco3", Modelo = "ModeloC", Ano = 2022, Tamanho = 10.0 });
            _context.Set<BarcoEntity>().Add(new BarcoEntity { Nome = "Barco4", Modelo = "ModeloD", Ano = 2021, Tamanho = 8.0 });
            _context.SaveChanges();

            
            var result = _barcoRepository.ObterTodos();

            
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Editar_DeveAtualizarBarco()
        {
            
            var barco = new BarcoEntity { Nome = "Barco5", Modelo = "ModeloE", Ano = 2019, Tamanho = 12.0 };
            _context.Set<BarcoEntity>().Add(barco);
            _context.SaveChanges();

            
            barco.Nome = "Barco5 Atualizado";
            var result = _barcoRepository.Editar(barco);

            
            Assert.NotNull(result);
            Assert.Equal("Barco5 Atualizado", result.Nome);
        }

        [Fact]
        public void Remover_DeveExcluirBarco()
        {
            
            var barco = new BarcoEntity { Nome = "Barco6", Modelo = "ModeloF", Ano = 2017, Tamanho = 18.0 };
            _context.Set<BarcoEntity>().Add(barco);
            _context.SaveChanges();

            
            _barcoRepository.Remover(barco.Id);

            
            Assert.Null(_context.Set<BarcoEntity>().Find(barco.Id));
        }
    }
}
