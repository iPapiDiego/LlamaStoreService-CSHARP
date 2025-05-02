using LlamaStoreService.Models.Productos;
using LlamaStoreService.Models.Users;
using LlamaStoreService.Repositories.DAO;
using Microsoft.AspNetCore.Mvc;
using static LlamaStoreService.Repositories.DAO.usuariosDAO;

namespace LlamaStoreService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        [HttpGet("getUsuarios")]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            var lista = await Task.Run(() => new usuariosDAO().listaDeUsuarios());
            return Ok(lista);
        }


        [HttpPost("postAgregaUsuarios")]
        public async Task<ActionResult<string>> PostAgregarUsuario(Usuario usu)
        {
            var mensaje = await Task.Run(() => new usuariosDAO().agregarUsuarios(usu));
            return Ok(mensaje);
        }

        [HttpPost("postActuaUsuarios")]
        public async Task<ActionResult<string>> PostActuaAgregarUsuario(Usuario usu)
        {
            var mensaje = await Task.Run(() => new usuariosDAO().actualizarUsuarios(usu));
            return Ok(mensaje);
        }

    }
}
