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
        private LiveStockContextDB db = new LiveStockContextDB();
        public FormVatNuoi()
        {
            InitializeComponent();
        }

        private void FormVatNuoi_Load(object sender, EventArgs e)
        {
            LoadVatNuoi();
            LoadComboBoxes();
            ClearInputFields();
            StyleDataGridView();
        }

        private void StyleDataGridView()
        {
            dgvVatNuoi.RowHeadersVisible = false;
            dgvVatNuoi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVatNuoi.AllowUserToAddRows = false;
            dgvVatNuoi.AllowUserToDeleteRows = false;
            dgvVatNuoi.ReadOnly = true;

            dgvVatNuoi.EnableHeadersVisualStyles = false;
            dgvVatNuoi.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvVatNuoi.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvVatNuoi.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvVatNuoi.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvVatNuoi.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvVatNuoi.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvVatNuoi.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvVatNuoi.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvVatNuoi.BackgroundColor = Color.White;

            dgvVatNuoi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadVatNuoi()
        {
            try
            {
                var dsVatNuoi = db.VatNuois.ToList();
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
                cbMaChuong.DataSource = db.ChuongVatNuois.ToList();
                cbMaChuong.DisplayMember = "MaChuong";
                cbMaChuong.ValueMember = "MaChuong";
                cbMaChuong.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Chuồng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                dgvVatNuoi.Rows[index].Cells[2].Value = vn.NgayNhap?.ToString("dd/MM/yyyy");
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
                if (DateTime.TryParse(row.Cells[2].Value?.ToString(), out var date))
                {
                    dtNgayNhap.Value = date;
                }
                cbMaChuong.SelectedValue = row.Cells[3].Value ?? -1;
                txtSoLuong.Text = row.Cells[4].Value?.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // TODO: Add logic for Add button here
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaVatNuoi.Text))
            {
                MessageBox.Show("Vui lòng chọn một vật nuôi để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Add other update logic here
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvVatNuoi.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một Vật nuôi để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maVN = dgvVatNuoi.SelectedRows[0].Cells[0].Value?.ToString();
            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa Vật nuôi '{maVN}' không? Mọi dữ liệu liên quan cũng sẽ bị xóa.", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    VatNuoi vnToDelete = db.VatNuois
                                          .Include(v => v.LichSuTangTruongs)
                                          .Include(v => v.NhanViens)
                                          .SingleOrDefault(v => v.MaVatNuoi == maVN);

                    if (vnToDelete != null)
                    {
                        vnToDelete.LichSuTangTruongs.Clear();
                        vnToDelete.NhanViens.Clear();
                        var relatedLogs = db.Log_LichSuChuong.Where(log => log.MaVatNuoi == maVN).ToList();
                        if (relatedLogs.Any())
                        {
                            db.Log_LichSuChuong.RemoveRange(relatedLogs);
                        }
                        db.VatNuois.Remove(vnToDelete);
                        db.SaveChanges();

                        MessageBox.Show("Xóa Vật nuôi và tất cả dữ liệu liên quan thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadVatNuoi();
                        ClearInputFields();
                    }
                }
                catch (Exception ex)
                {
                    string errorMessage = "Lỗi khi xóa Vật nuôi: " + ex.Message;
                    if (ex.InnerException != null)
                    {
                        errorMessage += "\n\nChi tiết: " + ex.InnerException.Message;
                    }
                    MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTheoDoi_Click(object sender, EventArgs e)
        {
            if (dgvVatNuoi.CurrentRow != null)
            {
                string maVatNuoiDuocChon = dgvVatNuoi.CurrentRow.Cells[0].Value.ToString();
                FormTheoDoiTangTruong formTheoDoi = new FormTheoDoiTangTruong(maVatNuoiDuocChon, db);
                formTheoDoi.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một lô vật nuôi trong danh sách để theo dõi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                              vn.TenVatNuoi.ToLower().Contains(keyword))
                                 .ToList();
            BindGrid(filteredList);
        }
    }
}