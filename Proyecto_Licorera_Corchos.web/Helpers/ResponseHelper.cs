using Proyecto_Licorera_Corchos.web.Core;
using Proyecto_Licorera_Corchos.web.Data.Entities;

namespace Proyecto_Licorera_Corchos.web.Helpers
{
    public static class ResponseHelper<T>
    {
        // Método para generar una respuesta exitosa con el modelo y un mensaje opcional
        public static Response<T> MakeResponseSuccess(T model, string message = "Operación realizada con éxito")
        {
            return new Response<T>
            {
                IsSuccess = true,
                Message = message,
                Result = model,
            };
        }

        // Método para generar una respuesta fallida con una excepción y un mensaje opcional
        public static Response<T> MakeResponseFail(Exception ex, string message = "Error al procesar la solicitud")
        {
            return new Response<T>
            {
                Errors = new List<string>
                {
                    ex.Message
                },
                IsSuccess = false,
                Message = message,
                Result = default
            };
        }

        // Método para generar una respuesta fallida solo con un mensaje
        public static Response<T> MakeResponseFail(string message)
        {
            return new Response<T>
            {
                IsSuccess = false,
                Message = message,
                Result = default
            };
        }
    }
}
