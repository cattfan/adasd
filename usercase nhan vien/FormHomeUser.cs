using QuanLyChanNuoi.usercase_nhan_vien;
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
    public partial class FormHomeUser : Form
    {
        public FormHomeUser()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void FormHomeUser_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn không nhận được nhắn tin nào cả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đã ở phiên bản mới nhất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form admin trước khi mở form chuồng nuôi
            FormChuongNuoi formChuongNuoi = new FormChuongNuoi();
            formChuongNuoi.FormClosed += (s, args) => this.Show(); // Hiển thị lại form admin khi form chuồng nuôi đóng
            formChuongNuoi.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form admin trước khi mở form báo cáo
            FormHoaDon formHoaDon = new FormHoaDon();
            formHoaDon.FormClosed += (s, args) => this.Show(); // Hiển thị lại form admin khi form báo cáo đóng
            formHoaDon.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form admin trước khi mở form thống kê
            FormQuanLyVatTuNV formVatTu = new FormQuanLyVatTuNV();
            formVatTu.FormClosed += (s, args) => this.Show(); // Hiển thị lại form admin khi form vật tư đóng
            formVatTu.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form admin trước khi mở form vật nuôi
            FormVatNuoiNV formvatnuoi = new FormVatNuoiNV();
            formvatnuoi.FormClosed += (s, args) => this.Show(); // Hiển thị lại form admin khi form nhà cung cấp đóng
            formvatnuoi.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close(); // Đóng form admin
            }
        }
    }
}
