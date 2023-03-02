using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductTracking.Application.DTOs.ResponseDTOs
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }
        [JsonIgnore]
        public int StatusCode{ get; set; }
        public List<String> Errors{ get; set; }


        public static CustomResponseDto<T> Success(T data,int statusCode)
            => new() { Data=data,StatusCode= statusCode};

        public static CustomResponseDto<T> Success(int statusCode)
            => new() {StatusCode = statusCode };

        public static CustomResponseDto<T> Fail(T data,int statusCode, List<string> errors)
            => new() { StatusCode = statusCode,Data=data,Errors=errors };

        public static CustomResponseDto<T> Fail( T data,int statusCode, string error)
            => new() { StatusCode = statusCode, Data = data, Errors = new() { error} };
    }
}
