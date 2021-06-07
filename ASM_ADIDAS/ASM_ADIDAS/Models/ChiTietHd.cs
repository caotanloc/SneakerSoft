using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ASM_ADIDAS.Models
{
    public partial class ChiTietHd
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public int MaSp { get; set; }
        public int IdhoaDon { get; set; }
    }
}
