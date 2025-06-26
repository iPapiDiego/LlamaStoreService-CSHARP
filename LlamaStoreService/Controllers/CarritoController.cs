using LlamaStoreService.Models.Car;
using LlamaStoreService.Repositories.DAO;
using Microsoft.AspNetCore.Mvc;

namespace LlamaStoreService.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        
            private readonly carritoDAO _repo;

            public CarritoController(carritoDAO repo)
            {
            _repo = repo;
            }

            [HttpGet("getIdUsuarios")]
            public async Task<ActionResult<List<Carrito>>> ObtenerCarrito(int codigo)
            {
                var carrito = await Task.Run(() => new carritoDAO().ListadoPorId(codigo));
                return Ok(carrito);
            }


            [HttpPost("getAgregar")]

            public async Task<ActionResult<string>> Agregar(Carrito model)
            {

                var result = await Task.Run(() => new carritoDAO().agregarAlCarrito(model));
                return Ok(result);
            }

            [HttpDelete("deleterCarrito")]

            public async Task<ActionResult<string>> DeleteCarrito(int idCarrito)
            {

                var result = await Task.Run(() => new carritoDAO().eliminarItemCarrito(idCarrito));
                return Ok(result);
            }





        
    }
}
