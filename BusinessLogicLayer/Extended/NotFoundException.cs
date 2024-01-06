namespace BusinessLogicLayer.Extended;

public class NotFoundException(string message)
   : Exception(message)
{

}
