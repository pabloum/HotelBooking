using HotelBooking.Entities.Exceptions;

namespace HotelBooking.Api.Models
{
    public class ApiError
    {
        public string Type { get; protected set; }
        public string Error { get; protected set; }
        public IEnumerable<object> Details { get; protected set; }

        public ApiError(string message, IEnumerable<string> details)
        {
            Type = "Validation error";
            Error = message;
            Details = details;
        }

        public ApiError(string message) : this(message, new[] { message })
        {
            Type = "Validation error";
            Error = message;
            Details = null;
        }

        public ApiError(ValidationException validatioError)
        {
            Type = "Validation error";
            Error = validatioError.Message;
            Details = validatioError.ValidationErrors;
        }
    }
}

