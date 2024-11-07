using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;
using System.Collections.Generic;

namespace CP3.Application.Services
{
    public class BarcoApplicationService : IBarcoApplicationService
    {
        private readonly IBarcoRepository _repository;

        public BarcoApplicationService(IBarcoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<BarcoEntity> ObterTodosBarcos()
        {
            return _repository.ObterTodos();
        }

        public BarcoEntity ObterBarcoPorId(int id)
        {
            return _repository.ObterPorId(id);
        }

        public BarcoEntity AdicionarBarco(IBarcoDto entity)
        {
            var barco = new BarcoEntity
            {
                Nome = entity.Nome,
                Modelo = entity.Modelo,
                Ano = entity.Ano,
                Tamanho = entity.Tamanho
            };
            return _repository.Adicionar(barco);
        }

        public BarcoEntity EditarBarco(int id, IBarcoDto entity)
        {
            var barcoExistente = _repository.ObterPorId(id);
            if (barcoExistente == null)
            {
                return null; 
            }

            barcoExistente.Nome = entity.Nome;
            barcoExistente.Modelo = entity.Modelo;
            barcoExistente.Ano = entity.Ano;
            barcoExistente.Tamanho = entity.Tamanho;

            return _repository.Editar(barcoExistente);
        }

        public BarcoEntity RemoverBarco(int id)
        {
            return _repository.Remover(id);
        }
    }
}
