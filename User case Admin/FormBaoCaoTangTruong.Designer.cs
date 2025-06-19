namespace QuanLyChanNuoi.User_case_Admin
{
    partial class FormBaoCaoTangTruong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBaoCaoTangTruong));
            this.panelControl = new System.Windows.Forms.Panel();
            this.btnXemBaoCao = new System.Windows.Forms.Button();
            this.cbChonVatNuoi = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panelControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.BackColor = System.Drawing.Color.White;
            this.panelControl.Controls.Add(this.btnXemBaoCao);
            this.panelControl.Controls.Add(this.cbChonVatNuoi);
            this.panelControl.Controls.Add(this.label1);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(800, 60);
            this.panelControl.TabIndex = 0;
            // 
            // btnXemBaoCao
            // 
            this.btnXemBaoCao.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemBaoCao.Location = new System.Drawing.Point(440, 15);
            this.btnXemBaoCao.Name = "btnXemBaoCao";
            this.btnXemBaoCao.Size = new System.Drawing.Size(121, 28);
            this.btnXemBaoCao.TabIndex = 2;
            this.btnXemBaoCao.Text = "Xem Báo Cáo";
            this.btnXemBaoCao.UseVisualStyleBackColor = true;
            this.btnXemBaoCao.Click += new System.EventHandler(this.btnXemBaoCao_Click);
            // 
            // cbChonVatNuoi
            // 
            this.cbChonVatNuoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChonVatNuoi.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbChonVatNuoi.FormattingEnabled = true;
            this.cbChonVatNuoi.Location = new System.Drawing.Point(125, 16);
            this.cbChonVatNuoi.Name = "cbChonVatNuoi";
            this.cbChonVatNuoi.Size = new System.Drawing.Size(299, 25);
            this.cbChonVatNuoi.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chọn vật nuôi:";
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 60);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 390);
            this.reportViewer1.TabIndex = 1;
            // 
            // FormBaoCaoTangTruong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.panelControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormBaoCaoTangTruong";
            this.Text = "Báo Cáo Tăng Trưởng Vật Nuôi";
            this.Load += new System.EventHandler(this.FormBaoCaoTangTruong_Load);
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.Button btnXemBaoCao;
        private System.Windows.Forms.ComboBox cbChonVatNuoi;
        private System.Windows.Forms.Label label1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}
