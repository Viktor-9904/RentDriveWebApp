namespace RentDrive.Services.Data.Common
{
    public class ServiceResponse<T>
    {
        public T? Result { get; set; }
        public bool Success { get; set; } = true;
        public string? ErrorMessage { get; set; }

        public static ServiceResponse<T> Ok(T result) => new ServiceResponse<T>() { Result = result, Success = true };
        public static ServiceResponse<T> Fail(string error) => new ServiceResponse<T>() { Success = false, ErrorMessage = error };
    }

}
