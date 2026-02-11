using System.Runtime.CompilerServices;

namespace NotificationService.Domain.Common
{
    public static class Guard
    {

        public static void AgainstNullOrWhiteSpace(string? argument, [CallerArgumentExpression("argument")] string? paramName = null)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentException("String parameter cannot be null or whitespace.", paramName);
            }
        }

        public static void AgainstStringLength(string argument, int exactLength, [CallerArgumentExpression("argument")] string? paramName = null)
        {
            AgainstNullOrWhiteSpace(argument, paramName);
            if (argument.Length != exactLength)
            {
                throw new ArgumentException($"Parameter must be exactly {exactLength} characters long.", paramName);
            }
        }

        public static void AgainstOutOfRange(int argument, int min, int max, [CallerArgumentExpression("argument")] string? paramName = null)
        {
            if (argument < min || argument > max)
            {
                throw new ArgumentOutOfRangeException(paramName, $"Parameter is out of valid range ({min}-{max}).");
            }
        }

        public static void AgainstInvalidEmail(string argument, [CallerArgumentExpression("argument")] string? paramName = null)
        {
            AgainstNullOrWhiteSpace(argument, paramName);
            try
            {
                var addr = new System.Net.Mail.MailAddress(argument);
                if (addr.Address != argument)
                {
                    throw new ArgumentException("Invalid email address format.", paramName);
                }
            }
            catch
            {
                throw new ArgumentException("Invalid email address format.", paramName);
            }
        }
        
        public static void AgainstInvalidPhoneNumber(string argument, [CallerArgumentExpression("argument")] string? paramName = null)
        {
            AgainstNullOrWhiteSpace(argument, paramName);
            var phoneNumberPattern = @"^\+?[1-9]\d{1,14}$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(argument, phoneNumberPattern))
            {
                throw new ArgumentException("Invalid phone number format.", paramName);
            }
        }
    }
}