namespace Domain.Models;

public class Salary
{
    public int SalaryId { get; set; }
    public int EmployeeId { get; set; }
    public double Amount { get; set; }
    public DateTime PayrollDate { get; set; }
}
