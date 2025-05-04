namespace LlamaStoreService.Models.Auditos
{
    public class Auditoria
    {
        public int idauditoria { get; set; }
        public string tabla_afectada { get; set; }
        public string id_registro { get; set; }
        public string accion { get; set; }
        public string valores_anteriores { get; set; }
        public string valores_nuevos { get; set; }
        public string usuario { get; set; }
        public DateTime fecha { get; set; }
    }
}
