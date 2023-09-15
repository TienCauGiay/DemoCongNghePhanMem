using Dapper;
using Microsoft.Extensions.Configuration;
using Demo.CongNghePhanMem.Core.Entities;
using Demo.CongNghePhanMem.Core.Interfaces.Infrastructures;
using Demo.CongNghePhanMem.Core.Interfaces.UnitOfWork;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CongNghePhanMem.Infrastructure.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #region Method riêng (Department)
        /// <summary>
        /// Tìm kiếm phòng ban theo tên
        /// </summary>
        /// <returns>Danh sách phòng ban</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<IEnumerable<Department>?> GetByName(string? textSearch)
        {
            textSearch = textSearch ?? string.Empty;
            var query = $"SELECT * FROM department WHERE departmentname LIKE @textSearch";
            var parameters = new { textSearch = $"%{textSearch}%" };

            var res = await _unitOfWork.Connection.QueryAsync<Department>(query, parameters, transaction: _unitOfWork.Transaction);
            return res;
        }

        #endregion
    }
}
