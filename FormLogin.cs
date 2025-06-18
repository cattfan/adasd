using QuanLyChanNuoi.User_case_Admin;
using QuanLyChanNuoi.usercase_nhan_vien;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace QuanLyChanNuoi
{
    public partial class FormLogin : Form
    {
        private Dictionary<string, (string password, string role)> taiKhoans = new Dictionary<string, (string, string)>
        {
            { "admin", ("admin123", "Admin") },
            { "nhanvien", ("123456", "NhanVien") }
        };

        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtTenDangNhap.Text.Trim();
            string password = txtMatKhau.Text.Trim();

            if (taiKhoans.ContainsKey(username))
            {
                var account = taiKhoans[username];
                if (account.password == password)
                {
                    this.Hide();

                    if (account.role == "Admin")
                    {
                        FormHomeAdmin adminForm = new FormHomeAdmin();
                        adminForm.FormClosed += (s, args) => this.Show();
                        adminForm.Show();
                    }
                    else if (account.role == "NhanVien")
                    {
                        FormHomeUser nvForm = new FormHomeUser();
                        nvForm.FormClosed += (s, args) => this.Show();
                        nvForm.Show();
                    }

                    txtMatKhau.Clear();
                    txtTenDangNhap.Clear();
                    chkShowPassword.Checked = false;
                    return;
                }
            }

            MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtMatKhau.Clear();
            txtTenDangNhap.Clear();
            chkShowPassword.Checked = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkShowPassword.Checked;
        }
    }
}