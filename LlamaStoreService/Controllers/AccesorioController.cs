using LlamaStoreService.Models.Accesorios;
using LlamaStoreService.Repositories.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LlamaStoreService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //hola como estan
    public class AccesorioController : ControllerBase
    {
        //BÑANCA
        [HttpGet("getAccesorios")]
        public async Task<ActionResult<List<ListaAccesorio>>> GetAccesorios()
        {
            var lista = await Task.Run(() => new accesorioDAO().listaDeAccesorios());
            return Ok(lista);
        }

        [HttpGet("getBuscarAccesrio")]
        public async Task<ActionResult<ListaAccesorio>> GetBucarAccesesorio(int id)
        {
            var accesorio = await Task.Run(() => new accesorioDAO().buscarAccesorio(id));
            return Ok(accesorio);
        }

        [HttpPost("postAgregarAccesorio")]
        public async Task<ActionResult<string>> PosAgregarAccesorio(CrudAccesorio acce)
        {
            var mensaje = await Task.Run(() => new accesorioDAO().agregarAccesorio(acce));
            return Ok(mensaje);
        }

        [HttpPost("postActualizarAccesorio")]
        public async Task<ActionResult<string>> PostActualizarAccesorio(CrudAccesorio acce)
        {
            var mensaje = await Task.Run(() => new accesorioDAO().actualizarAccesorio(acce));
            return Ok(mensaje);
        }

        [HttpDelete("deleteAccesorio")]
        public async Task<ActionResult<string>> DeleteAccesorio(string id)
        {
            var mensaje = await Task.Run(() => new accesorioDAO().eliminarAccesorio(id));
            return Ok(mensaje);
        }

        [HttpGet("getTiposAcce")]
        public async Task<ActionResult<List<Tipo>>> GetTiposAcce()
        {
            var lista = await Task.Run(() => new accesorioDAO().listaTipos());
            return Ok(lista);
        }

        [HttpGet("getModeloAcce")]
        public async Task<ActionResult<List<Modelo>>> GetModeloAcce()
        {
            var lista = await Task.Run(() => new accesorioDAO().listaModelos());
            return Ok(lista);
        }
    }
}
