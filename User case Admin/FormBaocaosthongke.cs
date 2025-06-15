using Microsoft.Reporting.WinForms;
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

namespace QuanLyChanNuoi.User_case_Admin
{
    public partial class FormBaocaosthongke : Form
    {
        public FormBaocaosthongke()
        {
            InitializeComponent();
        }

        private void FormBaocaosthongke_Load(object sender, EventArgs e)
        {
            try
            {
                using (var db = new LiveStockContextDB())
                {
                    var data = new thongkeReport();

                    // 2. Gán giá trị vào các thuộc tính đã được đổi tên cho chính xác
                    data.Nhanvien = db.NhanViens.Count();
                    data.Vatnuoi = db.VatNuois.Count();
                    data.Chuongnuoi = db.ChuongVatNuois.Count();
                    data.Vattu = db.VatTus.Count();
                    data.Nhacungcap = db.NhaCungCaps.Count();
                    data.Hoadon = db.HoaDons.Count();

                    // 3. Report cần một danh sách, dù chỉ có một dòng dữ liệu
                    var reportDataList = new List<thongkeReport> { data };

                    // 4. Tạo ReportDataSource
                    // Tên "DataManagement" phải khớp với tên DataSet trong file .rdlc
                    var rds = new ReportDataSource("DataSetthongke", reportDataList);

                    // 5. Gán DataSource cho ReportViewer
                    // Đảm bảo đường dẫn "FormBaoCao.rdlc" là chính xác
                    // và file đã được set "Copy to Output Directory" thành "Copy if newer"
                    this.reportViewer1.LocalReport.ReportPath = "Reportthongke.rdlc";
                    this.reportViewer1.LocalReport.DataSources.Clear();
                    this.reportViewer1.LocalReport.DataSources.Add(rds);
                    this.reportViewer1.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                // Thêm try-catch để bắt lỗi nếu có, giúp gỡ rối dễ hơn
                MessageBox.Show("Đã xảy ra lỗi khi tạo báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer2_Load(object sender, EventArgs e)
        {

        }
    }
}
