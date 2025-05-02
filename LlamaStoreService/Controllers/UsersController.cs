using LlamaStoreService.Models.Users;
using LlamaStoreService.Repositories.DAO;
using Microsoft.AspNetCore.Mvc;

namespace LlamaStoreService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        [HttpGet("getUsuarios")]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            var lista = await Task.Run(() => new userDAO().listaDeUsuarios());
            return Ok(lista);
        }

        [HttpPost("postAgregaUsuarios")]
        public async Task<ActionResult<string>> PostAgregarUsuario(Usuario usu)
        {
            var mensaje = await Task.Run(() => new userDAO().agregarUsuarios(usu));
            return Ok(mensaje);
        }

        [HttpPost("postActuaUsuarios")]
        public async Task<ActionResult<string>> PostActuaAgregarUsuario(Usuario usu)
        {
            var mensaje = await Task.Run(() => new userDAO().actualizarUsuarios(usu));
            return Ok(mensaje);
        }

        [HttpDelete("deleteUsuario")]
        public async Task<ActionResult<string>> DeleteUsuario(int id)
        {
            var mensaje= await Task.Run(()=> new userDAO().eliminarUsuario(id));
            return Ok(mensaje);
        }

        
    }
}
