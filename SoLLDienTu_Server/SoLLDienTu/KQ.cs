using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoLLDienTu
{
    public class KQ
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public long total { get; set; }
        public dynamic data { get; set; }
    }
}
