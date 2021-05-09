using System;
using System.Collections.Generic;

#nullable disable

namespace SoLLDienTu.Models
{
    public partial class MonHoc
    {
        public MonHoc()
        {
            Gvmhs = new HashSet<Gvmh>();
            KetQuas = new HashSet<KetQua>();
        }

        public string MaMh { get; set; }
        public string TenMh { get; set; }
        public int? SoTc { get; set; }
        public int? Kyhoc { get; set; }

        public virtual ICollection<Gvmh> Gvmhs { get; set; }
        public virtual ICollection<KetQua> KetQuas { get; set; }
    }
}
