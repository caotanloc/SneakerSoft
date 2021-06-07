using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ASM_ADIDAS.Models
{
    public partial class Sanpham
    {
        public int MaSp { get; set; }
        public string TenSp { get; set; }
        public decimal DonGia { get; set; }
        public string MoTaSp { get; set; }
        public string HinhAnh { get; set; }
        [DisplayName("Tải hình lên")]
        [NotMapped]
        public IFormFile ProfileImage { get; set; }
        public int NhomSp { get; set; }

        public virtual NhomSp NhomSpNavigation { get; set; }
    }
}
