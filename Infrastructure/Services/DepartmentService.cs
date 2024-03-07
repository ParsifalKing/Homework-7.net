using Dapper;
using Domain.Models;
using Infrastructure.DataContext;

namespace Infrastructure.Services;

public class DepartmentService
{
    private readonly DapperContext _context;
    public DepartmentService()
    {
        _context = new DapperContext();
    }

    public void AddDepartment(Department department)
    {
        var sql = @"insert into Departments(DepartmentName) values(@DepartmentName)";
        _context.Connection().Execute(sql, department);
    }

    public List<Department> GetDepartmens()
    {
        var sql = @"select * from Departments";
        return _context.Connection().Query<Department>(sql).ToList();
    }

    public void UpdateDepartment(Department department)
    {
        var sql = @"update Departments set DepartmentName=@DepartmentName where DepartmentId=@DepartmentId";
        _context.Connection().Execute(sql, department);
    }

    public void DeleteDepartment(int id)
    {
        var sql = @"delete from Departments where DepartmentId=@DepartmentId";
        _context.Connection().Execute(sql, new { Id = id });
    }

    // 1
    public ListOfSome<Department, Employee> GetDepartmentEmployeesById(int departmentId)
    {
        var sql = @"select * from Departments where DepartmentId=@DepartmentId;
        select * from Employees where DepartmentId = @DepartmentId; 
        ";

        using (var multiple = _context.Connection().QueryMultiple(sql, new { DepartmentId = departmentId }))
        {
            var departmentEmployee = new ListOfSome<Department, Employee>();
            departmentEmployee.Any = multiple.ReadFirst<Department>();
            departmentEmployee.listOfSome = multiple.Read<Employee>().ToList();
            return departmentEmployee;
        }

    }


    // 2
    public List<ListOfSome<Department, EmployeeType>> GetDepartmentsEmployees()
    {
        var sql1 = @"select DepartmentId from Departments;";
        var departments_Id = _context.Connection().Query<int>(sql1).ToList();
        var sql2 = @"select * from Departments where DepartmentId=@DepartmentId;
        select * from Employees where DepartmentId = @DepartmentId; 
        ";

        var departmentsEmployees = new List<ListOfSome<Department, EmployeeType>>();
        foreach (var item in departments_Id)
        {
            using (var multiple = _context.Connection().QueryMultiple(sql2, new { DepartmentId = item }))
            {
                var departmentEmployee = new ListOfSome<Department, EmployeeType>();
                departmentEmployee.Any = multiple.ReadFirst<Department>();
                departmentEmployee.listOfSome = multiple.Read<EmployeeType>().ToList();
                foreach (var employee in departmentEmployee.listOfSome)
                {
                    var sql3 = @"select Round(Avg(Amount),2) from Salaries
                    where EmployeeId = @EmployeeId";
                    var averageAmount = _context.Connection().QueryFirst<double>(sql3, new { EmployeeId = employee.EmployeeId });
                    employee.AverageAmount = averageAmount;
                }
                departmentsEmployees.Add(departmentEmployee);
            }
        }
        return departmentsEmployees;

    }


}
