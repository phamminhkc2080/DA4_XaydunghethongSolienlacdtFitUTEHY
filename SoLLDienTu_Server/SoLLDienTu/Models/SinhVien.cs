using System;
using System.Collections.Generic;

#nullable disable

namespace SoLLDienTu.Models
{
    public partial class SinhVien
    {
        public SinhVien()
        {
            KetQuas = new HashSet<KetQua>();
        }

        public string MaSv { get; set; }
        public string TenSv { get; set; }
        public string NgaySinh { get; set; }
        public int? GioiTinh { get; set; }
        public string ThuongTru { get; set; }
        public string TamTru { get; set; }
        public string Sdt { get; set; }
        public string Anh { get; set; }
        public string MaLop { get; set; }

        public virtual Lop MaLopNavigation { get; set; }
        public virtual ICollection<KetQua> KetQuas { get; set; }
    }
}
