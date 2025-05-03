using LlamaStoreService.Models.Users;

namespace LlamaStoreVista.Models.Admin
{
    public class AdminViewModel
    {
        public List<Producto> Productos { get; set; } = new();
        public Usuario UsuarioNuevo { get; set; } = new();
        public Producto ProductoNuevo { get; set; } = new();

        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
    }
}
