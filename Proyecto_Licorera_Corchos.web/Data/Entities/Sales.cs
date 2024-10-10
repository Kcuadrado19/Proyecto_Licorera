using System.ComponentModel.DataAnnotations;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class Sales
    {
        [Key] // Indica que es clave primaria
        public int Id_Sales { get; set; }

        [Required(ErrorMessage = "El campo '{0}' es requerido.")] // Indica que es un campo requerido
        public DateTime Sale_Date { get; set; } // Se utiliza DateTime para representar fechas

        [Required(ErrorMessage = "El campo '{0}' es requerido.")] // Indica que es un campo requerido
        public float Total_Sales { get; set; } // Se utiliza float para representar el total de ventas

        [Required(ErrorMessage = "El campo '{0}' es requerido.")] // Indica que es un campo requerido
        public int Id_Orders { get; set; } // Relacionado con el pedido
    }
}
