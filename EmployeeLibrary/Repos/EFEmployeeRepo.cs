using EmployeeLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLibrary.Repos
{
    public class EFEmployeeRepo : IEmployeeRepo
    {
        WellsFargoDBContext ctx = new WellsFargoDBContext();
        public async Task DeleteEmployeeAsync(int eid)
        {
            Employee emp2del = await GetEmployeeAsync(eid);
            ctx.Employees.Remove(emp2del);
            await ctx.SaveChangesAsync();
        }
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            List<Employee> employees = await ctx.Employees.ToListAsync();
            return employees;
        }
        public async Task<Employee> GetEmployeeAsync(int eid)
        {
            try
            {
                Employee employee = await (from emp in ctx.Employees where emp.EmpId == eid select emp).FirstAsync();
                return employee;
            }
            catch (Exception)
            {
                throw new Exception("No such emp id");
            }
        }
        public async Task InsertEmployeeAsync(Employee employee)
        {
            try
            {
                await ctx.Employees.AddAsync(employee);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateEmployeeAsync(int eid, Employee employee)
        {
            Employee emp2edit = await GetEmployeeAsync(eid);
            emp2edit.EmpName = employee.EmpName;
            emp2edit.Salary = employee.Salary;
            await ctx.SaveChangesAsync();
        }
    }
}
