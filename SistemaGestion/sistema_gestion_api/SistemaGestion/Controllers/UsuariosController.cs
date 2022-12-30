using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaGestion.Models;
using SistemaGestion.Repositories;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private UsuarioRepository repository = new UsuarioRepository();
        [HttpGet]
        public ActionResult<List<Usuario>> Get()
        {
             try
             {
                List<Usuario> lista = repository.getUsuario();
                return Ok(lista);
             }
             catch (Exception ex)
             {
                return Problem(ex.Message);
             }
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult Post([FromBody] Usuario usuario)
        {
            try
            {
                Usuario crearUsuario = repository.CrearUsuario(usuario);
                return StatusCode(StatusCodes.Status201Created, crearUsuario);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("GetUsuario/{id}")]
        public ActionResult<Producto> GetUsuario(int id)
        {
            try
            {
                Usuario? usuario = repository.ObtenerUsuario(id);
                if (usuario != null)
                {
                    return Ok(usuario);
                }
                else
                {
                    return NotFound("El usuario no fue encontrado");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("[action]")]
        public ActionResult Delete([FromBody] int id)
        {
            try
            {
                bool EliminarUsuario = repository.EliminarUsuario(id);
                if (EliminarUsuario)
                {
                    return Ok();
                }
                else { return NotFound(); }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Usuario> Put(long id, [FromBody] Usuario usuarioAActualizar)
        {
            try
            {
                Usuario? usuarioActualizado = repository.ActualizarUsuario(id, usuarioAActualizar);
                if (usuarioActualizado != null)
                {
                    return Ok(usuarioActualizado);
                }
                else
                {
                    return NotFound("El usuario no fue encontrado");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

