namespace CaseTecnico.Domain.ResultObject
{
    public class Result<T>
    {
        public bool Success { get; set; }

        public bool Failure => !Success;

        public Error? Error { get; set; }

        public T? Data { get; set; }

        public Result() { }
        
        public Result(bool success, Error error)
        {
            Error = error;
            Success = success;
        }

        public static Result<T> FailureResult(Error error) => new(false, error);

        public static implicit operator Result<T>(T value)
        {
            return new Result<T>
            {
                Success = true,
                Data = value,
            };
        }
    }
}
