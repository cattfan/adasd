using QuanLyChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;

namespace QuanLyChanNuoi.User_case_Admin
{
    public partial class FormXemhoadon : Form
    {
        private LiveStockContextDB db = new LiveStockContextDB();
        private List<ChiTietHoaDon> allInvoiceDetails;

        public FormXemhoadon()
        {
            InitializeComponent();
        }

        private void FormXemhoadon_Load(object sender, EventArgs e)
        {
            LoadAllData();
        }

        private void LoadAllData()
        {
            try
            {
                this.allInvoiceDetails = db.ChiTietHoaDons
                                           .Include(ct => ct.HoaDon)
                                           .Include(ct => ct.VatTu)
                                           .Include(ct => ct.NhaCungCap)
                                           .ToList();

                BindGrid(this.allInvoiceDetails);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không thể tải dữ liệu hóa đơn: " + (ex.InnerException?.Message ?? ex.Message), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindGrid(List<ChiTietHoaDon> dataToDisplay)
        {
            dgvHoaDon.Rows.Clear();
            foreach (var item in dataToDisplay)
            {
                int index = dgvHoaDon.Rows.Add();
                dgvHoaDon.Rows[index].Cells[0].Value = item.HoaDon != null ? item.HoaDon.MaHoaDon : "";
                dgvHoaDon.Rows[index].Cells[1].Value = item.HoaDon != null && item.HoaDon.NgayLap.HasValue ? item.HoaDon.NgayLap.Value.ToString("dd/MM/yyyy") : "";
                dgvHoaDon.Rows[index].Cells[2].Value = item.VatTu != null ? item.VatTu.TenVatTu : "";
                dgvHoaDon.Rows[index].Cells[3].Value = item.NhaCungCap != null ? item.NhaCungCap.TenNhaCungCap : "";
                dgvHoaDon.Rows[index].Cells[4].Value = item.SoLuong;
                dgvHoaDon.Rows[index].Cells[5].Value = item.DonGia?.ToString("N0");
                dgvHoaDon.Rows[index].Cells[6].Value = ((item.SoLuong ?? 0) * (item.DonGia ?? 0)).ToString("N0");
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtTimKiem.Text.ToLower().Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                BindGrid(this.allInvoiceDetails);
                return;
            }

            var filteredList = this.allInvoiceDetails.Where(item =>
                (item.HoaDon != null && item.HoaDon.MaHoaDon.ToLower().Contains(searchTerm)) ||
                (item.VatTu != null && item.VatTu.TenVatTu.ToLower().Contains(searchTerm)) ||
                (item.NhaCungCap != null && item.NhaCungCap.TenNhaCungCap.ToLower().Contains(searchTerm))
            ).ToList();

            BindGrid(filteredList);
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
