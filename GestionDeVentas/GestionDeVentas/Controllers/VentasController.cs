using Microsoft.AspNetCore.Mvc;
using GestionDeVentas.Repository;
using GestionDeVentas.Models;

namespace GestionDeVentas.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VentaController : Controller
    {
        VentaRepository repository = new VentaRepository();

        [HttpPost]
        public ActionResult Post([FromBody] Venta venta)
        {
            try
            {
                repository.RegistrarVenta(venta);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(repository.obtenerVenta2(null));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
