using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace SF_API.Common
{
    public enum ErrorType
    {
        None,
        NotFound,
        Validation,
        Conflict,
        ActionError,
        Unexpected,
    }


    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        [JsonIgnore]
        public ErrorType ErrorType { get; set; } = ErrorType.None;
        
        public int Status { get; set; }
        public T? Data { get; set; }

        public static ServiceResult<T> Ok(T data, string message = "")
        {
            return new ServiceResult<T>
            {
                Success = true,
                Message = message,
                Data = data,
                Status = MapStatus(ErrorType.None, true)
            };
        }

        public static ServiceResult<T> Ok(string message = "")
        {
            return new ServiceResult<T> { Success = true, Message = message, Status = 200 };
        }

        public static ServiceResult<T> OkAction(T data, string action, string fieldName)
        {
            return new ServiceResult<T> { Success = true, Message = $"Exito al {action} el {fieldName} ", Data = data, Status = 200 };
        }

        public static ServiceResult<T> OkAction(string action, string fieldName)
        {
            return new ServiceResult<T> { Success = true, Message = $"Exito al {action} el {fieldName} ", Status = 200 };
        }

        public static ServiceResult<T> OkFinded(T data, string fieldName)
        {
            return new ServiceResult<T> { Success = true, Message = $"{fieldName} obtenido con exito", Data = data, Status = 200 };
        }

        public static ServiceResult<T> Fail(string message, ErrorType error)
        {
            return new ServiceResult<T>
            {
                Success = false,
                Message = message,
                ErrorType = error,
                Status = MapStatus(error, false)
            };
        }

        public static ServiceResult<T> FailAction(string action, string fieldName)
        {
            return new ServiceResult<T> { Success = false, Message = $"Ocurrio un error al intentar {action} el {fieldName}", ErrorType = ErrorType.ActionError, Status = MapStatus(ErrorType.ActionError, false) };
        }

        public static ServiceResult<T> FailIdNotFound(string fieldName, int id)
        {
            return new ServiceResult<T> { Success = false, Message = $"La ID: {id} no corresponde a algun {fieldName}", ErrorType = ErrorType.NotFound, Status = MapStatus(ErrorType.NotFound, false) };
        }

        public static ServiceResult<T> FailActionExcepcion(string action, string fieldName)
        {
            return new ServiceResult<T> { Success = false, Message = $"Ocurrio una excepcion al intentar {action} el {fieldName}", ErrorType = ErrorType.ActionError, Status = MapStatus(ErrorType.ActionError, false) };
        }

        private static int MapStatus(ErrorType errorType, bool success)
        {
            if (success) return 200;

            return errorType switch
            {
                ErrorType.NotFound => 404,
                ErrorType.Validation => 400,
                ErrorType.Conflict => 409,
                ErrorType.ActionError => 400,
                ErrorType.Unexpected => 500,
                _ => 400
            };
        }

    }
}
