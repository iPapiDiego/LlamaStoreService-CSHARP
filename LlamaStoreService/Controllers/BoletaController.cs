using LlamaStoreService.Models.Tickets;
using LlamaStoreService.Repositories.DAO;
using Microsoft.AspNetCore.Mvc;

namespace LlamaStoreService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoletaController : Controller
    {
        [HttpGet("getBoleta")]
        public async Task<ActionResult<List<ListaBoleta>>> GetBoletas()
        {
            var lista = await Task.Run(() => new ticketDAO().listaVentas());
            return Ok(lista);
        }
    }
}
