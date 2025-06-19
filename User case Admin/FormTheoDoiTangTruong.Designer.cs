namespace QuanLyChanNuoi.User_case_Admin
{
    partial class FormTheoDoiTangTruong
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTheoDoiTangTruong));
            this.panelInfo = new System.Windows.Forms.Panel();
            this.lblTenVatNuoi = new System.Windows.Forms.Label();
            this.lblMaVatNuoi = new System.Windows.Forms.Label();
            this.groupBoxUpdate = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelUpdate = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpNgayKiemTra = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.numSoLuongMau = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTongCanNangMau = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCanNangTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numSoLuongThucTe = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.flowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.groupBoxHistory = new System.Windows.Forms.GroupBox();
            this.dgvLichSu = new System.Windows.Forms.DataGridView();
            this.panelInfo.SuspendLayout();
            this.groupBoxUpdate.SuspendLayout();
            this.tableLayoutPanelUpdate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongMau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongThucTe)).BeginInit();
            this.flowLayoutPanelButtons.SuspendLayout();
            this.groupBoxHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).BeginInit();
            this.SuspendLayout();
            // 
            // panelInfo
            // 
            this.panelInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(147)))), ((int)(((byte)(95)))));
            this.panelInfo.Controls.Add(this.lblTenVatNuoi);
            this.panelInfo.Controls.Add(this.lblMaVatNuoi);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInfo.Location = new System.Drawing.Point(10, 10);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(964, 60);
            this.panelInfo.TabIndex = 0;
            // 
            // lblTenVatNuoi
            // 
            this.lblTenVatNuoi.AutoSize = true;
            this.lblTenVatNuoi.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenVatNuoi.ForeColor = System.Drawing.Color.White;
            this.lblTenVatNuoi.Location = new System.Drawing.Point(423, 17);
            this.lblTenVatNuoi.Name = "lblTenVatNuoi";
            this.lblTenVatNuoi.Size = new System.Drawing.Size(117, 25);
            this.lblTenVatNuoi.TabIndex = 1;
            this.lblTenVatNuoi.Text = "Tên Lô: ...";
            // 
            // lblMaVatNuoi
            // 
            this.lblMaVatNuoi.AutoSize = true;
            this.lblMaVatNuoi.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaVatNuoi.ForeColor = System.Drawing.Color.White;
            this.lblMaVatNuoi.Location = new System.Drawing.Point(18, 17);
            this.lblMaVatNuoi.Name = "lblMaVatNuoi";
            this.lblMaVatNuoi.Size = new System.Drawing.Size(111, 25);
            this.lblMaVatNuoi.TabIndex = 0;
            this.lblMaVatNuoi.Text = "Mã Lô: ...";
            // 
            // groupBoxUpdate
            // 
            this.groupBoxUpdate.Controls.Add(this.tableLayoutPanelUpdate);
            this.groupBoxUpdate.Controls.Add(this.flowLayoutPanelButtons);
            this.groupBoxUpdate.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxUpdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxUpdate.Location = new System.Drawing.Point(10, 70);
            this.groupBoxUpdate.Name = "groupBoxUpdate";
            this.groupBoxUpdate.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxUpdate.Size = new System.Drawing.Size(964, 250);
            this.groupBoxUpdate.TabIndex = 1;
            this.groupBoxUpdate.TabStop = false;
            this.groupBoxUpdate.Text = "Cập nhật thông tin tăng trưởng";
            // 
            // tableLayoutPanelUpdate
            // 
            this.tableLayoutPanelUpdate.ColumnCount = 2;
            this.tableLayoutPanelUpdate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelUpdate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelUpdate.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanelUpdate.Controls.Add(this.dtpNgayKiemTra, 1, 0);
            this.tableLayoutPanelUpdate.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanelUpdate.Controls.Add(this.numSoLuongMau, 1, 1);
            this.tableLayoutPanelUpdate.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanelUpdate.Controls.Add(this.txtTongCanNangMau, 1, 2);
            this.tableLayoutPanelUpdate.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanelUpdate.Controls.Add(this.txtCanNangTB, 1, 3);
            this.tableLayoutPanelUpdate.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanelUpdate.Controls.Add(this.numSoLuongThucTe, 1, 4);
            this.tableLayoutPanelUpdate.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanelUpdate.Controls.Add(this.txtGhiChu, 1, 5);
            this.tableLayoutPanelUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelUpdate.Location = new System.Drawing.Point(10, 28);
            this.tableLayoutPanelUpdate.Name = "tableLayoutPanelUpdate";
            this.tableLayoutPanelUpdate.RowCount = 6;
            this.tableLayoutPanelUpdate.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelUpdate.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelUpdate.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelUpdate.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelUpdate.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelUpdate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelUpdate.Size = new System.Drawing.Size(944, 172);
            this.tableLayoutPanelUpdate.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ngày kiểm tra:";
            // 
            // dtpNgayKiemTra
            // 
            this.dtpNgayKiemTra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpNgayKiemTra.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayKiemTra.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayKiemTra.Location = new System.Drawing.Point(179, 3);
            this.dtpNgayKiemTra.Name = "dtpNgayKiemTra";
            this.dtpNgayKiemTra.Size = new System.Drawing.Size(762, 25);
            this.dtpNgayKiemTra.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Số lượng mẫu:";
            // 
            // numSoLuongMau
            // 
            this.numSoLuongMau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numSoLuongMau.Location = new System.Drawing.Point(179, 34);
            this.numSoLuongMau.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            this.numSoLuongMau.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numSoLuongMau.Name = "numSoLuongMau";
            this.numSoLuongMau.Size = new System.Drawing.Size(762, 25);
            this.numSoLuongMau.TabIndex = 3;
            this.numSoLuongMau.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tổng cân nặng mẫu (kg):";
            // 
            // txtTongCanNangMau
            // 
            this.txtTongCanNangMau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTongCanNangMau.Location = new System.Drawing.Point(179, 65);
            this.txtTongCanNangMau.Name = "txtTongCanNangMau";
            this.txtTongCanNangMau.Size = new System.Drawing.Size(762, 25);
            this.txtTongCanNangMau.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Cân nặng TB (kg):";
            // 
            // txtCanNangTB
            // 
            this.txtCanNangTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCanNangTB.Location = new System.Drawing.Point(179, 96);
            this.txtCanNangTB.Name = "txtCanNangTB";
            this.txtCanNangTB.ReadOnly = true;
            this.txtCanNangTB.Size = new System.Drawing.Size(762, 25);
            this.txtCanNangTB.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Số lượng thực tế:";
            // 
            // numSoLuongThucTe
            // 
            this.numSoLuongThucTe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numSoLuongThucTe.Location = new System.Drawing.Point(179, 127);
            this.numSoLuongThucTe.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.numSoLuongThucTe.Name = "numSoLuongThucTe";
            this.numSoLuongThucTe.Size = new System.Drawing.Size(762, 25);
            this.numSoLuongThucTe.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Ghi chú:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu.Location = new System.Drawing.Point(179, 158);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(762, 11);
            this.txtGhiChu.TabIndex = 11;
            // 
            // flowLayoutPanelButtons
            // 
            this.flowLayoutPanelButtons.Controls.Add(this.btnThoat);
            this.flowLayoutPanelButtons.Controls.Add(this.btnXoa);
            this.flowLayoutPanelButtons.Controls.Add(this.btnThem);
            this.flowLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanelButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelButtons.Location = new System.Drawing.Point(10, 200);
            this.flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            this.flowLayoutPanelButtons.Size = new System.Drawing.Size(944, 40);
            this.flowLayoutPanelButtons.TabIndex = 1;
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.btnThoat.FlatAppearance.BorderSize = 0;
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Location = new System.Drawing.Point(851, 3);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(90, 34);
            this.btnThoat.TabIndex = 14;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(57)))), ((int)(((byte)(53)))));
            this.btnXoa.FlatAppearance.BorderSize = 0;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(755, 3);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(90, 34);
            this.btnXoa.TabIndex = 13;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(160)))), ((int)(((byte)(71)))));
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(659, 3);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(90, 34);
            this.btnThem.TabIndex = 12;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // groupBoxHistory
            // 
            this.groupBoxHistory.Controls.Add(this.dgvLichSu);
            this.groupBoxHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxHistory.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxHistory.Location = new System.Drawing.Point(10, 320);
            this.groupBoxHistory.Name = "groupBoxHistory";
            this.groupBoxHistory.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxHistory.Size = new System.Drawing.Size(964, 231);
            this.groupBoxHistory.TabIndex = 2;
            this.groupBoxHistory.TabStop = false;
            this.groupBoxHistory.Text = "Lịch sử theo dõi";
            // 
            // dgvLichSu
            // 
            this.dgvLichSu.AllowUserToAddRows = false;
            this.dgvLichSu.AllowUserToDeleteRows = false;
            this.dgvLichSu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLichSu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLichSu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLichSu.Location = new System.Drawing.Point(10, 28);
            this.dgvLichSu.Name = "dgvLichSu";
            this.dgvLichSu.ReadOnly = true;
            this.dgvLichSu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLichSu.Size = new System.Drawing.Size(944, 193);
            this.dgvLichSu.TabIndex = 0;
            // 
            // FormTheoDoiTangTruong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.groupBoxHistory);
            this.Controls.Add(this.groupBoxUpdate);
            this.Controls.Add(this.panelInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormTheoDoiTangTruong";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Theo dõi tăng trưởng";
            this.Load += new System.EventHandler(this.FormTheoDoiTangTruong_Load);
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.groupBoxUpdate.ResumeLayout(false);
            this.tableLayoutPanelUpdate.ResumeLayout(false);
            this.tableLayoutPanelUpdate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongMau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongThucTe)).EndInit();
            this.flowLayoutPanelButtons.ResumeLayout(false);
            this.groupBoxHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label lblTenVatNuoi;
        private System.Windows.Forms.Label lblMaVatNuoi;
        private System.Windows.Forms.GroupBox groupBoxUpdate;
        private System.Windows.Forms.GroupBox groupBoxHistory;
        private System.Windows.Forms.DataGridView dgvLichSu;
        private System.Windows.Forms.DateTimePicker dtpNgayKiemTra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numSoLuongMau;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCanNangTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTongCanNangMau;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numSoLuongThucTe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelUpdate;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtons;
    }
}
