namespace LlamaStoreService.Models.Accesorios
{
    public class CrudAccesorio
    {
        public int idacce { get; set; }
        public string idtipo { get; set; }
        public string idmarca { get; set; }
        public string idmodelo { get; set; }
        public decimal precio { get; set; }
        public decimal precio_oferta { get; set; }
        public int stock { get; set; }
        public string descripcion { get; set; }
    }
}
