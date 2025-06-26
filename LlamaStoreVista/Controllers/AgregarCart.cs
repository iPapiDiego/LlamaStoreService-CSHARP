using Microsoft.AspNetCore.Mvc;
using LlamaStoreVista.Models.Carrito;
using Newtonsoft.Json;

namespace LlamaStoreVista.Controllers
{
    public class AgregarCart : Controller
    {
        private readonly string baseApi = "https://localhost:44331/api/Carrito/";

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] Carrito req)
        {
            using var client = new HttpClient { BaseAddress = new Uri(baseApi) };
            var resp = await client.PostAsJsonAsync("getAgregar", req); // getAgregar va aquí, no en la base
            return resp.IsSuccessStatusCode ? Ok() : StatusCode((int)resp.StatusCode);
        }

        [HttpGet]
        public async Task<JsonResult> GetCart(int codigoUsuario)
        {
            using var client = new HttpClient { BaseAddress = new Uri(baseApi) };
            var resp = await client.GetAsync($"getIdUsuarios?codigo={codigoUsuario}");
            if (!resp.IsSuccessStatusCode) return Json(new List<Carrito>());

            var data = JsonConvert.DeserializeObject<List<Carrito>>(await resp.Content.ReadAsStringAsync())
                       ?? new();
            return Json(data);
        }
    }
}
