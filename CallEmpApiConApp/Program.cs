using EmployeeLibrary.Models;
using System.Net.Http.Json;
namespace CallEmpApiConApp {
    internal class Program {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5256/api/Employee/") };
        static async Task Main(string[] args) {
            Console.Write("Press ENTER when ready...");
            Console.ReadLine();
            await ShowAllEmps();
            //await ShowOneEmp();
            //await InsertEmp();
            //await UpdateEmp();
            await DeleteEmp();
            await ShowAllEmps();
        }
        private static async Task DeleteEmp() {
            Console.Write("Enter an emp id to delete: ");
            int eid = Convert.ToInt32(Console.ReadLine());
            try {
                Employee employee = await client.GetFromJsonAsync<Employee>("" + eid);
                Console.WriteLine($"Emp Id: {employee.EmpId}, Name: {employee.EmpName}, Salary: {employee.Salary}");
                Console.Write("Are you sure to delete? ");
                char choice = Convert.ToChar(Console.ReadLine());
                if (choice == 'y') {
                    await client.DeleteAsync("" + eid);
                    Console.WriteLine("Employee deleted");
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
        private static async Task UpdateEmp() {
            Console.Write("Enter an emp id to update: ");
            int eid = Convert.ToInt32(Console.ReadLine());
            try {
                Employee employee = await client.GetFromJsonAsync<Employee>("" + eid);
                Console.WriteLine($"Emp Id: {employee.EmpId}, Name: {employee.EmpName}, Salary: {employee.Salary}");
                Console.Write("Enter the new name: ");
                employee.EmpName = Console.ReadLine();
                Console.Write("Enter the new salary: ");
                employee.Salary = Convert.ToDecimal(Console.ReadLine());
                await client.PutAsJsonAsync<Employee>("" + eid, employee);
                Console.WriteLine("Employee details updated");
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
        private static async Task InsertEmp() {
            Employee employee = new Employee();
            Console.Write("Enter an emp id: ");
            employee.EmpId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter name: ");
            employee.EmpName = Console.ReadLine();
            Console.Write("Enter salary: ");
            employee.Salary = Convert.ToDecimal(Console.ReadLine());
            try {
                await client.PostAsJsonAsync<Employee>("", employee);
                Console.WriteLine("New employee added");
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
        private static async Task ShowOneEmp() {
            Console.Write("Enter an emp id: ");
            int eid = Convert.ToInt32(Console.ReadLine());
            try {
                Employee employee = await client.GetFromJsonAsync<Employee>("" + eid);
                Console.WriteLine($"Emp Id: {employee.EmpId}, Name: {employee.EmpName}, Salary: {employee.Salary}");
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
        private static async Task ShowAllEmps() {
            List<Employee> employees = await client.GetFromJsonAsync<List<Employee>>("");
            foreach (Employee employee in employees) {
                Console.WriteLine($"Emp Id: {employee.EmpId}, Name: {employee.EmpName}, Salary: {employee.Salary}");
            }
        }
    }
}
