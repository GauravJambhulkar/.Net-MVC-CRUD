using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_CRUD.Data;
using MVC_CRUD.Models;
using MVC_CRUD.Models.Domain;

namespace MVC_CRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDbContext mVCDemoDbContext;

        public EmployeesController(MVCDemoDbContext mVCDemoDbContext)
        {
            this.mVCDemoDbContext = mVCDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mVCDemoDbContext.Employees.ToListAsync();

            return View(employees);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth
            };

            await mVCDemoDbContext.Employees.AddAsync(employee);
            await mVCDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        //Update Employee
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await mVCDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth
                };
                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mVCDemoDbContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.Department = model.Department;
                employee.DateOfBirth = model.DateOfBirth;

                await mVCDemoDbContext.SaveChangesAsync();
                
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }



        //Delete Employee
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mVCDemoDbContext.Employees.FindAsync(model.Id);

            if (employee != null) 
            {
                mVCDemoDbContext.Employees.Remove(employee);
                await mVCDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

    }
}
