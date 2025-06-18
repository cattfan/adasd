using Microsoft.Reporting.WinForms;
using QuanLyChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyChanNuoi.User_case_Admin
{
    public partial class FormThongKeTongQuat : Form
    {
        private LiveStockContextDB db = new LiveStockContextDB();

        public FormThongKeTongQuat()
        {
            InitializeComponent();
        }

        private void FormThongKeTongQuat_Load(object sender, EventArgs e)
        {
            try
            {
                var thongKeData = new List<ThongKeItem>
                {
                    new ThongKeItem { TenThongKe = "Nhân Viên", SoLuong = db.NhanViens.Count() },
                    new ThongKeItem { TenThongKe = "Vật Nuôi", SoLuong = db.VatNuois.Sum(v => v.SoLuong) ?? 0 },
                    new ThongKeItem { TenThongKe = "Chuồng Trại", SoLuong = db.ChuongVatNuois.Count() },
                    new ThongKeItem { TenThongKe = "Vật Tư", SoLuong = db.VatTus.Count() },
                    new ThongKeItem { TenThongKe = "Nhà Cung Cấp", SoLuong = db.NhaCungCaps.Count() },
                    new ThongKeItem { TenThongKe = "Hóa Đơn", SoLuong = db.HoaDons.Count() }
                };

                ReportDataSource rds = new ReportDataSource("ThongKeDataSet", thongKeData);

                reportViewer1.LocalReport.ReportEmbeddedResource = "QuanLyChanNuoi.ThongKeTongQuat.rdlc";

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);

                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tạo báo cáo thống kê: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormThongKeTongQuat_Load_1(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }

    public class ThongKeItem
    {
        public string TenThongKe { get; set; }
        public int SoLuong { get; set; }
    }
}