using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
public class Orders
    {
        [Key] // Indica que es la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Especifica que es auto-incremental (IDENTITY)
        public int Id_Order { get; set; }

        [MaxLength(50, ErrorMessage = "El campo '{0}' debe tener máximo '{1}' caracteres.")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]

        //[Display(Nombre = "Pedido")]
        public string Orders_Name { get; set; }

        public int? Contact { get; set; } // Campo opcional (nullable)

        [Column(TypeName = "money")] // Especifica el tipo de dato 'MONEY'
        public decimal? Total_Order { get; set; } // Campo opcional (nullable)

        [DataType(DataType.Date)] // Valida que sea de tipo fecha
        public DateTime? Orders_Date { get; set; } // Campo opcional (nullable)

        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int Id_Product { get; set; } // Relación con Producto

        [ForeignKey("Id_Product")]
        public virtual Products Products { get; set; } // Propiedad de navegación para Producto





        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int Id_Client { get; set; } // Relación con Cliente

        [ForeignKey("Id_Client")]
        public virtual Clients Clients { get; set; } // Propiedad de navegación para Cliente

        // Relación con la tabla Accounting
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int Id_Accounting { get; set; } // Clave foránea a Accounting

        [ForeignKey("Id_Accounting")]
        public virtual Accounting Accounting { get; set; } // Propiedad de navegación para Accounting

        [ForeignKey("Id_Sales")]
        public virtual Sales Sales { get; set; } // Propiedad de navegación para Cliente

    }
}
