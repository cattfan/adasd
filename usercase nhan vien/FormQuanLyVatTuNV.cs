using QuanLyChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;

namespace QuanLyChanNuoi
{
    public partial class FormQuanLyVatTuNV : Form
    {
        private LiveStockContextDB db = new LiveStockContextDB();

        public FormQuanLyVatTuNV()
        {
            InitializeComponent();
        }

        private void FormQuanLyVatTuNV_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDataIntoDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataIntoDataGridView()
        {
            List<VatTu> listVattu = db.VatTus.ToList();
            BindGrid(listVattu);
        }

        private void BindGrid(List<VatTu> listVattu)
        {
            dgvVatTu.Rows.Clear();
            foreach (var item in listVattu)
            {
                int index = dgvVatTu.Rows.Add();
                dgvVatTu.Rows[index].Cells[0].Value = item.MaVatTu;
                dgvVatTu.Rows[index].Cells[1].Value = item.TenVatTu;
                dgvVatTu.Rows[index].Cells[2].Value = item.DonViTinh;
                dgvVatTu.Rows[index].Cells[3].Value = item.SoLuong;
                dgvVatTu.Rows[index].Cells[4].Value = (item.SoLuong.HasValue && item.SoLuong > 0) ? "Còn hàng" : "Hết hàng";
            }
        }

        private void ClearInputFields()
        {
            txtMaVatTu.Text = "";
            txtTenVatTu.Text = "";
            txtDonViTinh.Text = "";
            txtSoLuong.Text = "";
            txtMaVatTu.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaVatTu.Text))
                {
                    MessageBox.Show("Vui lòng chọn một vật tư để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                VatTu vatTuToUpdate = db.VatTus.FirstOrDefault(vt => vt.MaVatTu == txtMaVatTu.Text);
                if (vatTuToUpdate != null)
                {
                    if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong < 0)
                    {
                        MessageBox.Show("Số lượng phải là một số nguyên không âm.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    vatTuToUpdate.SoLuong = soLuong;
                    vatTuToUpdate.TrangThai = vatTuToUpdate.SoLuong > 0 ? "Còn hàng" : "Hết hàng";

                    db.SaveChanges();
                    MessageBox.Show("Cập nhật số lượng vật tư thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadDataIntoDataGridView();
                    ClearInputFields();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy vật tư để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa vật tư: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvVatTu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvVatTu.Rows[e.RowIndex];
                txtMaVatTu.Text = row.Cells[0].Value?.ToString();
                txtTenVatTu.Text = row.Cells[1].Value?.ToString();
                txtDonViTinh.Text = row.Cells[2].Value?.ToString();
                txtSoLuong.Text = row.Cells[3].Value?.ToString();
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchText = txtTimKiem.Text.Trim().ToLower();
                List<VatTu> searchResults = db.VatTus
                    .Where(vt => vt.MaVatTu.ToLower().Contains(searchText) || vt.TenVatTu.ToLower().Contains(searchText))
                    .ToList();
                BindGrid(searchResults);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm vật tư: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
