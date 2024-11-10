namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class User
    {
        public int Id { get; set; } // Clave primaria
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}

