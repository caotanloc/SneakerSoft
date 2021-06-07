using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ASM_ADIDAS.Models
{
    public partial class NhomSp
    {
        public NhomSp()
        {
            Sanpham = new HashSet<Sanpham>();
        }

        public int MaNhom { get; set; }
        public string TenNhom { get; set; }

        public virtual ICollection<Sanpham> Sanpham { get; set; }
    }
}
