namespace PruebaPeriferiaIT.Application.Common.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Data { get; }
        public List<string> Errors { get; } = new();

        private Result(bool isSuccess, T? data, List<string>? errors)
        {
            IsSuccess = isSuccess;
            Data = data;
            if (errors != null)
                Errors.AddRange(errors);
        }

        public static Result<T> Success(T data) => new(true, data, null);
        public static Result<T> Failure(params string[] errors) => new(false, default, errors.ToList());
    }
}
