using EmployeeLibrary.Models;
using EmployeeLibrary.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace EmployeeMvcApp.Controllers
{
    public class EmployeeController : Controller {
        //IEmployeeRepo empRepo;
        /*public EmployeeController(IEmployeeRepo employeeRepo) {
            //empRepo = new EFEmployeeRepo();
            empRepo = employeeRepo;
        }*/
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5256/api/Employee/") };
        public async Task<ActionResult> Index() {
            //List<Employee> employees = await empRepo.GetAllEmployeesAsync();
            List<Employee> employees = await client.GetFromJsonAsync<List<Employee>>("");
            return View(employees);
        }
        public async Task<ActionResult> Details(int id) {
            //Employee employee = await empRepo.GetEmployeeAsync(id);
            Employee employee = await client.GetFromJsonAsync<Employee>("" + id);
            return View(employee);
        }
        public ActionResult Create() {
            Employee emp = new Employee();
            return View(emp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Employee employee) {
            try {
                //await empRepo.InsertEmployeeAsync(employee);
                await client.PostAsJsonAsync<Employee>("", employee);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
        public async Task<ActionResult> Edit(int id) {
            //Employee employee = await empRepo.GetEmployeeAsync(id);
            Employee employee = await client.GetFromJsonAsync<Employee>("" + id);
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Employee employee) {
            try {
                //await empRepo.UpdateEmployeeAsync(id, employee);
                await client.PutAsJsonAsync<Employee>("" + id, employee);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
        public async Task<ActionResult> Delete(int id) {
            //Employee employee = await empRepo.GetEmployeeAsync(id);
            Employee employee = await client.GetFromJsonAsync<Employee>("" + id);
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection) {
            try {
                //await empRepo.DeleteEmployeeAsync(id);
                await client.DeleteAsync("" + id);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
    }
}
