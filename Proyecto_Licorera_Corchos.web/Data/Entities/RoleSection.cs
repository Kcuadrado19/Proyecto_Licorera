using static System.Collections.Specialized.BitVector32;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class RoleSection
    {
        public int RoleId { get; set; }
        public LicoreraRole Role { get; set; } // Ajustado para usar LicoreraRole

        public int SectionId { get; set; }
        public Section Section { get; set; }
    }
}

