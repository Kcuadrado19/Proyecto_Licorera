namespace Proyecto_Licorera_Corchos.web.Core
{
    public class Response<T>
    {

        public bool IsSuccess {  get; set; }
        public String Message { get; set; }

        public List <string> Errors { get; set; }

        public T Result { get; set; }

    }
}
