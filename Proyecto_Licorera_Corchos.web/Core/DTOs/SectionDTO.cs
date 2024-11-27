using Proyecto_Licorera_Corchos.web.Data.Entities;

namespace Proyecto_Licorera_Corchos.web.DTOs
{
    public class SectionDto : Section
    {
        public bool Selected { get; set; } = false;
    }
}
