using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CongNghePhanMem.Core.Entities
{
    public class Department : BaseEntity
    {
        /// <summary>
        /// Khai báo các Property của thực thể Department
        /// </summary>
        #region Property riêng (Department)
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid departmentid { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string departmentdode { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string departmentname { get; set; }
        #endregion
    }
}
