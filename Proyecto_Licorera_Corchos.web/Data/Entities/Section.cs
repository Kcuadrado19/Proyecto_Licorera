﻿using System.ComponentModel.DataAnnotations;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class Section
    {
        [Key] 
        public int Id { get; set; }

        [Display(Name = " Sección")] 
        [Required(ErrorMessage = "El campo '{0}' es requerido.")] // Indica que es un campo requerido
        public string Name { get; set; }

        [Display(Name = " Description")]
        public string? Description { get; set; }

        [Display(Name = " Está Oculta?")]
        public bool IsHidden { get; set; }

       
    }
}