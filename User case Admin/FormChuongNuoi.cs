using QuanLyChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyChanNuoi
{
    public partial class FormChuongNuoi : Form
    {
        private LiveStockContextDB db = new LiveStockContextDB();

        public FormChuongNuoi()
        {
            InitializeComponent();
        }

        private void FormChuongNuoi_Load(object sender, EventArgs e)
        {
            LoadChuong();
        }

        private void LoadChuong()
        {
            try
            {
                List<ChuongVatNuoi> danhSachChuong = db.ChuongVatNuois.ToList();
                BindGrid(danhSachChuong);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindGrid(List<ChuongVatNuoi> chuongVatNuois)
        {
            dgvChuongNuoi.Rows.Clear();
            foreach (var item in chuongVatNuois)
            {
                int index = dgvChuongNuoi.Rows.Add();
                dgvChuongNuoi.Rows[index].Cells[0].Value = item.MaChuong;
                dgvChuongNuoi.Rows[index].Cells[1].Value = item.ViTri;
                dgvChuongNuoi.Rows[index].Cells[2].Value = item.DienTich.HasValue ? item.DienTich.Value.ToString("N2") : "0.00";
            }
        }

        private void ClearInputFields()
        {
            txtMaChuong.Clear();
            txtViTri.Clear();
            txtDienTich.Clear();
            txtMaChuong.Enabled = true;
            txtMaChuong.Focus();
        }

        private void dgvChuongNuoi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvChuongNuoi.Rows[e.RowIndex];
                txtMaChuong.Text = row.Cells[0].Value?.ToString();
                txtViTri.Text = row.Cells[1].Value?.ToString();
                txtDienTich.Text = row.Cells[2].Value?.ToString();
                txtMaChuong.Enabled = false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaChuong.Text) || string.IsNullOrWhiteSpace(txtViTri.Text))
                {
                    MessageBox.Show("Mã chuồng và Vị trí không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (db.ChuongVatNuois.Any(c => c.MaChuong == txtMaChuong.Text))
                {
                    MessageBox.Show("Mã chuồng đã tồn tại.", "Trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtDienTich.Text, out decimal dienTich) || dienTich < 0)
                {
                    MessageBox.Show("Diện tích không hợp lệ. Phải là một số không âm.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ChuongVatNuoi chuong = new ChuongVatNuoi
                {
                    MaChuong = txtMaChuong.Text.Trim(),
                    ViTri = txtViTri.Text.Trim(),
                    DienTich = dienTich
                };

                db.ChuongVatNuois.Add(chuong);
                db.SaveChanges();
                MessageBox.Show("Thêm chuồng mới thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadChuong();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                var chuong = db.ChuongVatNuois.Find(txtMaChuong.Text);
                if (chuong == null)
                {
                    MessageBox.Show("Không tìm thấy chuồng để sửa. Vui lòng chọn một chuồng từ danh sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtDienTich.Text, out decimal dienTich) || dienTich < 0)
                {
                    MessageBox.Show("Diện tích không hợp lệ. Phải là một số không âm.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                chuong.ViTri = txtViTri.Text.Trim();
                chuong.DienTich = dienTich;

                db.SaveChanges();
                MessageBox.Show("Cập nhật thông tin thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadChuong();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                var chuong = db.ChuongVatNuois.Find(txtMaChuong.Text);
                if (chuong == null)
                {
                    MessageBox.Show("Không tìm thấy chuồng để xóa. Vui lòng chọn một chuồng từ danh sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show($"Bạn có chắc muốn xóa chuồng '{chuong.MaChuong}' không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    db.ChuongVatNuois.Remove(chuong);
                    db.SaveChanges();
                    MessageBox.Show("Xóa chuồng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadChuong();
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa chuồng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTimKiem.Text.Trim().ToLower();
                var searchResults = db.ChuongVatNuois
                                      .Where(c => c.MaChuong.ToLower().Contains(keyword) || c.ViTri.ToLower().Contains(keyword))
                                      .ToList();
                BindGrid(searchResults);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
