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

namespace QuanLyChanNuoi
{
    public partial class FormQuanLyVatTu : Form
    {
        public FormQuanLyVatTu()
        {
            InitializeComponent();
        }

        private void FormQuanLyVatTu_Load(object sender, EventArgs e)
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
            using (LiveStockContextDB context = new LiveStockContextDB())
            {
                List<VatTu> listVattu = context.VatTus.ToList();
                BindGrid(listVattu);
            }
        }

        // Không cần thay đổi hàm này
        private void BindGrid(List<VatTu> listVattu)
        {
            dvgVattu.Rows.Clear();
            foreach (var item in listVattu)
            {
                int index = dvgVattu.Rows.Add();
                dvgVattu.Rows[index].Cells[0].Value = item.MaVatTu;
                dvgVattu.Rows[index].Cells[1].Value = item.TenVatTu;
                dvgVattu.Rows[index].Cells[2].Value = item.DonViTinh;
                dvgVattu.Rows[index].Cells[3].Value = item.SoLuong;
                // Trạng thái được tính toán dựa trên số lượng
                dvgVattu.Rows[index].Cells[4].Value = (item.SoLuong.HasValue && item.SoLuong > 0) ? "Còn hàng" : "Hết hàng";
            }
        }

        // XÓA: Phương thức LoadDonViTinhComboBox không còn cần thiết nữa

        // THAY ĐỔI: Cập nhật để xóa txtDonViTinh
        private void ClearInputFields()
        {
            txtMaVatTu.Text = "";
            txtTenVatTu.Text = "";
            txtDonViTinh.Text = ""; // Sửa từ ComboBox sang TextBox
            txtSoLuong.Text = "";
            txtMaVatTu.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                using (LiveStockContextDB context = new LiveStockContextDB())
                {
                    // Cải thiện kiểm tra dữ liệu đầu vào
                    if (string.IsNullOrWhiteSpace(txtMaVatTu.Text) ||
                        string.IsNullOrWhiteSpace(txtTenVatTu.Text) ||
                        string.IsNullOrWhiteSpace(txtDonViTinh.Text))
                    {
                        MessageBox.Show("Mã, Tên và Đơn vị tính không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (context.VatTus.Any(vt => vt.MaVatTu == txtMaVatTu.Text))
                    {
                        MessageBox.Show("Mã vật tư này đã tồn tại. Vui lòng nhập mã khác.", "Trùng mã vật tư", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(txtSoLuong.Text) || !int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong < 0)
                    {
                        MessageBox.Show("Số lượng phải là một số nguyên không âm.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    VatTu newVatTu = new VatTu
                    {
                        MaVatTu = txtMaVatTu.Text.Trim(),
                        TenVatTu = txtTenVatTu.Text.Trim(),
                        DonViTinh = txtDonViTinh.Text.Trim(), // THAY ĐỔI: Lấy dữ liệu từ TextBox
                        SoLuong = soLuong
                    };
                    newVatTu.TrangThai = newVatTu.SoLuong > 0 ? "Còn hàng" : "Hết hàng";

                    context.VatTus.Add(newVatTu);
                    context.SaveChanges();
                    MessageBox.Show("Thêm vật tư thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadDataIntoDataGridView();
                    ClearInputFields();
                }
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
                if (dvgVattu.SelectedRows.Count > 0)
                {
                    string maVatTuToUpdate = dvgVattu.SelectedRows[0].Cells[0].Value.ToString();

                    using (LiveStockContextDB context = new LiveStockContextDB())
                    {
                        VatTu vatTuToUpdate = context.VatTus.FirstOrDefault(vt => vt.MaVatTu == maVatTuToUpdate);
                        if (vatTuToUpdate != null)
                        {
                            // Kiểm tra dữ liệu đầu vào khi sửa
                            if (string.IsNullOrWhiteSpace(txtTenVatTu.Text) ||
                                string.IsNullOrWhiteSpace(txtDonViTinh.Text))
                            {
                                MessageBox.Show("Tên và Đơn vị tính không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (string.IsNullOrWhiteSpace(txtSoLuong.Text) || !int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong < 0)
                            {
                                MessageBox.Show("Số lượng phải là một số nguyên không âm.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            // Cập nhật thông tin
                            vatTuToUpdate.TenVatTu = txtTenVatTu.Text.Trim();
                            vatTuToUpdate.DonViTinh = txtDonViTinh.Text.Trim(); // THAY ĐỔI: Lấy dữ liệu từ TextBox
                            vatTuToUpdate.SoLuong = soLuong;
                            vatTuToUpdate.TrangThai = vatTuToUpdate.SoLuong > 0 ? "Còn hàng" : "Hết hàng";

                            context.SaveChanges();
                            MessageBox.Show("Cập nhật vật tư thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadDataIntoDataGridView();
                            ClearInputFields();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy vật tư để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một vật tư để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (dvgVattu.SelectedRows.Count > 0)
                {
                    string maVatTuToDelete = dvgVattu.SelectedRows[0].Cells[0].Value.ToString();

                    if (MessageBox.Show($"Bạn có chắc chắn muốn xóa vật tư có mã '{maVatTuToDelete}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (LiveStockContextDB context = new LiveStockContextDB())
                        {
                            VatTu vatTuToDelete = context.VatTus.FirstOrDefault(vt => vt.MaVatTu == maVatTuToDelete);
                            if (vatTuToDelete != null)
                            {
                                context.VatTus.Remove(vatTuToDelete);
                                context.SaveChanges();
                                MessageBox.Show("Xóa vật tư thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                LoadDataIntoDataGridView();
                                ClearInputFields();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy vật tư để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một vật tư để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa vật tư: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // THAY ĐỔI: Cập nhật để gán giá trị cho txtDonViTinh
        private void dvgVattu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dvgVattu.Rows[e.RowIndex];
                txtMaVatTu.Text = row.Cells[0].Value?.ToString();
                txtTenVatTu.Text = row.Cells[1].Value?.ToString();
                txtDonViTinh.Text = row.Cells[2].Value?.ToString(); // Sửa từ ComboBox sang TextBox
                txtSoLuong.Text = row.Cells[3].Value?.ToString();

                // Không cho phép sửa mã vật tư
                txtMaVatTu.Enabled = false;
            }
        }

        private void txtTimKiemVatTu_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (LiveStockContextDB context = new LiveStockContextDB())
                {
                    string searchText = txtTimKiemVatTu.Text.Trim().ToLower(); // Chuyển sang chữ thường để tìm kiếm không phân biệt hoa/thường
                    List<VatTu> searchResults = context.VatTus
                        .Where(vt => vt.MaVatTu.ToLower().Contains(searchText) || vt.TenVatTu.ToLower().Contains(searchText))
                        .ToList();
                    BindGrid(searchResults);
                }
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