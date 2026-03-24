namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmProjectMembers
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelHeader = new Panel();
            this.lblTitle    = new Label();
            this.panelAccent = new Panel();
            this.dgvMembers  = new DataGridView();
            this.panelAdd    = new Panel();
            this.lblAddTitle = new Label();
            this.cboUser     = new ComboBox();
            this.cboProjectRole = new ComboBox();
            this.btnAddMember = new Button();
            this.panelBottom = new Panel();
            this.btnRemove   = new Button();
            this.btnClose    = new Button();
            this.lblCount    = new Label();

            this.colMemberId   = new DataGridViewTextBoxColumn();
            this.colMemberName = new DataGridViewTextBoxColumn();
            this.colMemberEmail = new DataGridViewTextBoxColumn();
            this.colMemberRole = new DataGridViewTextBoxColumn();
            this.colJoinedAt   = new DataGridViewTextBoxColumn();

            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.dgvMembers).BeginInit();
            this.panelAdd.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();

            // ── Header ───────────────────────────────────────
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Height = 56;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.panelAccent);

            this.lblTitle.AutoSize = false;
            this.lblTitle.Dock = DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Padding = new Padding(16, 0, 0, 0);
            this.lblTitle.Text = "👥  Thành viên dự án";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.panelAccent.BackColor = System.Drawing.Color.FromArgb(124, 58, 237);
            this.panelAccent.Dock = DockStyle.Bottom;
            this.panelAccent.Height = 3;

            // ── DataGridView ─────────────────────────────────
            this.dgvMembers.AllowUserToAddRows = false;
            this.dgvMembers.AllowUserToDeleteRows = false;
            this.dgvMembers.BackgroundColor = System.Drawing.Color.White;
            this.dgvMembers.BorderStyle = BorderStyle.None;
            this.dgvMembers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvMembers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMembers.Dock = DockStyle.Fill;
            this.dgvMembers.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dgvMembers.GridColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.dgvMembers.MultiSelect = false;
            this.dgvMembers.ReadOnly = true;
            this.dgvMembers.RowHeadersVisible = false;
            this.dgvMembers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvMembers.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.dgvMembers.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvMembers.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvMembers.EnableHeadersVisualStyles = false;
            this.dgvMembers.RowTemplate.Height = 34;

            this.colMemberId.Name = "colMemberId"; this.colMemberId.HeaderText = "ID"; this.colMemberId.Visible = false;
            this.colMemberName.Name = "colMemberName"; this.colMemberName.HeaderText = "Họ tên"; this.colMemberName.Width = 180;
            this.colMemberEmail.Name = "colMemberEmail"; this.colMemberEmail.HeaderText = "Email"; this.colMemberEmail.Width = 200;
            this.colMemberRole.Name = "colMemberRole"; this.colMemberRole.HeaderText = "Vai trò DA"; this.colMemberRole.Width = 110;
            this.colJoinedAt.Name = "colJoinedAt"; this.colJoinedAt.HeaderText = "Ngày tham gia"; this.colJoinedAt.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            this.dgvMembers.Columns.AddRange(new DataGridViewColumn[]
            { colMemberId, colMemberName, colMemberEmail, colMemberRole, colJoinedAt });

            // ── Panel thêm thành viên ────────────────────────
            this.panelAdd.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.panelAdd.Dock = DockStyle.Bottom;
            this.panelAdd.Height = 80;
            this.panelAdd.Controls.AddRange(new Control[]
            { this.lblAddTitle, this.cboUser, this.cboProjectRole, this.btnAddMember });

            this.lblAddTitle.AutoSize = true;
            this.lblAddTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblAddTitle.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblAddTitle.Location = new System.Drawing.Point(14, 8);
            this.lblAddTitle.Text = "THÊM THÀNH VIÊN";

            this.cboUser.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboUser.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboUser.Location = new System.Drawing.Point(14, 30);
            this.cboUser.Size = new System.Drawing.Size(260, 30);

            this.cboProjectRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboProjectRole.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboProjectRole.Location = new System.Drawing.Point(284, 30);
            this.cboProjectRole.Size = new System.Drawing.Size(120, 30);

            this.btnAddMember.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnAddMember.Cursor = Cursors.Hand;
            this.btnAddMember.FlatStyle = FlatStyle.Flat;
            this.btnAddMember.FlatAppearance.BorderSize = 0;
            this.btnAddMember.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddMember.ForeColor = System.Drawing.Color.White;
            this.btnAddMember.Location = new System.Drawing.Point(414, 30);
            this.btnAddMember.Size = new System.Drawing.Size(100, 30);
            this.btnAddMember.Text = "➕ Thêm";
            this.btnAddMember.Click += btnAddMember_Click;

            // ── Panel bottom ─────────────────────────────────
            this.panelBottom.BackColor = System.Drawing.Color.White;
            this.panelBottom.Dock = DockStyle.Bottom;
            this.panelBottom.Height = 50;
            this.panelBottom.Controls.AddRange(new Control[] { btnRemove, btnClose, lblCount });

            this.btnRemove.BackColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.btnRemove.Cursor = Cursors.Hand;
            this.btnRemove.FlatStyle = FlatStyle.Flat;
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.Location = new System.Drawing.Point(14, 10);
            this.btnRemove.Size = new System.Drawing.Size(140, 32);
            this.btnRemove.Text = "🗑️  Xóa thành viên";
            this.btnRemove.Click += btnRemove_Click;

            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblCount.Location = new System.Drawing.Point(164, 10);
            this.lblCount.Size = new System.Drawing.Size(200, 32);
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.btnClose.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnClose.Cursor = Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.btnClose.Location = new System.Drawing.Point(414, 10);
            this.btnClose.Size = new System.Drawing.Size(100, 32);
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += btnClose_Click;

            // ── Form ─────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(540, 460);
            this.Controls.Add(this.dgvMembers);
            this.Controls.Add(this.panelAdd);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProjectMembers";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Thành viên dự án";

            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.dgvMembers).EndInit();
            this.panelAdd.ResumeLayout(false);
            this.panelAdd.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private Panel panelHeader, panelAccent, panelAdd, panelBottom;
        private Label lblTitle, lblAddTitle, lblCount;
        private DataGridView dgvMembers;
        private ComboBox cboUser, cboProjectRole;
        private Button btnAddMember, btnRemove, btnClose;
        private DataGridViewTextBoxColumn colMemberId, colMemberName, colMemberEmail, colMemberRole, colJoinedAt;
    }
}
