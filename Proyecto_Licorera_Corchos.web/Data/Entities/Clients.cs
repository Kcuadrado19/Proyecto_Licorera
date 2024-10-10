using System.ComponentModel.DataAnnotations;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class Clients
    {
        [Key] // Indica que es clave primaria
        public int Id_Client { get; set; }

        [MaxLength(50, ErrorMessage = "El campo '{0}' debe tener máximo '{1}' carácteres")] // Valida que el campo no supere 50 caracteres
        [Required(ErrorMessage = "El campo '{0}' es requerido.")] // Indica que es un campo requerido
        public string Client_Name { get; set; }

        [MaxLength(50, ErrorMessage = "El campo '{0}' debe tener máximo '{1}' carácteres")] // Valida que el campo no supere 50 caracteres
        [Required(ErrorMessage = "El campo '{0}' es requerido.")] // Indica que es un campo requerido
        public string Phone { get; set; }

    }
}
