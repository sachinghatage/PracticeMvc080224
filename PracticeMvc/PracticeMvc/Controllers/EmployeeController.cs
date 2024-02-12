using Microsoft.AspNetCore.Mvc;
using PracticeMvc.Data;
using PracticeMvc.Models;

namespace PracticeMvc.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index(string sortOrder)
        {
            List<Employee> sortedEmployees = SortEmployees(sortOrder);
            return View(sortedEmployees);
        }

        [HttpGet]    //to avoid form submission twice better to use get and post for same method
        public IActionResult CreateEmployee()
        {
            return View();
        }
        /*
                [HttpPost]
                public IActionResult CreateEmployee(Employee employee)
                {
                    if (ModelState.IsValid)   // by using this we ensure that form is submitted only once on post request,if this condition is not given values in db are saved twice
                    {
                        dbContext.Employees.Add(employee);
                        dbContext.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    return View(employee);
                }*/

        /*[HttpPost]
        public IActionResult CreateEmployee(Employee employee)
        {
                dbContext.Employees.Add(employee);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
  
        }*/


        [HttpPost]
        public IActionResult CreateEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                // Save the employee to the database
                dbContext.Employees.Add(employee);
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            // If ModelState is not valid, re-populate dropdowns and return the view with errors
            return View(employee);
        }



        [HttpGet]
        public IActionResult EditEmployee(int id) 
        {
            Employee? employee = dbContext.Employees.Find(id);
             return View(employee);
        
        }

        [HttpPost]
        public IActionResult EditEmployee(Employee employee)
        {
            dbContext.Employees.Update(employee);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteEmployee(int id)
        {
            Employee? employee = dbContext.Employees.Find(id);
            return View(employee);
        }

        [HttpPost]
        public IActionResult DeleteEmployee(Employee employee)
        {
            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }


        private List<Employee> SortEmployees(string sortOrder)
        {
            IQueryable<Employee> employeesQuery = dbContext.Employees.AsQueryable();

            switch(sortOrder)
            {
                case "id_desc":
                    employeesQuery = employeesQuery.OrderByDescending(e=>e.Id);
                    break;

                case "name":
                    employeesQuery = employeesQuery.OrderBy(e=>e.Name);
                    break;


                case "name_desc":
                    employeesQuery = employeesQuery.OrderByDescending(e=>e.Name);
                    break;


                default:
                    employeesQuery = employeesQuery.OrderBy(e=>e.Id);
                    break;
            }

            return employeesQuery.ToList();
        }

    }
}
