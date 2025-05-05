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

        [HttpGet("getBuscarCelular")]
        public async Task<ActionResult<CelularLista>> GetBucarCelular(string id)
        {
            var celular = await Task.Run(() => new productoDAO().buscarCelular(id));
            return Ok(celular);
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


        //LISTAS DE SUBTABLAS

        [HttpGet("getMarcas")]
        public async Task<ActionResult<List<CelularLista>>> GetMarcas()
        {
            var lista = await Task.Run(() => new productoDAO().listaDeMarcas());
            return Ok(lista);
        }

        [HttpGet("getGamas")]
        public async Task<ActionResult<List<CelularLista>>> GetGamas()
        {
            var lista = await Task.Run(() => new productoDAO().listaDeGamas());
            return Ok(lista);
        }

        [HttpGet("getSistemas")]
        public async Task<ActionResult<List<CelularLista>>> GetSistemas()
        {
            var lista = await Task.Run(() => new productoDAO().listaDeSistemas());
            return Ok(lista);
        }


        [HttpGet("getEstados")]
        public async Task<ActionResult<List<CelularLista>>> GetEstados()
        {
            var lista = await Task.Run(() => new productoDAO().listaEstados());
            return Ok(lista);
        }

        [HttpGet("getRoles")]
        public async Task<ActionResult<List<CelularLista>>> GetRoles()
        {
            var lista = await Task.Run(() => new productoDAO().listaRoles());
            return Ok(lista);
        }

        [HttpGet("getTipoAcce")]
        public async Task<ActionResult<List<CelularLista>>> GetTipoAcce()
        {
            var lista = await Task.Run(() => new productoDAO().listaTiposAccesorios());
            return Ok(lista);
        }

        [HttpGet("getModeloAcce")]
        public async Task<ActionResult<List<CelularLista>>> GetModeloAcce()
        {
            var lista = await Task.Run(() => new productoDAO().listaModelosAccesorios());
            return Ok(lista);
        }

    }



}
