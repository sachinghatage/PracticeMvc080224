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

        public IActionResult Index()
        {

            List<Employee> employees = dbContext.Employees.ToList();
            return View(employees);
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

        [HttpPost]
        public IActionResult CreateEmployee(Employee employee)
        {
                dbContext.Employees.Add(employee);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
  
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
    }
}
