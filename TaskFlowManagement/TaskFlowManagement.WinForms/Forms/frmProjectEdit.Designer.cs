namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmProjectEdit
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelHeader     = new Panel();
            this.lblTitleForm    = new Label();
            this.panelAccentLine = new Panel();
            this.panelBody       = new Panel();
            this.panelFooter     = new Panel();
            this.panelFooterLine = new Panel();
            this.lblError        = new Label();
            this.btnSave         = new Button();
            this.btnCancel       = new Button();

            // Body controls
            this.lblName     = new Label(); this.txtName        = new TextBox();
            this.lblCustomer = new Label(); this.cboCustomer    = new ComboBox();
            this.lblOwner    = new Label(); this.cboOwner       = new ComboBox();
            this.lblStart    = new Label(); this.dtpStartDate   = new DateTimePicker();
            this.lblDeadline = new Label(); this.dtpDeadline    = new DateTimePicker();
            this.chkDeadline = new CheckBox();
            this.lblBudget   = new Label(); this.txtBudget      = new TextBox();
            this.lblPriority = new Label(); this.cboPriority    = new ComboBox();
            this.lblDesc     = new Label(); this.txtDescription = new TextBox();

            this.panelHeader.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();

            // ── Header ───────────────────────────────────────
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Height = 64;
            this.panelHeader.Controls.Add(this.lblTitleForm);
            this.panelHeader.Controls.Add(this.panelAccentLine);

            this.lblTitleForm.AutoSize = false;
            this.lblTitleForm.Dock = DockStyle.Fill;
            this.lblTitleForm.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitleForm.ForeColor = System.Drawing.Color.White;
            this.lblTitleForm.Padding = new Padding(18, 0, 0, 4);
            this.lblTitleForm.Text = "➕  Tạo dự án mới";
            this.lblTitleForm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.panelAccentLine.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.panelAccentLine.Dock = DockStyle.Bottom;
            this.panelAccentLine.Height = 4;

            // ── Body ─────────────────────────────────────────
            // Spacing: label Y mỗi field cách 60px (compact hơn vì nhiều field)
            this.panelBody.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.panelBody.Dock = DockStyle.Fill;
            this.panelBody.AutoScroll = true;
            this.panelBody.Controls.AddRange(new Control[] {
                lblName, txtName, lblCustomer, cboCustomer, lblOwner, cboOwner,
                lblStart, dtpStartDate, lblDeadline, chkDeadline, dtpDeadline,
                lblBudget, txtBudget, lblPriority, cboPriority,
                lblDesc, txtDescription
            });

            int y = 14; int gap = 56; int lx = 20; int tw = 400;

            // Tên dự án
            SetLabel(lblName, "TÊN DỰ ÁN *", lx, y);
            SetTextBox(txtName, "Nhập tên dự án...", lx, y + 18, tw, 1);

            // Khách hàng
            y += gap;
            SetLabel(lblCustomer, "KHÁCH HÀNG", lx, y);
            this.cboCustomer.BackColor = System.Drawing.Color.White;
            this.cboCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboCustomer.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboCustomer.Location = new System.Drawing.Point(lx, y + 18);
            this.cboCustomer.Size = new System.Drawing.Size(tw, 30);
            this.cboCustomer.TabIndex = 2;

            // Quản lý (PM)
            y += gap;
            SetLabel(lblOwner, "QUẢN LÝ DỰ ÁN (PM) *", lx, y);
            this.cboOwner.BackColor = System.Drawing.Color.White;
            this.cboOwner.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboOwner.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboOwner.Location = new System.Drawing.Point(lx, y + 18);
            this.cboOwner.Size = new System.Drawing.Size(tw, 30);
            this.cboOwner.TabIndex = 3;

            // Ngày bắt đầu + Deadline (cùng hàng)
            y += gap;
            SetLabel(lblStart, "NGÀY BẮT ĐẦU", lx, y);
            this.dtpStartDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpStartDate.Format = DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(lx, y + 18);
            this.dtpStartDate.Size = new System.Drawing.Size(190, 30);
            this.dtpStartDate.TabIndex = 4;

            SetLabel(lblDeadline, "DEADLINE", lx + 210, y);
            this.chkDeadline.AutoSize = true;
            this.chkDeadline.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.chkDeadline.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.chkDeadline.Location = new System.Drawing.Point(lx + 290, y + 1);
            this.chkDeadline.Text = "Có deadline";
            this.chkDeadline.CheckedChanged += chkDeadline_CheckedChanged;

            this.dtpDeadline.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDeadline.Format = DateTimePickerFormat.Short;
            this.dtpDeadline.Location = new System.Drawing.Point(lx + 210, y + 18);
            this.dtpDeadline.Size = new System.Drawing.Size(190, 30);
            this.dtpDeadline.TabIndex = 5;
            this.dtpDeadline.Enabled = false;

            // Ngân sách + Priority (cùng hàng)
            y += gap;
            SetLabel(lblBudget, "NGÂN SÁCH (VNĐ)", lx, y);
            SetTextBox(txtBudget, "VD: 500000000", lx, y + 18, 190, 6);

            SetLabel(lblPriority, "ĐỘ ƯU TIÊN", lx + 210, y);
            this.cboPriority.BackColor = System.Drawing.Color.White;
            this.cboPriority.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboPriority.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboPriority.Location = new System.Drawing.Point(lx + 210, y + 18);
            this.cboPriority.Size = new System.Drawing.Size(190, 30);
            this.cboPriority.TabIndex = 7;

            // Mô tả
            y += gap;
            SetLabel(lblDesc, "MÔ TẢ (tùy chọn)", lx, y);
            this.txtDescription.BackColor = System.Drawing.Color.White;
            this.txtDescription.BorderStyle = BorderStyle.FixedSingle;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.Location = new System.Drawing.Point(lx, y + 18);
            this.txtDescription.Multiline = true;
            this.txtDescription.PlaceholderText = "Mô tả chi tiết dự án...";
            this.txtDescription.Size = new System.Drawing.Size(tw, 60);
            this.txtDescription.TabIndex = 8;

            // ── Footer ───────────────────────────────────────
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Dock = DockStyle.Bottom;
            this.panelFooter.Height = 76;
            this.panelFooter.Controls.AddRange(new Control[] { panelFooterLine, lblError, btnSave, btnCancel });

            this.panelFooterLine.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.panelFooterLine.Dock = DockStyle.Top;
            this.panelFooterLine.Height = 1;

            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.lblError.Location = new System.Drawing.Point(16, 6);
            this.lblError.Size = new System.Drawing.Size(420, 18);

            this.btnSave.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnSave.Cursor = Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(16, 28);
            this.btnSave.Size = new System.Drawing.Size(270, 40);
            this.btnSave.Text = "💾  Lưu";
            this.btnSave.Click += btnSave_Click;

            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnCancel.Cursor = Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.btnCancel.Location = new System.Drawing.Point(296, 28);
            this.btnCancel.Size = new System.Drawing.Size(130, 40);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += btnCancel_Click;

            // ── Form ─────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(460, 570);
            this.Controls.Add(this.panelBody);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProjectEdit";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Dự án";

            this.panelHeader.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // Helpers tạo Label/TextBox đồng bộ style
        private void SetLabel(Label lbl, string text, int x, int y)
        {
            lbl.AutoSize = true;
            lbl.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            lbl.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lbl.Location = new System.Drawing.Point(x, y);
            lbl.Text = text;
        }
        private void SetTextBox(TextBox txt, string placeholder, int x, int y, int w, int tabIdx)
        {
            txt.BackColor = System.Drawing.Color.White;
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = new System.Drawing.Font("Segoe UI", 10F);
            txt.Location = new System.Drawing.Point(x, y);
            txt.PlaceholderText = placeholder;
            txt.Size = new System.Drawing.Size(w, 30);
            txt.TabIndex = tabIdx;
        }

        private Panel panelHeader, panelAccentLine, panelBody, panelFooter, panelFooterLine;
        private Label lblTitleForm, lblError;
        private Label lblName, lblCustomer, lblOwner, lblStart, lblDeadline, lblBudget, lblPriority, lblDesc;
        private TextBox txtName, txtBudget, txtDescription;
        private ComboBox cboCustomer, cboOwner, cboPriority;
        private DateTimePicker dtpStartDate, dtpDeadline;
        private CheckBox chkDeadline;
        private Button btnSave, btnCancel;
    }
}
