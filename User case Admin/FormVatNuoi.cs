using QuanLyChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using QuanLyChanNuoi.User_case_Admin;

namespace QuanLyChanNuoi
{
    public partial class FormVatNuoi : Form
    {
        LiveStockContextDB context = new LiveStockContextDB();

        public FormVatNuoi()
        {
            InitializeComponent();
        }

        private void FormVatNuoi_Load(object sender, EventArgs e)
        {
            try
            {
                SetupDataGridView();
                LoadChuongDataIntoComboBox();
                LoadAllVatNuoi();
                SetInitialControlState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            dgvVatNuoi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVatNuoi.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dgvVatNuoi.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvVatNuoi.RowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvVatNuoi.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
        }

        private void SetInitialControlState()
        {
            txtMaVatNuoi.Enabled = false;
            nudSoLuong.Minimum = 1;
            nudSoLuong.Maximum = 10000;
        }

        private void BindGrid(List<VatNuoi> vatNuoiList)
        {
            dgvVatNuoi.Rows.Clear();
            foreach (var item in vatNuoiList)
            {
                int index = dgvVatNuoi.Rows.Add();
                dgvVatNuoi.Rows[index].Cells["MaVatNuoiColumn"].Value = item.MaVatNuoi;
                dgvVatNuoi.Rows[index].Cells["TenVatNuoiColumn"].Value = item.TenVatNuoi;
                dgvVatNuoi.Rows[index].Cells["NgayNhapColumn"].Value = item.NgayNhap?.ToString("dd/MM/yyyy");
                dgvVatNuoi.Rows[index].Cells["MaChuongColumn"].Value = item.MaChuong;
                dgvVatNuoi.Rows[index].Cells["SoLuongColumn"].Value = item.SoLuong;
            }
        }

        private void LoadAllVatNuoi()
        {
            var allVatNuoi = context.VatNuois.ToList();
            BindGrid(allVatNuoi);
        }

        private void LoadChuongDataIntoComboBox()
        {
            cboMaChuong.DataSource = context.ChuongVatNuois.ToList();
            cboMaChuong.DisplayMember = "TenChuong";
            cboMaChuong.ValueMember = "MaChuong";
            cboMaChuong.SelectedIndex = -1;
        }

        private void ClearInputFields()
        {
            txtMaVatNuoi.Text = "";
            txtTenVatNuoi.Text = "";
            dtNgayNhap.Value = DateTime.Now;
            cboMaChuong.SelectedIndex = -1;
            nudSoLuong.Value = 1;
            txtTenVatNuoi.Focus();
            errorProvider1.Clear();
        }

        private bool IsDataValid()
        {
            errorProvider1.Clear();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtTenVatNuoi.Text))
            {
                errorProvider1.SetError(txtTenVatNuoi, "Tên vật nuôi không được để trống.");
                isValid = false;
            }

            if (cboMaChuong.SelectedValue == null)
            {
                errorProvider1.SetError(cboMaChuong, "Vui lòng chọn chuồng.");
                isValid = false;
            }

            return isValid;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!IsDataValid())
            {
                return;
            }

            try
            {
                var newVatNuoi = new VatNuoi
                {
                    MaVatNuoi = "VN" + DateTime.Now.ToString("yyMMddHHmmss"),
                    TenVatNuoi = txtTenVatNuoi.Text,
                    NgayNhap = dtNgayNhap.Value,
                    SoLuong = (int)nudSoLuong.Value,
                    MaChuong = cboMaChuong.SelectedValue.ToString()
                };
                context.VatNuois.Add(newVatNuoi);
                context.SaveChanges();

                LoadAllVatNuoi();
                MessageBox.Show("Thêm mới vật nuôi thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm mới: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaVatNuoi.Text))
            {
                MessageBox.Show("Vui lòng chọn một vật nuôi từ danh sách để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsDataValid())
            {
                return;
            }

            try
            {
                string maVatNuoi = txtMaVatNuoi.Text;
                var dbUpdate = context.VatNuois.FirstOrDefault(p => p.MaVatNuoi == maVatNuoi);
                if (dbUpdate != null)
                {
                    dbUpdate.TenVatNuoi = txtTenVatNuoi.Text;
                    dbUpdate.NgayNhap = dtNgayNhap.Value;
                    dbUpdate.SoLuong = (int)nudSoLuong.Value;
                    dbUpdate.MaChuong = cboMaChuong.SelectedValue.ToString();

                    context.SaveChanges();

                    LoadAllVatNuoi();
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaVatNuoi.Text))
            {
                MessageBox.Show("Vui lòng chọn một vật nuôi để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa vật nuôi '{txtTenVatNuoi.Text}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    string maVatNuoi = txtMaVatNuoi.Text;
                    var dbVatNuoi = context.VatNuois.FirstOrDefault(p => p.MaVatNuoi == maVatNuoi);
                    if (dbVatNuoi != null)
                    {
                        context.VatNuois.Remove(dbVatNuoi);
                        context.SaveChanges();
                        LoadAllVatNuoi();
                        ClearInputFields();
                        MessageBox.Show("Xóa vật nuôi thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void dgvVatNuoi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvVatNuoi.Rows.Count)
            {
                DataGridViewRow row = dgvVatNuoi.Rows[e.RowIndex];

                if (row.Cells["MaVatNuoiColumn"].Value != null)
                {
                    txtMaVatNuoi.Text = row.Cells["MaVatNuoiColumn"].Value.ToString();
                    txtTenVatNuoi.Text = row.Cells["TenVatNuoiColumn"].Value.ToString();
                    if (DateTime.TryParse(row.Cells["NgayNhapColumn"].Value.ToString(), out DateTime ngayNhap))
                    {
                        dtNgayNhap.Value = ngayNhap;
                    }
                    cboMaChuong.SelectedValue = row.Cells["MaChuongColumn"].Value;
                    nudSoLuong.Value = Convert.ToDecimal(row.Cells["SoLuongColumn"].Value);
                }
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();
            var filteredList = context.VatNuois
                .Where(vn => vn.TenVatNuoi.ToLower().Contains(keyword) || vn.MaVatNuoi.ToLower().Contains(keyword))
                .ToList();
            BindGrid(filteredList);
        }

        private void btnTheoDoi_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaVatNuoi.Text))
            {
                string maVatNuoi = txtMaVatNuoi.Text;
                FormTheoDoiTangTruong formTheoDoi = new FormTheoDoiTangTruong(maVatNuoi, context);
                formTheoDoi.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một vật nuôi từ danh sách để theo dõi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
