using System.Net;

namespace CosmosCRUD.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public HttpStatusCode _httpStatus;

        public UserAlreadyExistsException(string message) : base(message)
        {
            _httpStatus = HttpStatusCode.Conflict;
        }
    }
}
