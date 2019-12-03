using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ProAgil.Domain;
using ProAgil.Repository;
using System;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalestranteController : ControllerBase
    {
        private IProAgilRepository _repository;
        public PalestranteController(IProAgilRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Post(Palestrante palestrante)
        {
            try
            {
                _repository.GetAllEventosAsync(true);
                return Ok();
            }
            catch(SystemException)
            {

            }
            return null;
        }
    }
}
