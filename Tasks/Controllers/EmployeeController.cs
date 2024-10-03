using AutoMapper;
using BLL.UnitOfWork;
using DAL.DataBase;
using DAL.Entities;
using DAL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tasks.Controllers
{
    public class EmployeeController : Controller
    {
        public IUnitOfWork UnitOfWork;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext context)
        {
                this.UnitOfWork = unitOfWork;
            this.mapper = mapper;
            this.context = context;
        }
      
        public async Task<IActionResult> Index()
        {
            var employees = await UnitOfWork.Employee.GetAllWithDetailsAsync();
            List<EmployeeViewModel> result = new List<EmployeeViewModel>();
            foreach (var item in employees)
            {
                EmployeeViewModel employee = new EmployeeViewModel();
                employee.Id = item.Id;
                if (item.Departments!=null)
                {
                    employee.DepartmentId = item.Departments.Id;
                    employee.DepartmentName = item.Departments.Name;


                }
                employee.Salary = item.Salary;
                if (item.Manager!=null)
                {
                    employee.ManagerId = item.Manager.ManagerId;
                    employee.ManagerName = item.Manager.Name;
                }
               
                employee.Name=item.Name;
                result.Add(employee);
            }
           
            return View(result);
        }

        public async Task< IActionResult> Create()
        {
            EmployeeViewModel employee = new EmployeeViewModel();
            employee.Departments =await UnitOfWork.Departments.GetAll();
            
            employee.Managers = await UnitOfWork.Employee.GetAll();

            
            

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                var employees = mapper.Map<Employees>(employeeViewModel);

                UnitOfWork.Employee.AddAsync(employees);
                await UnitOfWork.complete();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeViewModel);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await UnitOfWork.Employee.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            var employeeViewModel = mapper.Map<EmployeeViewModel>(employee);
            employeeViewModel.Departments =await UnitOfWork.Departments.GetAll();

            employeeViewModel.Managers = await UnitOfWork.Employee.GetAll();

            
            return View(employeeViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeViewModel employeeViewModel)
        {
            if (id != employeeViewModel.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var employee = await UnitOfWork.Employee.GetByIdAsync(id);
                if (employee == null)
                    return NotFound();

                mapper.Map(employeeViewModel, employee); // تحديث البيانات في الكيان

                await UnitOfWork.complete();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeViewModel);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await UnitOfWork.Employee.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            var employeeViewModel = mapper.Map<EmployeeViewModel>(employee);
            return View(employeeViewModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var data = context.Employees_TBL.Find(id);
                context.Employees_TBL.Remove(data);
                //await _unitOfWork.complete();
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));

            }
          
            
         
        }



    }
}

