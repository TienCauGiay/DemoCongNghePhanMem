using AutoMapper;
using Demo.CongNghePhanMem.Core.DTO.Employees;
using Demo.CongNghePhanMem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CongNghePhanMem.Core.Mapper
{
    /// <summary>
    /// class chuyển đổi dữ liệu employee sang các đối tượng phù hợp
    /// </summary>
    /// Created By: BNTIEN (17/06/2023)
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile() 
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
            CreateMap<FilterEmployee, FilterEmployeeDto>();
            CreateMap<Employee, EmployeeExportDto>();
        }
    }
}
