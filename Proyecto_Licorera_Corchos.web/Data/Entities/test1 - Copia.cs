using System.ComponentModel.DataAnnotations;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class test1
    {
        [Key] //indica que es clave primaria
        public int Id { get; set; }

        //para crearlos se necesitan los nuget

        [MaxLength(32, ErrorMessage = "El campo '{0}' debe tener máximo '{1}' carácteres")]  //se asegura que se cree un varchar(n)que permita 32 carateres
        [Required(ErrorMessage = "El campo '{0}' es requerido.")] //al igual que es alterior son válidaciones
        public string FirstName { get; set; }

        [MaxLength(32, ErrorMessage = "El campo '{0}' debe tener máximo '{1}' carácteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string LastName { get; set; }

    }
}
