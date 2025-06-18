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
        public int ID { get; set; } // Đổi tên cho nhất quán
        public string MaVatNuoi { get; set; } // <--- Sửa tên và kiểu dữ liệu
        public DateTime NgayKiemTra { get; set; } // <--- Sửa tên cho khớp với form
        public int? SoLuongMau { get; set; }
        public double? TongCanNangMau { get; set; }
        public double? CanNangTrungBinhMau { get; set; }
        public int? SoLuongThucTeTrongDan { get; set; }
        public double? TongTrongLuongUocTinh { get; set; }
        public string GhiChu { get; set; }
    }
}