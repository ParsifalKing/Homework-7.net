using Dapper;
using Domain.Models;
using Infrastructure.DataContext;

namespace Infrastructure.Services;

public class EmployeeService
{
    private readonly DapperContext _context;
    public EmployeeService()
    {
        _context = new DapperContext();
    }

    public void AddEmployee(Employee employee)
    {
        var sql = @"insert into Employees(FirstName,LastName,DepartmentId,Position,HireDate) values(@FirstName,@LastName,@DepartmentId,@Position,@HireDate)";
        _context.Connection().Execute(sql, employee);
    }

    public List<Employee> GetEmployees()
    {
        var sql = @"select * from Employees";
        return _context.Connection().Query<Employee>(sql).ToList();
    }

    public void UpdateEmployee(Employee employee)
    {
        var sql = @"update Employees set FirstName=@FirstName,LastName=@LastName,DepartmentId=@DepartmentId,Position=@Position,HireDate=@HireDate where EmployeeId=@EmployeeId";
        _context.Connection().Execute(sql, employee);
    }

    public void DeleteEmployee(int id)
    {
        var sql = @"delete from Employees where EmployeeId=@EmployeeId";
        _context.Connection().Execute(sql, new { Id = id });
    }

    // 3
    public List<ListOfSome<Employee, Salary>> GetEmployeesWithSalary()
    {
        var sql1 = @"select EmployeeId from Employees;";
        var employees_Id = _context.Connection().Query<int>(sql1).ToList();
        var sql2 = @"select * from Employees where EmployeeId=@EmployeeId;
        select * from Salaries where EmployeeId=@EmployeeId 
        order by PayrollDate desc limit 1;";

        var employeesSalaries = new List<ListOfSome<Employee, Salary>>();
        foreach (var item in employees_Id)
        {
            using (var multiple = _context.Connection().QueryMultiple(sql2, new { EmployeeId = item }))
            {
                var employeeSalary = new ListOfSome<Employee, Salary>();
                employeeSalary.Any = multiple.ReadFirst<Employee>();
                employeeSalary.listOfSome = multiple.Read<Salary>().ToList();
                employeesSalaries.Add(employeeSalary);
            }
        }
        return employeesSalaries;
    }

    // 4
    public List<ListOfSome<Employee, Salary>> GetEmployeesWihtManySalary()
    {
        var employeesWithManySalary = new List<ListOfSome<Employee, Salary>>();
        var employeesSalary = GetEmployeesWithSalary();

        double averageAmount = 0;
        int cnt = 0;
        foreach (var employee in employeesSalary)
        {
            foreach (var employee2 in employee.listOfSome)
            {
                averageAmount = averageAmount + employee2.Amount;
                cnt++;
            }
        }
        averageAmount = averageAmount / cnt;

        foreach (var employee in employeesSalary)
        {
            foreach (var employee2 in employee.listOfSome)
            {
                if (employee2.Amount >= averageAmount)
                {
                    employeesWithManySalary.Add(employee);
                }
            }
        }
        return employeesWithManySalary;

    }

}
