using QuanLyChanNuoi.Models;
using QuanLyChanNuoi.User_case_Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyChanNuoi
{
    public partial class FormHoaDon : Form
    {
        private LiveStockContextDB db = new LiveStockContextDB();
        private List<ChiTietHoaDon> danhSachChiTietHienTai;
        private ChiTietHoaDon selectedChiTiet;

        public FormHoaDon()
        {
            InitializeComponent();
        }

        private void FormHoaDon_Load(object sender, EventArgs e)
        {
            danhSachChiTietHienTai = new List<ChiTietHoaDon>();
            LoadComboBoxData();
            BindGrid(danhSachChiTietHienTai);
        }

        private void LoadComboBoxData()
        {
            try
            {
                cmbVatTu.DataSource = db.VatTus.ToList();
                cmbVatTu.DisplayMember = "TenVatTu";
                cmbVatTu.ValueMember = "MaVatTu";

                cmbNhaCungCap.DataSource = db.NhaCungCaps.ToList();
                cmbNhaCungCap.DisplayMember = "TenNhaCungCap";
                cmbNhaCungCap.ValueMember = "MaNhaCungCap";

                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu vật tư/nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindGrid(List<ChiTietHoaDon> danhSach)
        {
            dgvHoaDon.Rows.Clear();
            if (danhSach == null) return;

            foreach (var item in danhSach)
            {
                int index = dgvHoaDon.Rows.Add();
                dgvHoaDon.Rows[index].Cells[0].Value = item.VatTu?.TenVatTu;
                dgvHoaDon.Rows[index].Cells[1].Value = item.NhaCungCap?.TenNhaCungCap;
                dgvHoaDon.Rows[index].Cells[2].Value = item.SoLuong;
                dgvHoaDon.Rows[index].Cells[3].Value = item.DonGia?.ToString("N0");
                dgvHoaDon.Rows[index].Cells[4].Value = ((item.SoLuong ?? 0) * (item.DonGia ?? 0)).ToString("N0");
                dgvHoaDon.Rows[index].Tag = item;
            }
            UpdateTongTien();
        }

        private void ClearInputFields()
        {
            cmbVatTu.SelectedIndex = -1;
            cmbNhaCungCap.SelectedIndex = -1;
            txtDonGia.Clear();
            nudSoLuong.Value = 0;
            selectedChiTiet = null;
            cmbVatTu.Focus();
        }

        private void UpdateTongTien()
        {
            decimal tongTien = danhSachChiTietHienTai.Sum(item => (item.SoLuong ?? 0) * (item.DonGia ?? 0));
            txtTongThanhTien.Text = tongTien.ToString("N0") + " VNĐ";
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvHoaDon.Rows.Count)
            {
                selectedChiTiet = dgvHoaDon.Rows[e.RowIndex].Tag as ChiTietHoaDon;
                if (selectedChiTiet != null)
                {
                    cmbVatTu.SelectedValue = selectedChiTiet.MaMatHang;
                    cmbNhaCungCap.SelectedValue = selectedChiTiet.MaNhaCungCap;
                    nudSoLuong.Value = selectedChiTiet.SoLuong ?? 1;
                    txtDonGia.Text = selectedChiTiet.DonGia?.ToString("N0");
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cmbVatTu.SelectedValue == null || cmbNhaCungCap.SelectedValue == null || nudSoLuong.Value <= 0 || string.IsNullOrWhiteSpace(txtDonGia.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtDonGia.Text, out decimal donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var newDetail = new ChiTietHoaDon
            {
                MaMatHang = cmbVatTu.SelectedValue as string,
                MaNhaCungCap = cmbNhaCungCap.SelectedValue as string,
                SoLuong = (int)nudSoLuong.Value,
                DonGia = donGia,
                VatTu = cmbVatTu.SelectedItem as VatTu,
                NhaCungCap = cmbNhaCungCap.SelectedItem as NhaCungCap,
            };
            danhSachChiTietHienTai.Add(newDetail);
            BindGrid(danhSachChiTietHienTai);
            ClearInputFields();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedChiTiet == null)
            {
                MessageBox.Show("Vui lòng chọn một mục để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtDonGia.Text, out decimal donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            selectedChiTiet.MaMatHang = cmbVatTu.SelectedValue as string;
            selectedChiTiet.VatTu = cmbVatTu.SelectedItem as VatTu;
            selectedChiTiet.MaNhaCungCap = cmbNhaCungCap.SelectedValue as string;
            selectedChiTiet.NhaCungCap = cmbNhaCungCap.SelectedItem as NhaCungCap;
            selectedChiTiet.SoLuong = (int)nudSoLuong.Value;
            selectedChiTiet.DonGia = donGia;

            BindGrid(danhSachChiTietHienTai);
            ClearInputFields();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedChiTiet == null)
            {
                MessageBox.Show("Vui lòng chọn một mục để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa mục này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                danhSachChiTietHienTai.Remove(selectedChiTiet);
                BindGrid(danhSachChiTietHienTai);
                ClearInputFields();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!danhSachChiTietHienTai.Any())
            {
                MessageBox.Show("Hóa đơn trống, không có gì để lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    string maHDMoi = "HD" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
                    var hoaDonMoi = new HoaDon
                    {
                        MaHoaDon = maHDMoi,
                        NgayLap = DateTime.Now
                    };
                    db.HoaDons.Add(hoaDonMoi);
                    db.SaveChanges(); // Lưu hóa đơn trước để có MaHoaDon

                    int sttCounter = 1;
                    foreach (var chiTietTam in danhSachChiTietHienTai)
                    {
                        var vatTuTrongDb = db.VatTus.Find(chiTietTam.MaMatHang);
                        if (vatTuTrongDb != null)
                        {
                            vatTuTrongDb.SoLuong += chiTietTam.SoLuong;
                        }

                        var chiTietMoi = new ChiTietHoaDon
                        {
                            MaHoaDon = maHDMoi,
                            STT = sttCounter++,
                            MaMatHang = chiTietTam.MaMatHang,
                            MaNhaCungCap = chiTietTam.MaNhaCungCap,
                            SoLuong = chiTietTam.SoLuong,
                            DonGia = chiTietTam.DonGia
                        };
                        db.ChiTietHoaDons.Add(chiTietMoi);
                    }

                    db.SaveChanges();
                    transaction.Commit();

                    MessageBox.Show($"Lưu hóa đơn thành công! Mã hóa đơn mới: {maHDMoi}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    danhSachChiTietHienTai.Clear();
                    BindGrid(danhSachChiTietHienTai);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Lỗi khi lưu hóa đơn: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXemHoaDon_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormXemhoadon formXemHoaDon = new FormXemhoadon();
            formXemHoaDon.FormClosed += (s, args) => this.Show();
            formXemHoaDon.Show();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
