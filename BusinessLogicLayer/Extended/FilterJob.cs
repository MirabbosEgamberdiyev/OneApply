

namespace BusinessLogicLayer.Extended;

public record FilterJob( string title, int employmentType, string location)
{
    public string Title = title;
    public int EmploymentType = employmentType;
    public string Location = location;
}
