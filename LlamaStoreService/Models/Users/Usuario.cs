using System.ComponentModel.DataAnnotations;

namespace LlamaStoreService.Models.Users
{
    public class Usuario
    {
        [Display(Name = "Codigo")] public int codigo { get; set; }
        [Display(Name = "Nombre")] public string nombre { get; set; }
        [Display(Name = "Apellido")] public string apellido { get; set; }
        [Display(Name = "Correo")] public string correo { get; set; }
        [Display(Name = "Clave")] public string clave { get; set; }
        [Display(Name = "Fecha")] public DateTime fnacim { get; set; }
        [Display(Name = "Tipo")] public int tipo { get; set; }
        [Display(Name = "Estado")] public int estado { get; set; }
    }
}
