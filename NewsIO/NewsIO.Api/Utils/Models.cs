namespace NewsIO.Api.Utils
{
    public class Models
    {
        public enum ResponseType
        {
            Undefined = 0,
            Successful = 1,
            Wraning = 2,
            Failed = 3
        }

        public class Response
        {
            public ResponseType Status { get; set; }

            public object Value { get; set; }

            public string Message { get; set; }
        }
    }
}
