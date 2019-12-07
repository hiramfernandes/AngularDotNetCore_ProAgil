using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ProAgil.Domain.Model;
using ProAgil.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private IProAgilRepository _repository;

        public EventosController(IProAgilRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _repository.GetAllEventosAsync(true));
            }
            catch (SystemException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _repository.GetEventoAsyncById(id, true));
            }
            catch (SystemException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                return Ok(await _repository.GetAllEventosAsyncByTema(tema, true));
            }
            catch (SystemException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento evento)
        {
            try
            {
                _repository.Add(evento);
                if (await _repository.SaveChangesAsync())
                    return Created($"/api/eventos/{evento.Id}", evento);
            }
            catch (SystemException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Evento evento)
        {
            try
            {
                var entity = await _repository.GetEventoAsyncById(evento.Id, false);
                if (entity == null)
                {
                    return NotFound();
                }
                else
                {
                    entity.ImageUrl = evento.ImageUrl;
                    entity.Tema = evento.Tema;
                    entity.Local = evento.Local;
                    entity.QtdPessoas = evento.QtdPessoas;
                    entity.DataEvento = evento.DataEvento;

                    _repository.Update(entity);
                    if (await _repository.SaveChangesAsync())
                        return Created($"/api/eventos/{evento.Id}", evento);

                }

            }
            catch (SystemException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }

        [HttpDelete("{eventoId}")]
        public async Task<IActionResult> Delete(int eventoId)
        {
            try
            {
                var evento = await _repository.GetEventoAsyncById(eventoId, false);
                if (evento == null)
                {
                    return NotFound();
                }
                else
                {
                    _repository.Delete(evento);
                    if (await _repository.SaveChangesAsync())
                        return Ok();
                }
            }
            catch (SystemException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }
    }
}
