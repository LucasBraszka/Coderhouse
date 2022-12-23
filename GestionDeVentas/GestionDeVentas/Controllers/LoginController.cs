using Microsoft.AspNetCore.Mvc;
using GestionDeVentas.Models;
using GestionDeVentas.Repository;

namespace GestionDeVentas.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LoginController : Controller
    {
        LoginRepository repository = new LoginRepository();

        [HttpPost]
        public ActionResult<Usuario> Login(Usuario usuario)
        {
            try
            {
                bool usuarioExiste = repository.verificarUsuario(usuario);
                return usuarioExiste ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
