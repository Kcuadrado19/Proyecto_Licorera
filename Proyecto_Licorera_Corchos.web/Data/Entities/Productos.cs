using System.ComponentModel.DataAnnotations;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class Productos
    {
        [Key] // Indica que es clave primaria
        public int Id_Producto { get; set; }

        [MaxLength(50, ErrorMessage = "El campo '{0}' debe tener máximo '{1}' carácteres")] // Valida que el campo no supere 50 caracteres
        [Required(ErrorMessage = "El campo '{0}' es requerido.")] // Indica que es un campo requerido
        public string? Categoria { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es requerido.")] // Indica que es un campo requerido
        public float Precio { get; set; } // Se utiliza decimal para el tipo MONEY

        [MaxLength(100, ErrorMessage = "El campo '{0}' debe tener máximo '{1}' carácteres")] // Valida que el campo no supere 100 caracteres
        public string? Descripcion { get; set; }

    }
}
