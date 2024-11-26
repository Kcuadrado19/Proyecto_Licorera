namespace Proyecto_Licorera_Corchos.web.DTOs
{
    public class SectionDTO
    {
        public int Id { get; set; } // Identificador de la sección

        public string Name { get; set; } // Nombre de la sección

        public string? Description { get; set; } // Descripción de la sección

        public bool IsHidden { get; set; } // Indica si la sección está oculta

        public bool Selected { get; set; } // Indica si la sección está seleccionada
    }
}