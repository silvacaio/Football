namespace Football.Domain.Core.Errors
{
    public struct Error
    {
        public string Msg { get; private set; }

        public static implicit operator Error(string msg)
        {
            return new Error { Msg = msg };
        }

        public static implicit operator string(Error error)
        {
            return error.Msg;
        }
    }
}
