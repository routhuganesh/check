using AutoMapper;
using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI
{
    public class AppMapperProfile:Profile
    {
        public AppMapperProfile()
        {
            CreateMap<EmployeeDto, Employee>();
            CreateMap<DepartmentDto, Department>();
        }
    }
}
