using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Demo.CongNghePhanMem.Core.DTO.Departments;
using Demo.CongNghePhanMem.Core.Interfaces.Services;

namespace Demo.CongNghePhanMem.Api.Controllers
{
    /// <summary>
    /// Controller triển khai các phương thức của entities department
    /// </summary>
    /// Created By: BNTIEN (17/06/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseController<DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService) : base(departmentService)
        {
            _departmentService = departmentService;
        }

        #region API riêng (Department)
        /// <summary>
        /// Tìm kiếm phòng ban theo tên
        /// </summary>
        /// <param name="departmentName"></param>
        /// <returns>Danh sách phòng ban</returns>
        /// Created By: BNTIEN (24/06/2023)
        [HttpGet("name")]
        public async Task<IActionResult> GetByName(string? departmentName)
        {
            departmentName = departmentName ?? string.Empty;
            var res = await _departmentService.GetByName(departmentName);
            return Ok(res);
        }
        #endregion
    }
}
