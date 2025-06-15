using QuanLyChanNuoi.Models;
using QuanLyChanNuoi.User_case_Admin;
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
    public partial class FormHoaDon : Form
    {
        private LiveStockContextDB db = new LiveStockContextDB();
        private List<ChiTietHoaDon> danhSachChiTietHienTai;
        private ChiTietHoaDon selectedChiTiet;
        private static Random _random = new Random();
        public FormHoaDon()
        {
            InitializeComponent();
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormHoaDon_Load(object sender, EventArgs e)
        {
            danhSachChiTietHienTai = new List<ChiTietHoaDon>();
            LoadComboBoxData();
            BindGrid(danhSachChiTietHienTai);
        }
        private void LoadHoadon()
        {
            danhSachChiTietHienTai = new List<ChiTietHoaDon>();

            LoadComboBoxData();
            // Hiển thị bảng trống ban đầu
            BindGrid(danhSachChiTietHienTai);
        }
        private void LoadComboBoxData()
        {
            try
            {
                // Lấy danh sách từ DBAdd commentMore actions
                var danhsachVatTu = db.VatTus.ToList();
                var danhsachNCC = db.NhaCungCaps.ToList();
                // Đổ dữ liệu vào ComboBox Vật tu
                cbbtenvattu.DataSource = danhsachVatTu;
                cbbtenvattu.DisplayMember = "TenVatTu";
                cbbtenvattu.ValueMember = "MaVatTu";
                // Đổ dữ liệu vào ComboBox Nhà cung cấp
                cbbtennhacungcap.DataSource = danhsachNCC;
                cbbtennhacungcap.DisplayMember = "TenNhaCungCap";
                cbbtennhacungcap.ValueMember = "MaNhaCungCap";
                // Xóa lựa chọn mặc định ban đầu
                cbbtenvattu.SelectedIndex = -1;
                cbbtennhacungcap.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu vật tư/nhà cung cấp: " + ex.Message);
            }
        }
        private void BindGrid(List<ChiTietHoaDon> danhSach)
        {
            dvghoadon.Rows.Clear();
            if (danhSach == null) return;

            foreach (var item in danhSach)
            {
                int index = dvghoadon.Rows.Add();

                // Lấy tên trực tiếp từ thuộc tính tham chiếu

                dvghoadon.Rows[index].Cells[0].Value = item.VatTu.TenVatTu;
                dvghoadon.Rows[index].Cells[1].Value = item.NhaCungCap?.TenNhaCungCap;
                dvghoadon.Rows[index].Cells[2].Value = item.SoLuong;
                dvghoadon.Rows[index].Cells[3].Value = item.DonGia;
                dvghoadon.Rows[index].Cells[4].Value = (item.SoLuong ?? 0) * (item.DonGia ?? 0);
                dvghoadon.Rows[index].Tag = item;
            }
            UpdateTongTien();
        }
        private void dvghoadon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dvghoadon.Rows.Count)
            {
                selectedChiTiet = dvghoadon.Rows[e.RowIndex].Tag as ChiTietHoaDon;
                if (selectedChiTiet != null)
                {
                    cbbtenvattu.SelectedValue = selectedChiTiet.MaMatHang;
                    cbbtennhacungcap.SelectedValue = selectedChiTiet.MaNhaCungCap;

                    lbsoluong.Value = selectedChiTiet.SoLuong ?? 1;

                    txtdongia.Text = selectedChiTiet.DonGia?.ToString();
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbbtenvattu.SelectedValue == null || cbbtennhacungcap.SelectedValue == null || lbsoluong.Value <= 0 || string.IsNullOrWhiteSpace(txtdongia.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo");
                return;
            }
            if (!decimal.TryParse(txtdongia.Text, out decimal donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ.", "Lỗi");
                return;
            }
            var newDetail = new ChiTietHoaDon
            {
                MaMatHang = cbbtenvattu.SelectedValue as string,
                MaNhaCungCap = cbbtennhacungcap.SelectedValue as string,
                SoLuong = (int)lbsoluong.Value,
                DonGia = donGia,
                VatTu = cbbtenvattu.SelectedItem as VatTu,
                NhaCungCap = cbbtennhacungcap.SelectedItem as NhaCungCap,
                LoaiMatHang = null, // Set giá trị null tường minh
                MaNhanVien = null   // Set giá trị null tường minh
            };
            danhSachChiTietHienTai.Add(newDetail);
            BindGrid(danhSachChiTietHienTai);
            ClearInputFields();
        }
        private void ClearInputFields()
        {
            // Bỏ chọn trong các ComboBoxAdd commentMore actions
            cbbtenvattu.SelectedIndex = -1;
            cbbtennhacungcap.SelectedIndex = -1;
            // Xóa nội dung TextBox và reset NumericUpDown
            txtdongia.Clear();
            lbsoluong.Value = 0;
            // Quan trọng: Reset đối tượng đang chọn để tránh sửa/xóa nhầm
            selectedChiTiet = null;
            // Di chuyển con trỏ về ô nhập liệu đầu tiên để tiện cho việc nhập mới
            cbbtenvattu.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedChiTiet == null)
            {
                MessageBox.Show("Vui lòng chọn một mục để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtdongia.Text, out decimal donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            selectedChiTiet.LoaiMatHang = cbbtenvattu.SelectedValue as string;
            selectedChiTiet.VatTu = cbbtenvattu.SelectedItem as VatTu;
            selectedChiTiet.MaNhaCungCap = cbbtennhacungcap.SelectedValue as string;
            selectedChiTiet.SoLuong = (int)lbsoluong.Value;
            selectedChiTiet.DonGia = decimal.Parse(txtdongia.Text);
            BindGrid(danhSachChiTietHienTai);
            ClearInputFields();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedChiTiet == null)
            {
                MessageBox.Show("Vui lòng chọn một mục để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa mục này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                danhSachChiTietHienTai.Remove(selectedChiTiet);
                BindGrid(danhSachChiTietHienTai);
                ClearInputFields();
            }
        }
        private void UpdateTongTien()
        {
            decimal tongTien = danhSachChiTietHienTai.Sum(item => (item.SoLuong ?? 0) * (item.DonGia ?? 0));

            txttongthanhtien.Text = tongTien.ToString("N0");
        }

        private void btnxemhoadon_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form admin trước khi mở form vật nuôiAdd commentMore actions
            FormXemhoadon formxemhoadon = new FormXemhoadon();
            formxemhoadon.FormClosed += (s, args) => this.Show(); // Hiển thị lại form admin khi form nhà cung cấp đóng
            formxemhoadon.Show();
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            if (!danhSachChiTietHienTai.Any())
            {
                MessageBox.Show("Hóa đơn trống, không có gì để lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // 1. Tự động tạo một Mã Hóa Đơn mới và duy nhất
                string maHDMoi = TaoMaHoaDonTuDong();

                // 2. Tạo đối tượng Hóa Đơn (master) mới
                var hoaDonMoi = new HoaDon
                {
                    MaHoaDon = maHDMoi,
                    NgayLap = DateTime.Now,
                    // Khởi tạo collection để sẵn sàng nhận các chi tiết
                    ChiTietHoaDons = new List<ChiTietHoaDon>()
                };

                // 3. Lặp qua danh sách tạm và thêm từng chi tiết vào Hóa Đơn master
                int sttCounter = 1;
                foreach (var chiTietTam in danhSachChiTietHienTai)
                {
                    // Gán MaHoaDon và STT cho từng chi tiết
                    chiTietTam.MaHoaDon = maHDMoi;
                    chiTietTam.STT = sttCounter++;

                    // Gán null cho các thuộc tính tham chiếu không cần thiết khi lưu
                    chiTietTam.VatTu = null;
                    chiTietTam.NhaCungCap = null;
                    chiTietTam.NhanVien = null;

                    // THAY ĐỔI QUAN TRỌNG: Thêm chi tiết vào danh sách của Hóa Đơn
                    hoaDonMoi.ChiTietHoaDons.Add(chiTietTam);
                }

                // 4. CHỈ CẦN THÊM HÓA ĐƠN MASTER VÀO DBCONTEXT
                //    Entity Framework sẽ tự động thêm các chi tiết con đi kèm.
                db.HoaDons.Add(hoaDonMoi);

                // 5. Lưu tất cả thay đổi vào DB trong một giao dịch duy nhất
                db.SaveChanges();

                MessageBox.Show($"Lưu hóa đơn thành công!\nMã hóa đơn mới của bạn là: {maHDMoi}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 6. Dọn dẹp form
                danhSachChiTietHienTai.Clear();
                BindGrid(danhSachChiTietHienTai);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                // Bắt lỗi VALIDATION một cách chi tiết nhất
                var errorMessages = new List<string>();
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        // Lấy tên thuộc tính và thông báo lỗi cụ thể
                        string errorMessage = $"Thuộc tính '{validationError.PropertyName}' bị lỗi: {validationError.ErrorMessage}";
                        errorMessages.Add(errorMessage);
                    }
                }
                var fullErrorMessage = string.Join("\n", errorMessages);
                MessageBox.Show("Lỗi xác thực dữ liệu, vui lòng kiểm tra lại:\n\n" + fullErrorMessage, "Lỗi Validation");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException dbUpdateEx)
            {
                // Bắt lỗi từ DATABASE một cách chi tiết nhất
                Exception innerException = dbUpdateEx;
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                MessageBox.Show("Lỗi từ database khi đang lưu:\n\n" + innerException.Message, "Lỗi Database");
            }
            catch (Exception ex)
            {
                // Bắt các lỗi chung khác
                MessageBox.Show("Đã xảy ra một lỗi không xác định:\n\n" + ex.Message, "Lỗi Chung");
            }

        }
        private string TaoMaHoaDonTuDong()
        {
            string uniquePart = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
            return $"HD{uniquePart}";
        }
    }
}
