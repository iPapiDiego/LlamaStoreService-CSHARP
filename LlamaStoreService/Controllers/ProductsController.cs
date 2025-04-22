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

        [HttpPost("postMergeCelular")]
        public async Task<ActionResult<string>> mergeCelular(CelularCrear cel)
        {
            var mensaje = await Task.Run(() => new productoDAO().mergeCelulares(cel));
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
