using Football.Domain.Core.Errors;

namespace Football.Domain.Core.Results
{
    public class Result<TSuccess>
    {
        internal Result(TSuccess success)
        {
            Success = success;
            IsError = false;       
        }

        internal Result(string error)
        {
            IsError = true;           
            Error = error;
        }

        public bool IsSuccess { get { return Success != null; } }
        public bool IsError { get; set; }      
        public TSuccess Success { get; set; }
        public Error Error { get; set; }

        public static implicit operator Result<TSuccess>(string msg)
            => new Result<TSuccess>(msg);

        public static implicit operator Result<TSuccess>(TSuccess success)
           => new Result<TSuccess>(success);
      
        public static Result<TSuccess> ICantResolver()
        {
            return "404 - Not Found";
        }
    }
}
