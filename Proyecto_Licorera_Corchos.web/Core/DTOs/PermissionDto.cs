namespace Proyecto_Licorera_Corchos.web.DTOs
{
    public class PermissionDTO
    {
        public int Id { get; set; } // Identificador del permiso

        public string Name { get; set; } // Nombre del permiso

        public string Description { get; set; } // Descripción del permiso

        public string Module { get; set; } // Módulo al que pertenece el permiso

        public bool Selected { get; set; } // Indica si el permiso está seleccionado
    }
}