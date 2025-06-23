using Microsoft.AspNetCore.Mvc;

namespace LlamaStoreVista.Controllers
{
    public class CarritoController : Controller
    {
        public IActionResult carrito()
        {
            return View();
        }
    }
}
