namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmCustomers
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTop      = new System.Windows.Forms.Panel();
            this.lblHeader     = new System.Windows.Forms.Label();
            this.panelFilter   = new System.Windows.Forms.Panel();
            this.txtSearch     = new System.Windows.Forms.TextBox();
            this.btnRefresh    = new System.Windows.Forms.Button();
            this.panelToolbar  = new System.Windows.Forms.Panel();
            this.btnAdd        = new System.Windows.Forms.Button();
            this.btnEdit       = new System.Windows.Forms.Button();
            this.btnDelete     = new System.Windows.Forms.Button();
            this.btnDetail     = new System.Windows.Forms.Button();
            this.lblCount      = new System.Windows.Forms.Label();
            this.dgvCustomers  = new System.Windows.Forms.DataGridView();
            this.panelStatus   = new System.Windows.Forms.Panel();
            this.lblStatus     = new System.Windows.Forms.Label();

            this.colCustId      = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCompany     = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContact     = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustEmail   = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustPhone   = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAddress     = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreatedAt   = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.panelTop.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.panelToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.dgvCustomers).BeginInit();
            this.SuspendLayout();

            // panelTop
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.panelTop.Dock      = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height    = 52;
            this.panelTop.Name      = "panelTop";
            this.panelTop.Controls.Add(this.lblHeader);

            this.lblHeader.AutoSize  = false;
            this.lblHeader.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font      = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Name      = "lblHeader";
            this.lblHeader.Text      = "  🏢  Quản lý Khách hàng";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // panelFilter
            this.panelFilter.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.panelFilter.Dock      = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Height    = 46;
            this.panelFilter.Name      = "panelFilter";
            this.panelFilter.Controls.AddRange(new System.Windows.Forms.Control[]
            { this.txtSearch, this.btnRefresh });

            this.txtSearch.BorderStyle    = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font           = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSearch.Location       = new System.Drawing.Point(12, 10);
            this.txtSearch.Name           = "txtSearch";
            this.txtSearch.PlaceholderText = "🔍  Tìm theo tên công ty, liên hệ, email...";
            this.txtSearch.Size           = new System.Drawing.Size(340, 26);
            this.txtSearch.TextChanged   += new System.EventHandler(this.txtSearch_TextChanged);

            this.btnRefresh.BackColor                  = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnRefresh.FlatStyle                  = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnRefresh.Font                       = new System.Drawing.Font("Segoe UI Emoji", 10F);
            this.btnRefresh.Location                   = new System.Drawing.Point(362, 10);
            this.btnRefresh.Name                       = "btnRefresh";
            this.btnRefresh.Size                       = new System.Drawing.Size(36, 26);
            this.btnRefresh.Text                       = "🔄";
            this.btnRefresh.Cursor                     = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Click                     += new System.EventHandler(this.btnRefresh_Click);

            // panelToolbar
            this.panelToolbar.BackColor = System.Drawing.Color.White;
            this.panelToolbar.Dock      = System.Windows.Forms.DockStyle.Top;
            this.panelToolbar.Height    = 44;
            this.panelToolbar.Name      = "panelToolbar";
            this.panelToolbar.Controls.AddRange(new System.Windows.Forms.Control[]
            { this.btnAdd, this.btnEdit, this.btnDelete, this.btnDetail, this.lblCount });

            // ── btnAdd ───────────────────────────────────────────
            this.btnAdd.BackColor                 = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnAdd.Cursor                    = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.Font                      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor                 = System.Drawing.Color.White;
            this.btnAdd.Location                  = new System.Drawing.Point(12, 7);
            this.btnAdd.Name                      = "btnAdd";
            this.btnAdd.Size                      = new System.Drawing.Size(130, 30);
            this.btnAdd.Text                      = "➕  Thêm mới";

            // ── btnEdit ──────────────────────────────────────────
            this.btnEdit.BackColor                 = System.Drawing.Color.FromArgb(5, 150, 105);
            this.btnEdit.Cursor                    = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.Font                      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor                 = System.Drawing.Color.White;
            this.btnEdit.Location                  = new System.Drawing.Point(152, 7);
            this.btnEdit.Name                      = "btnEdit";
            this.btnEdit.Size                      = new System.Drawing.Size(100, 30);
            this.btnEdit.Text                      = "✏️  Sửa";

            // ── btnDelete ────────────────────────────────────────
            this.btnDelete.BackColor                 = System.Drawing.Color.FromArgb(220, 38, 38);
            this.btnDelete.Cursor                    = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.Font                      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor                 = System.Drawing.Color.White;
            this.btnDelete.Location                  = new System.Drawing.Point(262, 7);
            this.btnDelete.Name                      = "btnDelete";
            this.btnDelete.Size                      = new System.Drawing.Size(100, 30);
            this.btnDelete.Text                      = "🗑️  Xóa";

            // ── btnDetail ────────────────────────────────────────
            this.btnDetail.BackColor                 = System.Drawing.Color.FromArgb(124, 58, 237);
            this.btnDetail.Cursor                    = System.Windows.Forms.Cursors.Hand;
            this.btnDetail.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetail.FlatAppearance.BorderSize = 0;
            this.btnDetail.Font                      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDetail.ForeColor                 = System.Drawing.Color.White;
            this.btnDetail.Location                  = new System.Drawing.Point(372, 7);
            this.btnDetail.Name                      = "btnDetail";
            this.btnDetail.Size                      = new System.Drawing.Size(130, 30);
            this.btnDetail.Text                      = "📋  Xem dự án";

            this.btnAdd.Click    += new System.EventHandler(this.btnAdd_Click);
            this.btnEdit.Click   += new System.EventHandler(this.btnEdit_Click);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            this.btnEdit.Enabled   = false;
            this.btnDelete.Enabled = false;
            this.btnDetail.Enabled = false;

            this.lblCount.AutoSize  = false;
            this.lblCount.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblCount.Location  = new System.Drawing.Point(516, 14);
            this.lblCount.Name      = "lblCount";
            this.lblCount.Size      = new System.Drawing.Size(200, 20);
            this.lblCount.Text      = "";

            // DataGridView
            this.dgvCustomers.AllowUserToAddRows    = false;
            this.dgvCustomers.AllowUserToDeleteRows = false;
            this.dgvCustomers.BackgroundColor       = System.Drawing.Color.White;
            this.dgvCustomers.BorderStyle           = System.Windows.Forms.BorderStyle.None;
            this.dgvCustomers.CellBorderStyle       = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomers.Dock                  = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustomers.Font                  = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dgvCustomers.GridColor             = System.Drawing.Color.FromArgb(241, 245, 249);
            this.dgvCustomers.MultiSelect           = false;
            this.dgvCustomers.Name                  = "dgvCustomers";
            this.dgvCustomers.ReadOnly              = true;
            this.dgvCustomers.RowHeadersVisible     = false;
            this.dgvCustomers.SelectionMode         = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomers.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.dgvCustomers.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvCustomers.ColumnHeadersDefaultCellStyle.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvCustomers.EnableHeadersVisualStyles = false;
            this.dgvCustomers.RowTemplate.Height    = 34;
            this.dgvCustomers.SelectionChanged     += new System.EventHandler(this.dgvCustomers_SelectionChanged);
            this.dgvCustomers.CellDoubleClick      += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustomers_CellDoubleClick);

            this.colCustId.Name    = "colCustId";    this.colCustId.HeaderText    = "ID";           this.colCustId.Width    = 45; this.colCustId.Visible = false;
            this.colCompany.Name   = "colCompany";   this.colCompany.HeaderText   = "Tên công ty";  this.colCompany.Width   = 220;
            this.colContact.Name   = "colContact";   this.colContact.HeaderText   = "Người liên hệ"; this.colContact.Width  = 160;
            this.colCustEmail.Name = "colCustEmail"; this.colCustEmail.HeaderText = "Email";         this.colCustEmail.Width = 190;
            this.colCustPhone.Name = "colCustPhone"; this.colCustPhone.HeaderText = "Điện thoại";   this.colCustPhone.Width = 120;
            this.colAddress.Name   = "colAddress";   this.colAddress.HeaderText   = "Địa chỉ";      this.colAddress.Width   = 200;
            this.colCreatedAt.Name = "colCreatedAt"; this.colCreatedAt.HeaderText = "Ngày tạo";     this.colCreatedAt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;

            this.dgvCustomers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            { colCustId, colCompany, colContact, colCustEmail, colCustPhone, colAddress, colCreatedAt });

            // panelStatus
            this.panelStatus.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.panelStatus.Dock      = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatus.Height    = 28;
            this.panelStatus.Name      = "panelStatus";
            this.panelStatus.Controls.Add(this.lblStatus);

            this.lblStatus.AutoSize  = false;
            this.lblStatus.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblStatus.Name      = "lblStatus";
            this.lblStatus.Padding   = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblStatus.Text      = "";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // frmCustomers
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.White;
            this.ClientSize          = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.dgvCustomers);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.panelToolbar);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelTop);
            this.Font          = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name          = "frmCustomers";
            this.Text          = "Quản lý Khách hàng";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;

            this.panelTop.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelToolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.dgvCustomers).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel   panelTop;
        private System.Windows.Forms.Label   lblHeader;
        private System.Windows.Forms.Panel   panelFilter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button  btnRefresh;
        private System.Windows.Forms.Panel   panelToolbar;
        private System.Windows.Forms.Button  btnAdd;
        private System.Windows.Forms.Button  btnEdit;
        private System.Windows.Forms.Button  btnDelete;
        private System.Windows.Forms.Button  btnDetail;
        private System.Windows.Forms.Label   lblCount;
        private System.Windows.Forms.DataGridView dgvCustomers;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompany;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContact;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreatedAt;
        private System.Windows.Forms.Panel   panelStatus;
        private System.Windows.Forms.Label   lblStatus;
    }
}
