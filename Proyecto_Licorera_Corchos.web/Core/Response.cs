using System;
using System.Collections.Generic;

namespace Proyecto_Licorera_Corchos.web.Core
{
    /// <summary>
    /// Clase genérica para manejar respuestas estándar en la aplicación.
    /// </summary>
    /// <typeparam name="T">Tipo del objeto resultado.</typeparam>
    public class Response<T>
    {
        /// <summary>
        /// Indica si la operación fue exitosa.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Mensaje descriptivo de la operación.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Lista de errores ocurridos durante la operación (si los hay).
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// Resultado genérico de la operación.
        /// </summary>
        public T? Result { get; set; }

        /// <summary>
        /// Propiedad auxiliar para determinar si existen errores.
        /// </summary>
        public bool HasErrors => Errors != null && Errors.Count > 0;
    }
}