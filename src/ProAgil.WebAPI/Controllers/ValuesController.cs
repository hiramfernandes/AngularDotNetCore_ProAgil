using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.WebAPI.Data;
using ProAgil.WebAPI.Model;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly Evento[] listaEventos;
        private DataContext _context;

        public ValuesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async  Task<IActionResult> Get(int id)
        {
            try
            {
                var eventos = _context.Eventos;
                return Ok(await eventos.FirstOrDefaultAsync(x => x.EventoId == id));
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
    }
}