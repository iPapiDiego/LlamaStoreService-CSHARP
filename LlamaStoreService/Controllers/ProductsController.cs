using LlamaStoreService.Models.Productos;
using LlamaStoreService.Models.Products;
using LlamaStoreService.Repositories.DAO;
using Microsoft.AspNetCore.Mvc;

namespace LlamaStoreService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : Controller
    {
        [HttpGet("getCelulares")]
        public async Task<ActionResult<List<CelularLista>>> GetCelulares()
        {
            var lista = await Task.Run(() => new productoDAO().listaDeCelulares());
            return Ok(lista);
        }

        [HttpPost("postAgregarCelular")]
        public async Task<ActionResult<string>> postCrear(CelularCrear cel)
        {
            var mensaje = await Task.Run(() => new productoDAO().agregarCelulares(cel));
            return Ok(mensaje);
        }

        [HttpPost("postActualizarCelular")]
        public async Task<ActionResult<string>> postActualiza(CelularCrear cel)
        {
            var mensaje = await Task.Run(() => new productoDAO().actualizarCelulares(cel));
            return Ok(mensaje);
        }

        [HttpDelete("deleteCelular")]
        public async Task<ActionResult<string>> DeleteCelular(string id)
        {
            var mensaje = await Task.Run(() => new productoDAO().eliminarCelular(id));
            return Ok(mensaje);
        }

    }



}
