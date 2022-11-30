using Microsoft.AspNetCore.Mvc;
using GestionDeVentas.Repository;
using GestionDeVentas.Models;

namespace GestionDeVentas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private UsuarioRepository repository = new UsuarioRepository();
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Usuario> lista = repository.listarUsuario();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}
