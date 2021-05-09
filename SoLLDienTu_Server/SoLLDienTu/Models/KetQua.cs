using System;
using System.Collections.Generic;

#nullable disable

namespace SoLLDienTu.Models
{
    public partial class KetQua
    {
        public string MaSv { get; set; }
        public string MaMh { get; set; }
        public double? DiemLd { get; set; }
        public double? DiemTl { get; set; }

        public virtual MonHoc MaMhNavigation { get; set; }
        public virtual SinhVien MaSvNavigation { get; set; }
    }
}
