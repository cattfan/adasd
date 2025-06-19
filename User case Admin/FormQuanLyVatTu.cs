using QuanLyChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyChanNuoi
{
    public partial class FormQuanLyVatTu : Form
    {
        LiveStockContextDB context = new LiveStockContextDB();

        public FormQuanLyVatTu()
        {
            InitializeComponent();
        }

        private void FormQuanLyVatTu_Load(object sender, EventArgs e)
        {
            try
            {
                BindGrid(context.VatTus.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindGrid(List<VatTu> listVatTu)
        {
            dgvVatTu.Rows.Clear();
            foreach (var item in listVatTu)
            {
                int index = dgvVatTu.Rows.Add();
                dgvVatTu.Rows[index].Cells[0].Value = item.MaVatTu;
                dgvVatTu.Rows[index].Cells[1].Value = item.TenVatTu;
                dgvVatTu.Rows[index].Cells[2].Value = item.DonViTinh;
                dgvVatTu.Rows[index].Cells[3].Value = item.SoLuong;
            }
        }

        private void ClearInputFields()
        {
            txtMaSo.Text = "";
            txtTen.Text = "";
            txtDonVi.Text = "";
            txtSoLuong.Text = "";
            txtMaSo.Focus();
            txtMaSo.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaSo.Text) ||
                    string.IsNullOrWhiteSpace(txtTen.Text) ||
                    string.IsNullOrWhiteSpace(txtDonVi.Text))
                {
                    MessageBox.Show("Mã, Tên và Đơn vị không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (context.VatTus.Any(vt => vt.MaVatTu == txtMaSo.Text))
                {
                    MessageBox.Show("Mã vật tư này đã tồn tại. Vui lòng nhập mã khác.", "Trùng mã vật tư", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong < 0)
                {
                    MessageBox.Show("Số lượng phải là một số nguyên không âm.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                VatTu newVatTu = new VatTu
                {
                    MaVatTu = txtMaSo.Text.Trim(),
                    TenVatTu = txtTen.Text.Trim(),
                    DonViTinh = txtDonVi.Text.Trim(),
                    SoLuong = soLuong,
                    TrangThai = soLuong > 0 ? "Còn hàng" : "Hết hàng"
                };

                context.VatTus.Add(newVatTu);
                context.SaveChanges();
                MessageBox.Show("Thêm vật tư thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                BindGrid(context.VatTus.ToList());
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm vật tư: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                VatTu vatTuToUpdate = context.VatTus.FirstOrDefault(vt => vt.MaVatTu == txtMaSo.Text);
                if (vatTuToUpdate != null)
                {
                    if (string.IsNullOrWhiteSpace(txtTen.Text) || string.IsNullOrWhiteSpace(txtDonVi.Text))
                    {
                        MessageBox.Show("Tên và Đơn vị không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong < 0)
                    {
                        MessageBox.Show("Số lượng phải là một số nguyên không âm.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    vatTuToUpdate.TenVatTu = txtTen.Text.Trim();
                    vatTuToUpdate.DonViTinh = txtDonVi.Text.Trim();
                    vatTuToUpdate.SoLuong = soLuong;
                    vatTuToUpdate.TrangThai = vatTuToUpdate.SoLuong > 0 ? "Còn hàng" : "Hết hàng";

                    context.SaveChanges();
                    MessageBox.Show("Cập nhật vật tư thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    BindGrid(context.VatTus.ToList());
                    ClearInputFields();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy vật tư để cập nhật. Vui lòng chọn một vật tư từ danh sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa vật tư: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                VatTu vatTuToDelete = context.VatTus.FirstOrDefault(vt => vt.MaVatTu == txtMaSo.Text);
                if (vatTuToDelete != null)
                {
                    if (MessageBox.Show($"Bạn có chắc chắn muốn xóa vật tư có mã '{vatTuToDelete.MaVatTu}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        context.VatTus.Remove(vatTuToDelete);
                        context.SaveChanges();
                        MessageBox.Show("Xóa vật tư thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        BindGrid(context.VatTus.ToList());
                        ClearInputFields();
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy vật tư để xóa. Vui lòng chọn một vật tư từ danh sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa vật tư: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvVatTu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvVatTu.Rows[e.RowIndex];
                txtMaSo.Text = row.Cells[0].Value?.ToString();
                txtTen.Text = row.Cells[1].Value?.ToString();
                txtDonVi.Text = row.Cells[2].Value?.ToString();
                txtSoLuong.Text = row.Cells[3].Value?.ToString();

                txtMaSo.Enabled = false;
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchText = txtTimKiem.Text.Trim().ToLower();
                List<VatTu> searchResults = context.VatTus
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
