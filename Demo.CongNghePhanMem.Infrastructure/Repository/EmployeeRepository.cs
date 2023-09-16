using Dapper;
using Microsoft.Extensions.Configuration;
using Demo.CongNghePhanMem.Core.DTO.Employees;
using Demo.CongNghePhanMem.Core.Entities;
using Demo.CongNghePhanMem.Core.Interfaces.Infrastructures;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.CongNghePhanMem.Core.Resources;
using Demo.CongNghePhanMem.Core.Interfaces.UnitOfWork;
using static Dapper.SqlMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Demo.CongNghePhanMem.Infrastructure.Repository
{
    /// <summary>
    /// class triển khai các phương thức của thực thể employee truy vấn cơ sở dữ liệu
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Created By: BNTIEN (17/06/2023)
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        /// <summary>
        /// Hàm tạo
        /// </summary>
        /// <param name="configuration"></param>
        public EmployeeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #region Method riêng (Employee)
        /// <summary>
        /// Lấy mã nhân viên lớn nhất trong hệ thống
        /// </summary>
        /// <returns>Mã nhân viên</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<string?> GetByCodeMaxAsync()
        {
            var sql = @"
        SELECT
            e.EmployeeCode
        FROM employee e
        WHERE e.EmployeeCode ~ '^NV-[0-9]+$' 
        ORDER BY LENGTH(e.EmployeeCode) DESC, e.EmployeeCode DESC
        LIMIT 1;";

            var maxEmployeeCode = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<string>(sql);
            return maxEmployeeCode;
        }


        /// <summary>
        /// Tìm kiếm và phân trang trên giao diện
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns>Danh sách nhân viên theo tìm kiếm, phân trang</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<FilterEmployee?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch)
        {
            textSearch = textSearch ?? string.Empty;
            var offset = (pageNumber - 1) * pageSize;

            try
            {
                var query = $@"
            SELECT
                veg.EmployeeId,
                veg.EmployeeCode,
                veg.FullName,
                veg.DateOfBirth,
                veg.Gender,
                veg.DepartmentId,
                veg.IdentityNumber,
                veg.IdentityDate,
                veg.IdentityPlace,
                veg.PositionName,
                veg.Address,
                veg.PhoneNumber,
                veg.PhoneLandline,
                veg.Email,
                veg.BankAccount,
                veg.BankName,
                veg.BankBranch,
                veg.IsCustomer,
                veg.IsProvider,
                veg.CreatedBy,
                veg.CreatedDate,
                veg.ModifiedBy,
                veg.ModifiedDate,
                veg.DepartmentName
            FROM view_employee_getall veg
            WHERE veg.EmployeeCode LIKE '%' || @textSearch || '%'
                OR veg.FullName LIKE '%' || @textSearch || '%'
                OR veg.PhoneNumber LIKE '%' || @textSearch || '%'
            OFFSET @offset LIMIT @pageSize;
        ";
                var query2 = $@"
            SELECT
                count(*)
            FROM view_employee_getall veg
            WHERE veg.EmployeeCode LIKE '%' || @textSearch || '%'
                OR veg.FullName LIKE '%' || @textSearch || '%'
                OR veg.PhoneNumber LIKE '%' || @textSearch || '%'
        ";
                var parameters = new
                {
                    textSearch,
                    pageSize,
                    offset
                };
                var employees = await _unitOfWork.Connection.QueryAsync<Employee>(query, parameters, transaction: _unitOfWork.Transaction);
                var totalRecord = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<int>(query2, parameters, transaction: _unitOfWork.Transaction);

                var currentPageRecords = Math.Min(pageSize, employees.Count());

                return new FilterEmployee
                {
                    TotalPage = (int)Math.Ceiling((decimal)totalRecord / pageSize),
                    TotalRecord = totalRecord,
                    CurrentPage = pageNumber,
                    CurrentPageRecords = currentPageRecords,
                    Data = employees.ToList()
                };
            }
            catch (Exception)
            {
                throw; // Rethrow the exception
            }
        }

        public override async Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            var rowsAffected = await base.DeleteMultipleAsync(ids);
            return rowsAffected;
        }

        public Task<MemoryStream> ExportExcelAsync(List<EmployeeExportDto> listEmployeeDto)
        {
            throw new NotImplementedException();
        }

        public override async Task<int> InsertAsync(Employee employee)
        {
            var sql = @"
        INSERT INTO employee (
            employeeid,
            employeecode,
            fullname,
            dateofbirth,
            gender,
            departmentid,
            identitynumber,
            identitydate,
            identityplace,
            positionname,
            address,
            phonenumber,
            phonelandline,
            email,
            bankaccount,
            bankname,
            bankbranch,
            iscustomer,
            isprovider,
            createdby,
            createddate,
            modifiedby,
            modifieddate
        ) VALUES (
            @EmployeeId,
            @EmployeeCode,
            @FullName,
            @DateOfBirth,
            @Gender,
            @DepartmentId,
            @IdentityNumber,
            @IdentityDate,
            @IdentityPlace,
            @PositionName,
            @Address,
            @PhoneNumber,
            @PhoneLandline,
            @Email,
            @BankAccount,
            @BankName,
            @BankBranch,
            @IsCustomer,
            @IsProvider,
            @CreatedBy,
            @CreatedDate,
            @ModifiedBy,
            @ModifiedDate
        )"
            ;

            var rowsAffected = await _unitOfWork.Connection.ExecuteAsync(sql, employee);
            return rowsAffected;
        }

        public override async Task<int> UpdateAsync(Employee employee, Guid id)
        {
            var sql = @"
        UPDATE employee
        SET
            employeecode = @EmployeeCode,
            fullname = @FullName,
            dateofbirth = @DateOfBirth,
            gender = @Gender,
            departmentid = @DepartmentId,
            identitynumber = @IdentityNumber,
            identitydate = @IdentityDate,
            identityplace = @IdentityPlace,
            positionname = @PositionName,
            address = @Address,
            phonenumber = @PhoneNumber,
            phonelandline = @PhoneLandline,
            email = @Email,
            bankaccount = @BankAccount,
            bankname = @BankName,
            bankbranch = @BankBranch,
            iscustomer = @IsCustomer,
            isprovider = @IsProvider,
            modifiedby = @ModifiedBy,
            modifieddate = @ModifiedDate
        WHERE
            employeeid = @EmployeeId";

            var parameters = new
            {
                EmployeeId = id,
                employee.EmployeeCode,
                employee.FullName,
                employee.DateOfBirth,
                employee.Gender,
                employee.DepartmentId,
                employee.IdentityNumber,
                employee.IdentityDate,
                employee.IdentityPlace,
                employee.PositionName,
                employee.Address,
                employee.PhoneNumber,
                employee.PhoneLandline,
                employee.Email,
                employee.BankAccount,
                employee.BankName,
                employee.BankBranch,
                employee.IsCustomer,
                employee.IsProvider,
                employee.ModifiedBy,
                employee.ModifiedDate
            };

            var rowsAffected = await _unitOfWork.Connection.ExecuteAsync(sql, parameters);
            return rowsAffected;
        }

        #endregion
    }
}
