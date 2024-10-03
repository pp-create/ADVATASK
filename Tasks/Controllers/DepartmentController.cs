using AutoMapper;
using BLL.UnitOfWork;
using DAL.DataBase;
using DAL.Entities;
using DAL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Tasks.Controllers
{
 
        public class DepartmentController : Controller
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
        private readonly ApplicationDbContext context;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext context)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            this.context = context;
        }

            // GET: Department/Index
            public async Task<IActionResult> Index()
             {
                var departments = await _unitOfWork.Departments.GetAll();
                var viewModel = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);

                return View(viewModel);
            }

          
            public async Task <IActionResult> Create()
            {
            DepartmentViewModel departmentViewModel = new DepartmentViewModel();
            departmentViewModel.Managers =await _unitOfWork.Employee.GetAll();

            return View(departmentViewModel);
            }

           
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(DepartmentViewModel viewModel)
            {
                if (ModelState.IsValid)
                {
                    var department = _mapper.Map<Department>(viewModel);
                if (department.ManagerId==0)
                {
                    department.ManagerId = null;
                }
                
                    _unitOfWork.Departments.AddAsync(department);
                await _unitOfWork.complete();
                    return RedirectToAction(nameof(Index));
                }
                return View(viewModel);
            }

            
            public async Task<IActionResult> Edit(int id)
            {
                var department = await _unitOfWork.Departments.GetByIdAsync(id);
                if (department == null)
                {
                    return NotFound();
                }

                 var viewModel = _mapper.Map<DepartmentViewModel>(department);
                 viewModel.Managers = await _unitOfWork.Employee.GetAll();
            return View(viewModel);
            }

           
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit( DepartmentViewModel viewModel)
            {
                
                if (ModelState.IsValid)
                {
                    
                    _unitOfWork.Departments.update(viewModel);
                    await _unitOfWork.complete();
                    return RedirectToAction(nameof(Index));
                }
                return View(viewModel);
            }

            
            public async Task<IActionResult> Delete(int id)
            {
                var department = await _unitOfWork.Departments.GetByIdAsync(id);
                if (department == null)
                {
                    return NotFound();
                }
               
               
                var viewModel = _mapper.Map<DepartmentViewModel>(department);
                return View(viewModel);
            }

           
            [HttpPost, ActionName("Delete")]
          
            public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {

           
               var data= context.Department_TBL.Find(id);
        context.Department_TBL.Remove(data);
            context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                return RedirectToAction(nameof(Index));
            }
        } 
        [HttpPost, ActionName("GetManagersByDepartment")]
          
            public async Task<IActionResult> GetManagersByDepartment(int departmentId)
            {
               var data= context.Department_TBL.Where(x=>x.Id==departmentId).Select(m => new { Id=m.Manager.Id, Name=m.Manager.Name }).ToList();
       
                return Json(data);
            }

        }

    
}

