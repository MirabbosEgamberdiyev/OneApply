

namespace BusinessLogicLayer.Extended;

public class CustomException(string message) : Exception
{
    public readonly string ErrorMessage = message;
}
