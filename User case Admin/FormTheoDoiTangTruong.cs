using QuanLyChanNuoi.Models;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyChanNuoi.User_case_Admin
{
    public partial class FormTheoDoiTangTruong : Form
    {
        private readonly string _maVatNuoi;
        private readonly LiveStockContextDB _context;

        public FormTheoDoiTangTruong(string maVatNuoi, LiveStockContextDB dbContext)
        {
            InitializeComponent();
            _maVatNuoi = maVatNuoi;
            _context = dbContext;
        }

        private void FormTheoDoiTangTruong_Load(object sender, EventArgs e)
        {
            try
            {
                LoadThongTinLo();
                BindGrid();
                AddEventHandlers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi");
            }
        }

        private void LoadThongTinLo()
        {
            var vatNuoi = _context.VatNuois.FirstOrDefault(v => v.MaVatNuoi == _maVatNuoi);
            if (vatNuoi != null)
            {
                lblMaVatNuoi.Text = "Mã Lô: " + vatNuoi.MaVatNuoi;
                lblTenVatNuoi.Text = "Tên Lô: " + vatNuoi.TenVatNuoi;
                numSoLuongThucTe.Value = vatNuoi.SoLuong ?? 0;
            }
        }

        private void BindGrid()
        {
            dgvLichSu.DataSource = null;
            var history = _context.LichSuTangTruongs
                                  .Where(h => h.MaVatNuoi == _maVatNuoi)
                                  .OrderByDescending(h => h.NgayKiemTra)
                                  .ToList();
            dgvLichSu.DataSource = history;
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dgvLichSu.Columns.Count > 0)
            {
                dgvLichSu.Columns["ID"].Visible = false; // Ẩn cột ID
                dgvLichSu.Columns["NgayKiemTra"].HeaderText = "Ngày Kiểm Tra";
                dgvLichSu.Columns["SoLuongMau"].HeaderText = "SL Mẫu";
                dgvLichSu.Columns["TongCanNangMau"].HeaderText = "Tổng cân nặng";
                dgvLichSu.Columns["CanNangTrungBinhMau"].HeaderText = "Cân Nặng TB (kg)";
                dgvLichSu.Columns["SoLuongThucTeTrongDan"].HeaderText = "SL Đàn";
                dgvLichSu.Columns["TongTrongLuongUocTinh"].HeaderText = "Tổng KG Ước Tính";
                dgvLichSu.Columns["GhiChu"].HeaderText = "Ghi Chú";
            }
        }

        private void AddEventHandlers()
        {
            txtTongCanNangMau.TextChanged += TinhCanNangTrungBinh;
            numSoLuongMau.ValueChanged += TinhCanNangTrungBinh;
            btnThem.Click += btnThem_Click;
            btnXoa.Click += btnXoa_Click;
            dgvLichSu.CellClick += dgvLichSu_CellClick;
        }

        private void TinhCanNangTrungBinh(object sender, EventArgs e)
        {
            double.TryParse(txtTongCanNangMau.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double tongCanNang);
            int soLuongMau = (int)numSoLuongMau.Value;
            txtCanNangTB.Text = (soLuongMau > 0 && tongCanNang > 0) ? (tongCanNang / soLuongMau).ToString("0.##") : "0";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (numSoLuongMau.Value <= 0 || string.IsNullOrWhiteSpace(txtTongCanNangMau.Text))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin Số lượng mẫu và Tổng cân nặng.", "Cảnh báo");
                    return;
                }

                double tongCanNang;
                // Đọc giá trị từ textbox một cách an toàn, chấp nhận cả dấu '.' và ','
                if (!double.TryParse(txtTongCanNangMau.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out tongCanNang) &&
                    !double.TryParse(txtTongCanNangMau.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out tongCanNang))
                {
                    MessageBox.Show("Tổng cân nặng không hợp lệ. Vui lòng kiểm tra lại.", "Lỗi");
                    return;
                }

                int soLuongMau = (int)numSoLuongMau.Value;
                double canNangTB = (soLuongMau > 0) ? tongCanNang / soLuongMau : 0;

                var newRecord = new LichSuTangTruong
                {
                    MaVatNuoi = _maVatNuoi,
                    NgayKiemTra = dtpNgayKiemTra.Value.Date,
                    SoLuongMau = soLuongMau,
                    TongCanNangMau = tongCanNang,
                    CanNangTrungBinhMau = canNangTB,
                    SoLuongThucTeTrongDan = (int)numSoLuongThucTe.Value,
                    GhiChu = txtGhiChu.Text
                };
                newRecord.TongTrongLuongUocTinh = newRecord.CanNangTrungBinhMau * newRecord.SoLuongThucTeTrongDan;

                _context.LichSuTangTruongs.Add(newRecord);
                _context.SaveChanges();

                MessageBox.Show("Thêm thành công!", "Thông báo");
                BindGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.InnerException?.Message ?? ex.Message, "Lỗi");
            }
            ClearInputFields();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvLichSu.CurrentRow == null) return;
            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int id = (int)dgvLichSu.CurrentRow.Cells["ID"].Value;
                var record = _context.LichSuTangTruongs.Find(id);
                if (record != null)
                {
                    _context.LichSuTangTruongs.Remove(record);
                    _context.SaveChanges();
                    BindGrid();
                }
            }
            ClearInputFields();
        }

        private void dgvLichSu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int id = (int)dgvLichSu.Rows[e.RowIndex].Cells["ID"].Value;
            var record = _context.LichSuTangTruongs.Find(id);
            if (record != null)
            {
                dtpNgayKiemTra.Value = record.NgayKiemTra;
                numSoLuongMau.Value = record.SoLuongMau ?? 1;
                txtTongCanNangMau.Text = record.TongCanNangMau?.ToString();
                numSoLuongThucTe.Value = record.SoLuongThucTeTrongDan ?? 0;
                txtGhiChu.Text = record.GhiChu;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void ClearInputFields()
        {
            // Xóa lựa chọn trên DataGridView
            dgvLichSu.ClearSelection();

            // Reset các control nhập liệu về giá trị mặc định
            dtpNgayKiemTra.Value = DateTime.Now;
            numSoLuongMau.Value = 1; // Hoặc giá trị mặc định bạn muốn
            txtTongCanNangMau.Clear();
            txtGhiChu.Clear();

            // Cập nhật lại số lượng thực tế từ thông tin lô ban đầu
            var vatNuoi = _context.VatNuois.FirstOrDefault(v => v.MaVatNuoi == _maVatNuoi);
            if (vatNuoi != null)
            {
                numSoLuongThucTe.Value = vatNuoi.SoLuong ?? 0;
            }

            // Đưa con trỏ về ô nhập liệu đầu tiên
            dtpNgayKiemTra.Focus();
        }
    }
}
