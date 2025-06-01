namespace SistemaEstoque.API.DTOs
{
    public class Response<T>
    {
        public bool success { get; set; }
        public string? errorMessage { get; set; }
        public string? message { get; set; }
        public T Data { get; set; }

        public static Response<T> Ok(T data)
        {
            return new Response<T> { success = true, Data = data };
        }

        public static Response<T> Ok()
        {
            return new Response<T> { success = true, message = "Sucesso na requisição"};
        }
        public static Response<T> Failed(string errorMesssge)
        {
            return new Response<T> { success = false, errorMessage = errorMesssge };
        }
    }
}
