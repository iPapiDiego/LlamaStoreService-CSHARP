using LlamaStoreService.Models.Auditos;
using LlamaStoreService.Repositories.DAO;
using Microsoft.AspNetCore.Mvc;

namespace LlamaStoreService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditoriaController : Controller
    {
        [HttpGet("getAuditoria")]
        public async Task<ActionResult<List<Auditoria>>> GetAuditoria()
        {
            var lista = await Task.Run(() => new auditoriaDAO().ListaCambios());
            return Ok(lista);
        }
    }
}
