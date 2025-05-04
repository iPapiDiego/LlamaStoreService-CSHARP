namespace LlamaStoreService.Models.Products
{
    public class CelularCrear
    {
        public string idcel { get; set; }
        public int idmarca { get; set; }
        public string modelo { get; set; }
        public decimal precio { get; set; }
        public decimal precio_oferta { get; set; }
        public int idgama { get; set; }
        public int stock { get; set; }
        public int stock_minimo { get; set; }
        public int idsistema { get; set; }
        public string especificaciones { get; set; }
        public int idestado { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
