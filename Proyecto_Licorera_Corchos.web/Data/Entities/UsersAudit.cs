﻿using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class UsersAudit
    {
        [Key] // Clave primaria
        public int Id_UsersAudit { get; set; }

        [MaxLength(50, ErrorMessage = "El campo '{0}' debe tener máximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public required string UserAudit_Name { get; set; }

        // Relación con la tabla Accounting
        public int Id_A { get; set; }
        
        public virtual required Accounting Accountin { get; set; }
    }

}

