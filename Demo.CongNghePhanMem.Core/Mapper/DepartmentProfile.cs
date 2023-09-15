using AutoMapper;
using Demo.CongNghePhanMem.Core.DTO.Departments;
using Demo.CongNghePhanMem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CongNghePhanMem.Core.Mapper
{
    /// <summary>
    /// class chuyển đổi dữ liệu department sang các đối tượng phù hợp
    /// </summary>
    /// Created By: BNTIEN (17/06/2023)
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile() 
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentCreateDto, Department>();
            CreateMap<DepartmentUpdateDto, Department>();
        }
    }
}
