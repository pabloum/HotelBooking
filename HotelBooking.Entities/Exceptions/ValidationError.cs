using System;
namespace HotelBooking.Entities.Exceptions
{
	public class ValidationError : Exception
	{
		public IEnumerable<ValidationError> ValidationErrors { get; }

		public ValidationError()
		{
		}

		public ValidationError(string message) : base(message)
		{
		}

		public ValidationError(string message, Exception ex) : base(message, ex)
		{
		}

		public ValidationError(string message, IEnumerable<string> errors) : base(message)
		{
			ValidationErrors = errors.Select(e => new ValidationError(e));
		}
	}
}

