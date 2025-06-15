using QuanLyChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;


namespace QuanLyChanNuoi.usercase_nhan_vien
{
    public partial class FormVatNuoiNV : Form
    {
        private LiveStockContextDB db = new LiveStockContextDB();
        public FormVatNuoiNV()
        {
            InitializeComponent();
        }
        private void FormVatNuoi_Load(object sender, EventArgs e)
        {
            LoadVatNuoi();
            LoadComboBoxes();
            ClearInputFields();
        }

        private void LoadVatNuoi()
        {
            try
            {
                List<VatNuoi> dsVatNuoi = db.VatNuois
                                            .Include(vn => vn.ChuongVatNuois)
                                            .ToList();
                BindGrid(dsVatNuoi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Vật nuôi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxes()
        {
            try
            {
                var danhSachChuong = db.ChuongVatNuois.ToList();
                cbMaChuong.DataSource = danhSachChuong;
                cbMaChuong.DisplayMember = "MaChuong";
                cbMaChuong.ValueMember = "MaChuong";
                cbMaChuong.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu ComboBoxes: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindGrid(List<VatNuoi> vatNuois)
        {
            dgvVatNuoi.Rows.Clear();
            foreach (var vn in vatNuois)
            {
                int index = dgvVatNuoi.Rows.Add();
                dgvVatNuoi.Rows[index].Cells[0].Value = vn.MaVatNuoi;
                dgvVatNuoi.Rows[index].Cells[1].Value = vn.TenVatNuoi;
                dgvVatNuoi.Rows[index].Cells[2].Value = vn.NgayNhap?.ToString("dd/MM/yyyy") ?? "";
                dgvVatNuoi.Rows[index].Cells[3].Value = vn.MaChuong;
                dgvVatNuoi.Rows[index].Cells[4].Value = vn.SoLuong;
            }
        }

        private void ClearInputFields()
        {
            txtMaVatNuoi.Clear();
            txtTenVatNuoi.Clear();
            dtNgayNhap.Value = DateTime.Now;
            cbMaChuong.SelectedIndex = -1;
            txtSoLuong.Clear();
            txtMaVatNuoi.Focus();
        }

        private void dgvVatNuoi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvVatNuoi.Rows[e.RowIndex];
                txtMaVatNuoi.Text = row.Cells[0].Value?.ToString();
                txtTenVatNuoi.Text = row.Cells[1].Value?.ToString();
                dtNgayNhap.Value = DateTime.TryParse(row.Cells[2].Value?.ToString(), out var date) ? date : DateTime.Now;
                string maChuongFromGrid = row.Cells[3].Value?.ToString();
                if (!string.IsNullOrEmpty(maChuongFromGrid))
                {
                    cbMaChuong.SelectedValue = maChuongFromGrid;
                }
                else
                {
                    cbMaChuong.SelectedIndex = -1;
                }

                txtSoLuong.Text = row.Cells[4].Value?.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
           
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
           
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaVatNuoi.Text) || string.IsNullOrWhiteSpace(txtTenVatNuoi.Text) || cbMaChuong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã, Tên Vật nuôi và chọn Chuồng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maVN = txtMaVatNuoi.Text.Trim();

            try
            {
                VatNuoi vnToUpdate = db.VatNuois.Find(maVN);

                if (vnToUpdate != null)
                {
                    vnToUpdate.TenVatNuoi = txtTenVatNuoi.Text.Trim();
                    vnToUpdate.NgayNhap = dtNgayNhap.Value;
                    vnToUpdate.MaChuong = cbMaChuong.SelectedValue.ToString();

                    int? soLuong = null;
                    if (!string.IsNullOrWhiteSpace(txtSoLuong.Text))
                    {
                        if (int.TryParse(txtSoLuong.Text, out int parsedSoLuong))
                        {
                            soLuong = parsedSoLuong;
                        }
                        else
                        {
                            MessageBox.Show("Số lượng không hợp lệ. Vui lòng nhập số nguyên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    vnToUpdate.SoLuong = soLuong;

                    db.SaveChanges();
                    MessageBox.Show("Cập nhật Vật nuôi thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadVatNuoi();
                    ClearInputFields();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy Vật nuôi có mã này để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa Vật nuôi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();

            var filteredList = db.VatNuois
                                 .Where(vn => vn.MaVatNuoi.ToLower().Contains(keyword) ||
                                               vn.TenVatNuoi.ToLower().Contains(keyword) ||
                                               vn.MaChuong.ToLower().Contains(keyword))
                                 .ToList();

            BindGrid(filteredList);
        }
    }
}
