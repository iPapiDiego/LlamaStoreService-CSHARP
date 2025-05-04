namespace LlamaStoreService.Models.Tickets
{
    public class Cabecera
    {
        public String num_bol { get; set; }
        public DateTime fch_bol { get; set; }
        public int cod_cliente { get; set; }
        public decimal subtotal { get; set; }
        public decimal igv { get; set; }
        public decimal total { get; set; }
        public string metodo_pago { get; set; }
        public int estado_pago { get; set; }
        public string direccion_envio { get; set; }
    }
}
