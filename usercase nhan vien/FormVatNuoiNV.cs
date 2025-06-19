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
            try
            {
                LoadVatNuoi();
                LoadComboBoxes();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải form: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadVatNuoi()
        {
            List<VatNuoi> dsVatNuoi = db.VatNuois
                                        .Include(vn => vn.ChuongVatNuois)
                                        .ToList();
            BindGrid(dsVatNuoi);
        }

        private void LoadComboBoxes()
        {
            try
            {
                cbMaChuong.DataSource = db.ChuongVatNuois.ToList();
                cbMaChuong.DisplayMember = "MaChuong";
                cbMaChuong.ValueMember = "MaChuong";
                cbMaChuong.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách chuồng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                dgvVatNuoi.Rows[index].Cells[2].Value = vn.NgayNhap.HasValue ? vn.NgayNhap.Value.ToString("dd/MM/yyyy") : "";
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
            txtTenVatNuoi.Enabled = false;
            dtNgayNhap.Enabled = false;
            cbMaChuong.Enabled = false;
            txtSoLuong.Enabled = true;
            txtMaVatNuoi.Focus();
        }

        private void dgvVatNuoi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvVatNuoi.Rows[e.RowIndex];
                txtMaVatNuoi.Text = row.Cells[0].Value?.ToString();
                txtTenVatNuoi.Text = row.Cells[1].Value?.ToString();
                if (DateTime.TryParse(row.Cells[2].Value?.ToString(), out var date))
                {
                    dtNgayNhap.Value = date;
                }
                cbMaChuong.SelectedValue = row.Cells[3].Value?.ToString();
                txtSoLuong.Text = row.Cells[4].Value?.ToString();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaVatNuoi.Text))
            {
                MessageBox.Show("Vui lòng chọn một vật nuôi để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                VatNuoi vnToUpdate = db.VatNuois.Find(txtMaVatNuoi.Text.Trim());

                if (vnToUpdate != null)
                {
                    if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong < 0)
                    {
                        MessageBox.Show("Số lượng không hợp lệ. Vui lòng nhập số nguyên không âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    vnToUpdate.SoLuong = soLuong;

                    db.SaveChanges();
                    MessageBox.Show("Cập nhật số lượng vật nuôi thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadVatNuoi();
                    ClearInputFields();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy vật nuôi có mã này để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa vật nuôi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
