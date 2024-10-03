using AutoMapper;
using BLL.Respostery;
using DAL.Entities;
using DAL.ViewModel;


namespace BLL.Helper
{
    public class DomainProfile:Profile
    {
        public DomainProfile()
        {
            CreateMap<EmployeRep, EmployeeViewModel>().ReverseMap();
            CreateMap<Department, DepartmentViewModel>().ReverseMap();

            CreateMap<EmployeeViewModel, Employees>().ReverseMap();
            CreateMap<DepartmentViewModel, Department>().ReverseMap();





        }
    }
}
