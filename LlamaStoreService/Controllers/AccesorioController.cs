using LlamaStoreService.Models.Accesorios;
using LlamaStoreService.Models.Productos;
using LlamaStoreService.Repositories.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LlamaStoreService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesorioController : ControllerBase
    {
        [HttpGet("getAccesorios")]
        public async Task<ActionResult<List<ListaAccesorio>>> GetAccesorios()
        {
            var lista = await Task.Run(() => new accesorioDAO().listaDeAccesorios());
            return Ok(lista);
        }
    }
}
