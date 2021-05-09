using System;
using System.Collections.Generic;

#nullable disable

namespace SoLLDienTu.Models
{
    public partial class Gvmh
    {
        public string MaGv { get; set; }
        public string MaMh { get; set; }

        public virtual GiaoVien MaGvNavigation { get; set; }
        public virtual MonHoc MaMhNavigation { get; set; }
    }
}
