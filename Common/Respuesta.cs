namespace SF_API.Common
{
    public class Respuesta<T>
    {
        public string Titulo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public int Status { get; set; }
        public T? Contenido { get; set; }

        public static Respuesta<T> Exito(string mensaje, T? contenido = default, string? titulo = null, int status = 200)
        {
            return new Respuesta<T>
            {
                Titulo = titulo ?? "Operación exitosa",
                Mensaje = mensaje,
                Status = status,
                Contenido = contenido
            };
        }

        public static Respuesta<T> Error(string mensaje, string? titulo = null, int status = 400)
        {
            return new Respuesta<T>
            {
                Titulo = titulo ?? "Error",
                Mensaje = mensaje,
                Status = status,
                Contenido = default
            };
        }
    }
}
