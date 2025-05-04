namespace LlamaStoreService.Models.Accesorios
{
    public class CrudAccesorio
    {
        public int idacce { get; set; }
        public int idtipo { get; set; }
        public int idmarca { get; set; }
        public int idmodelo { get; set; }
        public decimal precio { get; set; }
        public decimal precio_oferta { get; set; }
        public int stock { get; set; }
        public string descripcion { get; set; }
    }
}
