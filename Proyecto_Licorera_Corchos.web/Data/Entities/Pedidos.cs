using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
public class Pedido
    {
        [Key] // Indica que es la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Especifica que es auto-incremental (IDENTITY)
        public int Id_Pedido { get; set; }

        [MaxLength(50, ErrorMessage = "El campo '{0}' debe tener máximo '{1}' caracteres.")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Nombre { get; set; }

        public int? Contacto { get; set; } // Campo opcional (nullable)

        [Column(TypeName = "money")] // Especifica el tipo de dato 'MONEY'
        public decimal? Precio_Pedido { get; set; } // Campo opcional (nullable)

        [DataType(DataType.Date)] // Valida que sea de tipo fecha
        public DateTime? Fecha_Pedido { get; set; } // Campo opcional (nullable)

        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int Id_Producto { get; set; } // Relación con Producto

        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int Id_Cliente { get; set; } // Relación con Cliente

        // Relaciones con otras tablas
        [ForeignKey("Id_Producto")]
        public virtual Producto Producto { get; set; } // Relación con la tabla Producto

        [ForeignKey("Id_Cliente")]
        public virtual Cliente Cliente { get; set; } // Relación con la tabla Cliente
    }




}
}
