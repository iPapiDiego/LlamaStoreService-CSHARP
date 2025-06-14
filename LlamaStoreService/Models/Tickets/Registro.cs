﻿namespace LlamaStoreService.Models.Tickets
{
    public class Registro
    {
        public string idcel { get; set; }
        public int idmarca { get; set; }
        public string modelo { get; set; }
        public decimal precio { get; set; }
        public int idgama { get; set; }
        public int stock { get; set; }
        public int idsistema { get; set; }
        public int idestado { get; set; }
        public decimal monto { get { return precio * stock; } }
    }
}
