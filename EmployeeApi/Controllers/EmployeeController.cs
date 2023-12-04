using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeLibrary.Models;
using EmployeeLibrary.Repos;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase {
        IEmployeeRepo empRepo;
        public EmployeeController(IEmployeeRepo employeeRepo) {
            empRepo = employeeRepo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll() {
            List<Employee> employees = await empRepo.GetAllEmployeesAsync();
            return Ok(employees);
        }
        [HttpGet("{eid}")]
        public async Task<ActionResult> GetOne(int eid) {
            try {
                Employee employee = await empRepo.GetEmployeeAsync(eid);
                return Ok(employee);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Insert(Employee employee) {
            try {
                await empRepo.InsertEmployeeAsync(employee);
                return Created($"api/Employee/{employee.EmpId}", employee);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{eid}")]
        public async Task<ActionResult> Update(int eid, Employee employee) {
            try {
                await empRepo.UpdateEmployeeAsync(eid, employee);
                return Ok(employee);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{eid}")]
        public async Task<ActionResult> Delete(int eid) {
            try {
                await empRepo.DeleteEmployeeAsync(eid);
                return Ok();
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
