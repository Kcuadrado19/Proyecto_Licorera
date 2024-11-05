using Proyecto_Licorera_Corchos.web.Core;

namespace Proyecto_Licorera_Corchos.web.Helpers
{
    public static class ResponseHelper<T>
    {
        public static Response<T> MakeResponseSuccess(T model, string message= "Tarea realizada con éxito")
        {
            return new Response<T>
            {
                IsSuccess = true,
                Message = message,
                Result = model,
            };
        }

        public static Response<T> MakeResposeFail(Exception ex, string message = "Error al generar la solicitud")
        {
            return new Response<T>
            {
                Errors = new List<string>
                {
                    ex.Message
                },

                IsSuccess = false,
                Message = message,
            };

        }

            public static Response<T> MakeResposeFail(string message)
            {
                return new Response<T>
                {
                    Errors = new List<string>
                {
                        message
                },

                    IsSuccess = false,
                    Message = message,
                };
            }
    }
}
