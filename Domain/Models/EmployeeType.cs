namespace Domain.Models;

public class EmployeeType
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int DepartmentId { get; set; }
    public string Position { get; set; }
    public DateTime HireDate { get; set; }
    public double AverageAmount { get; set; }
}
