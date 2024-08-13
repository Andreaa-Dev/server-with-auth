namespace Backend.src.Shared
{
    public class CustomException : Exception
    {
        public int StatusCode { get; }
        public CustomException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public static CustomException NotFound(string message = "Item not found")
        {
            return new CustomException(404, message);
        }

        public static CustomException BadRequest(string message = "Bad request. Please check your request")
        {
            return new CustomException(400, message);
        }

        public static CustomException UnAuthorized(string message = "Cant not log in")
        {
            return new CustomException(401, message);
        }

        public static CustomException InternalError(string message = "Internal error")
        {
            return new CustomException(500, message);
        }
        public static CustomException InvalidData(string message = "Invalid data")
        {
            return new CustomException(412, message);
        }

    }
}