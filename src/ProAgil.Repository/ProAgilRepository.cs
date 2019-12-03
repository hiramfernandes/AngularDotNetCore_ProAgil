using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Domain.Model;
using ProAgil.Repository.Data;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private ProAgilContext _context { get; }

        public ProAgilRepository(ProAgilContext context) => _context = context;

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            var query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            if (includePalestrantes)
            {
                query.Include(pe => pe.PalestrantesEventos)
                .ThenInclude(p => p.Palestrante);
            }

            return await query.AsNoTracking().OrderByDescending(o => o.DataEvento).ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrantes)
        {
            var query = _context.Eventos
                .AsNoTracking()
                .Where(e => e.Tema.ToLower().Contains(tema.ToLower()))
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            if (includePalestrantes)
            {
                query.Include(pe => pe.PalestrantesEventos)
                .ThenInclude(p => p.Palestrante);
            }

            return await query.AsNoTracking().OrderByDescending(o => o.DataEvento).ToArrayAsync();
        }

        public async Task<Evento> GetEventoAsyncById(int eventoId, bool includePalestrantes)
        {
            var query = _context.Eventos
                .Where(e => e.Id == eventoId)
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            if (includePalestrantes)
            {
                query.Include(pe => pe.PalestrantesEventos)
                .ThenInclude(p => p.Palestrante);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante> GetPalestranteAsync(int palestranteId, bool includeEvento = false)
        {
            var query = _context.Palestrantes
               .Where(e => e.Id == palestranteId)
               .AsNoTracking()
               .Include(c => c.RedesSociais);

            if (includeEvento)
            {
                query.Include(ev => ev.PalestrantesEventos);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
