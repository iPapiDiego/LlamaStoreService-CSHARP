using LlamaStoreVista.Models.Carrito;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LlamaStoreVista.Controllers
{
    public class CarritoController : Controller
    {

        private readonly string conexionListado = "https://localhost:44331/api/Carrito/getIdUsuarios";
        private readonly string conexionDeleteCarrito = "https://localhost:44331/api/Carrito/deleterCarrito";

        public async Task<IActionResult> IndexCarrito(int codigo)
        {
            var userIdClaim = User.FindFirst("codigo");
            if (userIdClaim == null)
                return RedirectToAction("Login", "Auth"); // Si no está logueado

            int userId = int.Parse(userIdClaim.Value);
            var lista = await ObtenerCarritoById(userId);

            return View("IndexCarrito", lista);
        }


        public async Task<List<Carrito>> ObtenerCarritoById(int codigo)
        {
            var temporal = new List<Carrito>();
            using var client = new HttpClient { BaseAddress = new Uri(conexionListado) };

            var resp = await client.GetAsync($"getIdUsuarios?codigo={codigo}");
            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadAsStringAsync();
                temporal = JsonConvert.DeserializeObject<List<Carrito>>(data) ?? new List<Carrito>();
            }
            return temporal;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart(int codigoUsuario)
        {
            var lista = await ObtenerCarritoById(codigoUsuario);
            return Json(lista);
        }



        public async Task<IActionResult> EliminarCarrito(string idCarrito)
        {
            if (string.IsNullOrEmpty(idCarrito))
                return RedirectToAction("IndexCarrito");

            string mensaje = "";
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(conexionDeleteCarrito);
                HttpResponseMessage response = await client.DeleteAsync("deleterCarrito/?idCarrito=" + idCarrito);
                string apiResponse = await response.Content.ReadAsStringAsync();
                mensaje = apiResponse;
            }
            TempData["mensaje"] = mensaje;
            return RedirectToAction("IndexCarrito");
        }
    }
}
