using Demo.CongNghePhanMem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CongNghePhanMem.Core.Interfaces.Infrastructures
{
    /// <summary>
    /// Interface employee repository
    /// </summary>
    /// Created By: BNTIEN (17/06/2023)
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        #region Method riêng (Department)
        /// <summary>
        /// Tìm kiếm phòng ban theo tên
        /// </summary>
        /// <returns>Danh sách phòng ban</returns>
        /// Created By: BNTIEN (17/06/2023)
        Task<IEnumerable<Department>?> GetByName(string? textSearch);
        #endregion
    }
}
