using Microsoft.AspNetCore.Mvc;
using GestionDeVentas.Repository;
using GestionDeVentas.Models;



namespace GestionDeVentas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : Controller
    {
        private ProductoRepository repository = new ProductoRepository();

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Producto> lista = repository.listarProducto();
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
