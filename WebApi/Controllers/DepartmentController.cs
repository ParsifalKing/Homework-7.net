using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentController : ControllerBase
{

    private readonly DepartmentService _departmentService;

    public DepartmentController()
    {
        _departmentService = new DepartmentService();
    }

    [HttpPost("add-department")]
    public void AddDepartment(Department department)
    {
        _departmentService.AddDepartment(department);
    }

    [HttpGet("get-departments")]
    public List<Department> GetDepartmens()
    {
        return _departmentService.GetDepartmens();
    }

    [HttpPut("update-department")]
    public void UpdateDepartment(Department department)
    {
        _departmentService.UpdateDepartment(department);
    }

    [HttpDelete("delete-department{id}")]
    public void DeleteDepartment(int id)
    {
        _departmentService.DeleteDepartment(id);
    }

    [HttpGet("get-department-employees-by-id{departmentId}")]
    public ListOfSome<Department, Employee> GetDepartmentEmployeesById(int departmentId)
    {
        return _departmentService.GetDepartmentEmployeesById(departmentId);
    }

    [HttpGet("get-departments-employees")]
    public List<ListOfSome<Department, EmployeeType>> GetDepartmentsEmployees()
    {
        return _departmentService.GetDepartmentsEmployees();
    }



}
