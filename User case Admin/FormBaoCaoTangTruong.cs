using Microsoft.Reporting.WinForms;
using QuanLyChanNuoi.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyChanNuoi.User_case_Admin
{
    public partial class FormBaoCaoTangTruong : Form
    {
        private LiveStockContextDB db = new LiveStockContextDB();
        public FormBaoCaoTangTruong()
        {
            InitializeComponent();
        }

        private void FormBaoCaoTangTruong_Load(object sender, EventArgs e)
        {
            cbChonVatNuoi.DataSource = db.VatNuois.ToList();
            cbChonVatNuoi.DisplayMember = "TenVatNuoi";
            cbChonVatNuoi.ValueMember = "MaVatNuoi";
            cbChonVatNuoi.SelectedIndex = -1;

            this.reportViewer1.RefreshReport();
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            if (cbChonVatNuoi.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn một vật nuôi để xem báo cáo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maVatNuoiChon = cbChonVatNuoi.SelectedValue.ToString();

            try
            {
                var duLieuBaoCao = db.LichSuTangTruongs
                                     .Where(ls => ls.MaVatNuoi == maVatNuoiChon)
                                     .OrderBy(ls => ls.NgayKiemTra)
                                     .ToList();

                if (!duLieuBaoCao.Any())
                {
                    MessageBox.Show("Vật nuôi này chưa có dữ liệu tăng trưởng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.RefreshReport();
                    return;
                }

                ReportDataSource rds = new ReportDataSource("DataSetTangTruong", duLieuBaoCao);
                reportViewer1.LocalReport.ReportEmbeddedResource = "QuanLyChanNuoi.BaoCaoTangTruong.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tạo báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}