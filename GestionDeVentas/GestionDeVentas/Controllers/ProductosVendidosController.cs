using Microsoft.AspNetCore.Mvc;
using GestionDeVentas.Repository;
using GestionDeVentas.Models;

namespace GestionDeVentas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosVendidosController : Controller
    {
        private ProductoVendidoRepository repository = new ProductoVendidoRepository();

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<ProductoVendido> lista = repository.listarProductoVendido();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] ProductoVendido productoV)
        {
            try
            {
                ProductoVendido productoVendido = repository.cargarProductosVendidos(productoV);
                return StatusCode(StatusCodes.Status201Created, productoVendido);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        //eliminar producto vendido desde IdVenta
        [HttpDelete]
        public ActionResult Delete([FromBody] long idVta)
        {
            try
            {
                bool seElimino = repository.eliminarProductoVendido(idVta);
                if (seElimino)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
