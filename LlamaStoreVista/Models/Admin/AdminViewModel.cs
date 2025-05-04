using LlamaStoreService.Models.Users;
using LlamaStoreVista.Models.Product;

namespace LlamaStoreVista.Models.Admin
{
    public class AdminViewModel
    {
        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public List<Producto> Productos { get; set; } = new List<Producto>();
        public List<Accesorio> Accesorios { get; set; } = new List<Accesorio>();

        public int PaginaActualUsuarios { get; set; }
        public int TotalPaginasUsuarios { get; set; }

        public int PaginaActualProductos { get; set; }
        public int TotalPaginasProductos { get; set; }

        public Usuario NuevoUsuario { get; set; } = new Usuario();
        public Producto NuevoProducto { get; set; } = new Producto();
    }
}
