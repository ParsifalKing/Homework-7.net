using Dapper;
using Domain.Models;
using Infrastructure.DataContext;

namespace Infrastructure.Services;

public class SalaryService
{
    private readonly DapperContext _context;
    public SalaryService()
    {
        _context = new DapperContext();
    }

    public void AddSalary(Salary salary)
    {
        var sql = @"insert into Salaries(EmployeeId,Amount,PayrollDate) values(@EmployeeId,@Amount,@PayrollDate)";
        _context.Connection().Execute(sql, salary);
    }

    public List<Salary> GetSalaries()
    {
        var sql = @"select * from Salaries";
        return _context.Connection().Query<Salary>(sql).ToList();
    }

    public void UpdateSalary(Salary salary)
    {
        var sql = @"update Salaries set EmployeeId=@EmployeeId,Amount=@Amount,PayrollDate=@PayrollDate where SalaryId=@SalaryId";
        _context.Connection().Execute(sql, salary);
    }

    public void DeleteSalary(int id)
    {
        var sql = @"delete from Salaries where SalaryId=@SalaryId";
        _context.Connection().Execute(sql, new { Id = id });
    }
}
