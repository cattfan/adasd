using QuanLyChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyChanNuoi
{
    public partial class FormQuanLyNhanVien : Form
    {
        LiveStockContextDB context = new LiveStockContextDB();

        public FormQuanLyNhanVien()
        {
            InitializeComponent();
        }

        private void FormQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            try
            {
                LoadComboBoxes();
                LoadNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải form: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadNhanVien()
        {
            var danhSach = context.NhanViens
                                  .Include(nv => nv.ToNhanVien)
                                  .Include(nv => nv.ChucVuNhanVien)
                                  .ToList();
            BindGrid(danhSach);
        }

        private void LoadComboBoxes()
        {
            cmbGioiTinh.Items.Add("Nam");
            cmbGioiTinh.Items.Add("Nữ");
            cmbGioiTinh.SelectedIndex = 0;

            cmbMaTo.DataSource = context.ToNhanViens.ToList();
            cmbMaTo.DisplayMember = "TenTo";
            cmbMaTo.ValueMember = "MaTo";

            cmbChucVu.DataSource = context.ChucVuNhanViens.ToList();
            cmbChucVu.DisplayMember = "TenChucVu";
            cmbChucVu.ValueMember = "MaChucVu";
        }

        private void BindGrid(List<NhanVien> nhanViens)
        {
            dgvNhanVien.Rows.Clear();
            foreach (var nv in nhanViens)
            {
                int index = dgvNhanVien.Rows.Add();
                dgvNhanVien.Rows[index].Cells[0].Value = nv.MaNhanVien;
                dgvNhanVien.Rows[index].Cells[1].Value = nv.HoTen;
                dgvNhanVien.Rows[index].Cells[2].Value = nv.NgaySinh.HasValue ? nv.NgaySinh.Value.ToString("dd/MM/yyyy") : "";
                dgvNhanVien.Rows[index].Cells[3].Value = nv.GioiTinh;
                dgvNhanVien.Rows[index].Cells[4].Value = nv.ToNhanVien != null ? nv.ToNhanVien.TenTo : "";
                dgvNhanVien.Rows[index].Cells[5].Value = nv.ChucVuNhanVien != null ? nv.ChucVuNhanVien.TenChucVu : "";
            }
        }

        private void ClearInputFields()
        {
            txtMaNV.Text = "";
            txtHoTen.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            cmbGioiTinh.SelectedIndex = 0;
            cmbMaTo.SelectedIndex = -1;
            cmbChucVu.SelectedIndex = -1;
            txtMaNV.Enabled = true;
            txtMaNV.Focus();
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                txtMaNV.Text = row.Cells[0].Value?.ToString();
                txtHoTen.Text = row.Cells[1].Value?.ToString();
                if (DateTime.TryParse(row.Cells[2].Value?.ToString(), out DateTime ngaySinh))
                {
                    dtpNgaySinh.Value = ngaySinh;
                }
                cmbGioiTinh.SelectedItem = row.Cells[3].Value?.ToString();

                string tenTo = row.Cells[4].Value?.ToString();
                if (!string.IsNullOrEmpty(tenTo))
                {
                    var to = context.ToNhanViens.FirstOrDefault(t => t.TenTo == tenTo);
                    if (to != null)
                    {
                        cmbMaTo.SelectedValue = to.MaTo;
                    }
                }

                string tenChucVu = row.Cells[5].Value?.ToString();
                if (!string.IsNullOrEmpty(tenChucVu))
                {
                    var chucVu = context.ChucVuNhanViens.FirstOrDefault(cv => cv.TenChucVu == tenChucVu);
                    if (chucVu != null)
                    {
                        cmbChucVu.SelectedValue = chucVu.MaChucVu;
                    }
                }

                txtMaNV.Enabled = false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaNV.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    MessageBox.Show("Mã và Tên nhân viên không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (context.NhanViens.Any(nv => nv.MaNhanVien == txtMaNV.Text))
                {
                    MessageBox.Show("Mã nhân viên đã tồn tại.", "Trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbMaTo.SelectedValue == null || cmbChucVu.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn Tổ và Chức vụ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                NhanVien newNhanVien = new NhanVien
                {
                    MaNhanVien = txtMaNV.Text.Trim(),
                    HoTen = txtHoTen.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value,
                    GioiTinh = cmbGioiTinh.SelectedItem.ToString(),
                    MaTo = cmbMaTo.SelectedValue.ToString(),
                    MaChucVu = cmbChucVu.SelectedValue.ToString()
                };

                context.NhanViens.Add(newNhanVien);
                context.SaveChanges();
                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadNhanVien();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                NhanVien nvToUpdate = context.NhanViens.FirstOrDefault(nv => nv.MaNhanVien == txtMaNV.Text);
                if (nvToUpdate != null)
                {
                    if (string.IsNullOrWhiteSpace(txtHoTen.Text))
                    {
                        MessageBox.Show("Tên nhân viên không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (cmbMaTo.SelectedValue == null || cmbChucVu.SelectedValue == null)
                    {
                        MessageBox.Show("Vui lòng chọn Tổ và Chức vụ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    nvToUpdate.HoTen = txtHoTen.Text.Trim();
                    nvToUpdate.NgaySinh = dtpNgaySinh.Value;
                    nvToUpdate.GioiTinh = cmbGioiTinh.SelectedItem.ToString();
                    nvToUpdate.MaTo = cmbMaTo.SelectedValue.ToString();
                    nvToUpdate.MaChucVu = cmbChucVu.SelectedValue.ToString();

                    context.SaveChanges();
                    MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadNhanVien();
                    ClearInputFields();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                NhanVien nvToDelete = context.NhanViens.FirstOrDefault(nv => nv.MaNhanVien == txtMaNV.Text);
                if (nvToDelete != null)
                {
                    if (MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên '{nvToDelete.HoTen}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        context.NhanViens.Remove(nvToDelete);
                        context.SaveChanges();
                        MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadNhanVien();
                        ClearInputFields();
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchText = txtTimKiem.Text.Trim().ToLower();
                var searchResults = context.NhanViens
                    .Where(nv => nv.MaNhanVien.ToLower().Contains(searchText) || nv.HoTen.ToLower().Contains(searchText))
                    .Include(nv => nv.ToNhanVien)
                    .Include(nv => nv.ChucVuNhanVien)
                    .ToList();
                BindGrid(searchResults);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
