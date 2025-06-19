namespace QuanLyChanNuoi.User_case_Admin
{
    partial class FormXemhoadon
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormXemhoadon));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnQuayLai = new System.Windows.Forms.Button();
            this.dgvHoaDon = new System.Windows.Forms.DataGridView();
            this.MaHoaDonColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayLapColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenVatTuColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenNhaCungCapColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuongColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DonGiaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThanhTienColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.txtTimKiem);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(964, 63);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tìm kiếm hóa đơn";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimKiem.Location = new System.Drawing.Point(90, 26);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(860, 25);
            this.txtTimKiem.TabIndex = 18;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "Tìm kiếm:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnQuayLai);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(10, 501);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(964, 50);
            this.panel1.TabIndex = 17;
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuayLai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.btnQuayLai.FlatAppearance.BorderSize = 0;
            this.btnQuayLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuayLai.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuayLai.ForeColor = System.Drawing.Color.White;
            this.btnQuayLai.Location = new System.Drawing.Point(861, 8);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(100, 34);
            this.btnQuayLai.TabIndex = 20;
            this.btnQuayLai.Text = "Quay Lại";
            this.btnQuayLai.UseVisualStyleBackColor = false;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // dgvHoaDon
            // 
            this.dgvHoaDon.AllowUserToAddRows = false;
            this.dgvHoaDon.AllowUserToDeleteRows = false;
            this.dgvHoaDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoaDon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaHoaDonColumn,
            this.NgayLapColumn,
            this.TenVatTuColumn,
            this.TenNhaCungCapColumn,
            this.SoLuongColumn,
            this.DonGiaColumn,
            this.ThanhTienColumn});
            this.dgvHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHoaDon.Location = new System.Drawing.Point(10, 73);
            this.dgvHoaDon.Name = "dgvHoaDon";
            this.dgvHoaDon.ReadOnly = true;
            this.dgvHoaDon.RowHeadersWidth = 51;
            this.dgvHoaDon.Size = new System.Drawing.Size(964, 428);
            this.dgvHoaDon.TabIndex = 18;
            // 
            // MaHoaDonColumn
            // 
            this.MaHoaDonColumn.HeaderText = "Mã Hóa Đơn";
            this.MaHoaDonColumn.Name = "MaHoaDonColumn";
            this.MaHoaDonColumn.ReadOnly = true;
            // 
            // NgayLapColumn
            // 
            this.NgayLapColumn.HeaderText = "Ngày Lập";
            this.NgayLapColumn.Name = "NgayLapColumn";
            this.NgayLapColumn.ReadOnly = true;
            // 
            // TenVatTuColumn
            // 
            this.TenVatTuColumn.HeaderText = "Tên Vật Tư";
            this.TenVatTuColumn.Name = "TenVatTuColumn";
            this.TenVatTuColumn.ReadOnly = true;
            // 
            // TenNhaCungCapColumn
            // 
            this.TenNhaCungCapColumn.HeaderText = "Nhà Cung Cấp";
            this.TenNhaCungCapColumn.Name = "TenNhaCungCapColumn";
            this.TenNhaCungCapColumn.ReadOnly = true;
            // 
            // SoLuongColumn
            // 
            this.SoLuongColumn.HeaderText = "Số Lượng";
            this.SoLuongColumn.Name = "SoLuongColumn";
            this.SoLuongColumn.ReadOnly = true;
            // 
            // DonGiaColumn
            // 
            this.DonGiaColumn.HeaderText = "Đơn Giá";
            this.DonGiaColumn.Name = "DonGiaColumn";
            this.DonGiaColumn.ReadOnly = true;
            // 
            // ThanhTienColumn
            // 
            this.ThanhTienColumn.HeaderText = "Thành Tiền";
            this.ThanhTienColumn.Name = "ThanhTienColumn";
            this.ThanhTienColumn.ReadOnly = true;
            // 
            // FormXemhoadon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.dgvHoaDon);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormXemhoadon";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xem Lịch Sử Hóa Đơn";
            this.Load += new System.EventHandler(this.FormXemhoadon_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnQuayLai;
        private System.Windows.Forms.DataGridView dgvHoaDon;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaHoaDonColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayLapColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenVatTuColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenNhaCungCapColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLuongColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DonGiaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThanhTienColumn;
    }
}
