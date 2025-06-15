using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChanNuoi.Models
{
    [Table("LichSuTangTruong")]
    public partial class LichSuTangTruong
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string MaVatNuoi { get; set; }

        public DateTime NgayKiemTra { get; set; }

        public int? SoLuongMau { get; set; }

        public double? TongCanNangMau { get; set; }

        public double? CanNangTrungBinhMau { get; set; }

        public int? SoLuongThucTeTrongDan { get; set; }

        public double? TongTrongLuongUocTinh { get; set; }

        [StringLength(255)]
        public string GhiChu { get; set; }

        public virtual VatNuoi VatNuoi { get; set; }
    }
}