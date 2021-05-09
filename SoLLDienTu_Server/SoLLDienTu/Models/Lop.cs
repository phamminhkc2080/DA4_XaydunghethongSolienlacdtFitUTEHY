using System;
using System.Collections.Generic;

#nullable disable

namespace SoLLDienTu.Models
{
    public partial class Lop
    {
        public Lop()
        {
            SinhViens = new HashSet<SinhVien>();
        }

        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public string MaGv { get; set; }

        public virtual GiaoVien MaGvNavigation { get; set; }
        public virtual ICollection<SinhVien> SinhViens { get; set; }
    }
}
