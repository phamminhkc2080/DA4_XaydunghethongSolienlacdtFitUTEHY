using System;
using System.Collections.Generic;

#nullable disable

namespace SoLLDienTu.Models
{
    public partial class GiaoVien
    {
        public GiaoVien()
        {
            Gvmhs = new HashSet<Gvmh>();
            Lops = new HashSet<Lop>();
        }

        public string MaGv { get; set; }
        public string TenGv { get; set; }
        public string NgaySinh { get; set; }
        public int? GioiTinh { get; set; }
        public string QueQuan { get; set; }
        public string Anh { get; set; }

        public virtual ICollection<Gvmh> Gvmhs { get; set; }
        public virtual ICollection<Lop> Lops { get; set; }
    }
}
