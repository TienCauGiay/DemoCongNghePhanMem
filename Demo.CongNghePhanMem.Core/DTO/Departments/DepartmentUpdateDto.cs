﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CongNghePhanMem.Core.DTO.Departments
{
    /// <summary>
    /// Lớp chuyển đổi đối tượng department sửa, bỏ đi 1 số trường không cần thiết
    /// </summary>
    /// Created By: BNTIEN (17/06/2023) 
    public class DepartmentUpdateDto
    {
        /// <summary>
        /// Khai báo các Property của thực thể Department
        /// </summary>
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string DepartmentCode { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
        #endregion
    }
}
