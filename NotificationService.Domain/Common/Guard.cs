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
        
    }
}