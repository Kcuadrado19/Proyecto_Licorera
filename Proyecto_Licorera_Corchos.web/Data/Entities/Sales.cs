using System.ComponentModel.DataAnnotations;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class Sales
    {
        [Key] // Indica que es clave primaria
        public int Id_Sales { get; set; }

        [Display(Name = "Venta")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string? Name { get; set; }

        [Display(Name = "Fecha venta")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public DateTime Sale_Date { get; set; }

        [Display(Name = "Valor venta")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        [Range(0, float.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public float Sales_Value { get; set; }

        [Display(Name = "Total ventas")]
        public float Total_Sales { get; set; }

     




    }
}
