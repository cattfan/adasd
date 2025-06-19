using QuanLyChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyChanNuoi
{
    public partial class FormNhaCungCap : Form
    {
        LiveStockContextDB context = new LiveStockContextDB();

        public FormNhaCungCap()
        {
            InitializeComponent();
        }

        private void FormNhaCungCap_Load(object sender, EventArgs e)
        {
            try
            {
                BindGrid(context.NhaCungCaps.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindGrid(List<NhaCungCap> listNhaCungCap)
        {
            dgvNhaCungCap.Rows.Clear();
            foreach (var item in listNhaCungCap)
            {
                int index = dgvNhaCungCap.Rows.Add();
                dgvNhaCungCap.Rows[index].Cells[0].Value = item.MaNhaCungCap;
                dgvNhaCungCap.Rows[index].Cells[1].Value = item.TenNhaCungCap;
                dgvNhaCungCap.Rows[index].Cells[2].Value = item.DiaChi;
            }
        }

        private void ClearInputFields()
        {
            txtMaNhaCungCap.Text = "";
            txtTenNhaCungCap.Text = "";
            txtDiaChi.Text = "";
            txtMaNhaCungCap.Focus();
            txtMaNhaCungCap.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaNhaCungCap.Text) ||
                    string.IsNullOrWhiteSpace(txtTenNhaCungCap.Text))
                {
                    MessageBox.Show("Mã và Tên nhà cung cấp không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (context.NhaCungCaps.Any(ncc => ncc.MaNhaCungCap == txtMaNhaCungCap.Text))
                {
                    MessageBox.Show("Mã nhà cung cấp này đã tồn tại. Vui lòng nhập mã khác.", "Trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                NhaCungCap newNhaCungCap = new NhaCungCap
                {
                    MaNhaCungCap = txtMaNhaCungCap.Text.Trim(),
                    TenNhaCungCap = txtTenNhaCungCap.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim()
                };

                context.NhaCungCaps.Add(newNhaCungCap);
                context.SaveChanges();
                MessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                BindGrid(context.NhaCungCaps.ToList());
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                NhaCungCap nccToUpdate = context.NhaCungCaps.FirstOrDefault(ncc => ncc.MaNhaCungCap == txtMaNhaCungCap.Text);
                if (nccToUpdate != null)
                {
                    if (string.IsNullOrWhiteSpace(txtTenNhaCungCap.Text))
                    {
                        MessageBox.Show("Tên nhà cung cấp không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    nccToUpdate.TenNhaCungCap = txtTenNhaCungCap.Text.Trim();
                    nccToUpdate.DiaChi = txtDiaChi.Text.Trim();

                    context.SaveChanges();
                    MessageBox.Show("Cập nhật nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    BindGrid(context.NhaCungCaps.ToList());
                    ClearInputFields();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhà cung cấp để cập nhật. Vui lòng chọn một nhà cung cấp từ danh sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                NhaCungCap nccToDelete = context.NhaCungCaps.FirstOrDefault(ncc => ncc.MaNhaCungCap == txtMaNhaCungCap.Text);
                if (nccToDelete != null)
                {
                    if (MessageBox.Show($"Bạn có chắc chắn muốn xóa nhà cung cấp '{nccToDelete.TenNhaCungCap}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        context.NhaCungCaps.Remove(nccToDelete);
                        context.SaveChanges();
                        MessageBox.Show("Xóa nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        BindGrid(context.NhaCungCaps.ToList());
                        ClearInputFields();
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhà cung cấp để xóa. Vui lòng chọn một nhà cung cấp từ danh sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhaCungCap.Rows[e.RowIndex];
                txtMaNhaCungCap.Text = row.Cells[0].Value?.ToString();
                txtTenNhaCungCap.Text = row.Cells[1].Value?.ToString();
                txtDiaChi.Text = row.Cells[2].Value?.ToString();

                txtMaNhaCungCap.Enabled = false;
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchText = txtTimKiem.Text.Trim().ToLower();
                List<NhaCungCap> searchResults = context.NhaCungCaps
                    .Where(ncc => ncc.MaNhaCungCap.ToLower().Contains(searchText) || ncc.TenNhaCungCap.ToLower().Contains(searchText))
                    .ToList();
                BindGrid(searchResults);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
