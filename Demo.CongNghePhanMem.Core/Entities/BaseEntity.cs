using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CongNghePhanMem.Core.Entities
{
    public abstract class BaseEntity
    {
        #region Property chung
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? createddate { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string? createdby { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime? modifieddate { get; set; }
        /// <summary>
        /// Người sửa
        /// </summary>
        public string? modifiedby { get; set; }
        #endregion
    }
}
