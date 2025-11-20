using SF_API.Common;

namespace SF_API.Utils
{
    public static class RespuestaFactory
    {
        public static Respuesta<T> Ok<T>(string mensaje, T contenido) => new()
        {
            Titulo = "Éxito",
            Mensaje = mensaje,
            Status = 200,
            Contenido = contenido
        };

        public static Respuesta<T> Fail<T>(string mensaje, int status) => new()
        {
            Titulo = "Error",
            Mensaje = mensaje,
            Status = status,
            Contenido = default
        };

        public static Respuesta<object> Fail(string mensaje, int status) => new()
        {
            Titulo = "Error",
            Mensaje = mensaje,
            Status = status,
            Contenido = null
        };

    }


}
