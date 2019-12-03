using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain.Model;
using ProAgil.Repository.Data;
using System;
using System.Threading.Tasks;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private ProAgilContext _context;

        public ValuesController(ProAgilContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async  Task<IActionResult> Get(int id)
        {
            try
            {
                var eventos = _context.Eventos;
                return Ok(await eventos.FirstOrDefaultAsync(x => x.Id == id));
            }
            catch (SystemException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var eventos = await _context.Eventos.ToListAsync();
                return Ok(eventos);
            }
            catch (SystemException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<IActionResult> Post(Evento evento)
        {
            await _context.Eventos.AddAsync(evento);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Evento evento)
        {
            //Get the existing Event
            var entity = _context.Eventos.Find(evento.Id);
            if (entity != null)
            {
                entity.ImageUrl = evento.ImageUrl;
                entity.Tema = evento.Tema;
                entity.Local = evento.Local;
                entity.QtdPessoas = evento.QtdPessoas;
                entity.DataEvento = evento.DataEvento;

                _context.Update(entity);
                _context.SaveChanges();
            }
            else return StatusCode(StatusCodes.Status404NotFound);

            return Ok();
        }
    }
}