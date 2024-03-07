
using Domain.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class SalaryController
{
    private readonly SalaryService _salaryService;
    public SalaryController()
    {
        _salaryService = new SalaryService();
    }

    [HttpPost("add-salary")]
    public void AddSalary(Salary salary)
    {
        _salaryService.AddSalary(salary);
    }

    [HttpGet("get-salaries")]
    public List<Salary> GetSalariess()
    {
        return _salaryService.GetSalaries();
    }

    [HttpPut("update-salary")]
    public void UpdateSalary(Salary salary)
    {
        _salaryService.UpdateSalary(salary);
    }

    [HttpDelete("delete-salary")]
    public void DeleteSalary(int id)
    {
        _salaryService.DeleteSalary(id);
    }

}
