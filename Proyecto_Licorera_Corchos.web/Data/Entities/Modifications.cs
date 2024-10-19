using System.ComponentModel.DataAnnotations;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class Modifications
    {
        [Key] // Clave primaria
        public int Id_Modification { get; set; }

        [MaxLength(100, ErrorMessage = "El campo '{0}' debe tener máximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Modification_Name { get; set; }
    }
}