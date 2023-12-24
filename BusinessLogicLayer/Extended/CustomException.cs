

namespace BusinessLogicLayer.Extended;

public class CustomException(string message) : Exception
{
    private readonly string ErrorMessage = message;
}
