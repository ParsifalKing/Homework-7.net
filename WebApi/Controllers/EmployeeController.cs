using Infrastructure.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class EmployeeController
{
    private readonly EmployeeService _employeeService;
    public EmployeeController()
    {
        _employeeService = new EmployeeService();
    }

    [HttpPost("add-employee")]
    public void AddEmployee(Employee employee)
    {
        _employeeService.AddEmployee(employee);
    }

    [HttpGet("get-employees")]
    public List<Employee> GetEmployees()
    {
        return _employeeService.GetEmployees();
    }

    [HttpPut("update-employee")]
    public void UpdateEmployee(Employee employee)
    {
        _employeeService.UpdateEmployee(employee);
    }

    [HttpDelete("delete-employee")]
    public void DeleteEmployee(int id)
    {
        _employeeService.DeleteEmployee(id);
    }

    [HttpGet("get-employees-with-salary")]
    public List<ListOfSome<Employee, Salary>> GetEmployeesWithSalary()
    {
        return _employeeService.GetEmployeesWithSalary();
    }

    [HttpGet("get-employee-with-many-salary")]
    public List<ListOfSome<Employee, Salary>> GetEmployeesWihtManySalary()
    {
        return _employeeService.GetEmployeesWihtManySalary();

    }


}
