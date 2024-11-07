using CP3.Data.AppData;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CP3.Data.Repositories
{
    public class BarcoRepository : IBarcoRepository
    {
        private readonly ApplicationContext _context;

        public BarcoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public BarcoEntity? ObterPorId(int id)
        {
            return _context.Set<BarcoEntity>().Find(id);
        }

        public IEnumerable<BarcoEntity>? ObterTodos()
        {
            return _context.Set<BarcoEntity>().ToList();
        }

        public BarcoEntity? Adicionar(BarcoEntity barco)
        {
            _context.Set<BarcoEntity>().Add(barco);
            _context.SaveChanges();
            return barco;
        }

        public BarcoEntity? Editar(BarcoEntity barco)
        {
            var entity = _context.Set<BarcoEntity>().Find(barco.Id);
            if (entity == null) return null;

            entity.Nome = barco.Nome;
            entity.Modelo = barco.Modelo;
            entity.Ano = barco.Ano;
            entity.Tamanho = barco.Tamanho;

            _context.SaveChanges();
            return entity;
        }

        public BarcoEntity? Remover(int id)
        {
            var barco = ObterPorId(id);
            if (barco != null)
            {
                _context.Set<BarcoEntity>().Remove(barco);
                _context.SaveChanges();
            }
            return barco;
        }
    }
}
