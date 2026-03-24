namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmTaskEdit
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ── Khởi tạo tất cả control ──────────────────────────
            panelLeft        = new Panel();
            panelRight       = new Panel();
            panelButtons     = new Panel();

            lblTitle         = new Label();
            txtTitle         = new TextBox();

            lblDescription   = new Label();
            txtDescription   = new RichTextBox();

            lblProject       = new Label();
            cboProject       = new ComboBox();

            lblAssignee      = new Label();
            cboAssignee      = new ComboBox();

            lblPriority      = new Label();
            cboPriority      = new ComboBox();

            lblStatus        = new Label();
            cboStatus        = new ComboBox();

            lblCategory      = new Label();
            cboCategory      = new ComboBox();

            lblProgress      = new Label();
            numProgress      = new NumericUpDown();
            chkIsCompleted   = new CheckBox();

            lblEstimated     = new Label();
            numEstimatedHours = new NumericUpDown();

            lblDueDate       = new Label();
            chkHasDueDate    = new CheckBox();
            dtpDueDate       = new DateTimePicker();

            btnSave          = new Button();
            btnCancel        = new Button();

            panelLeft.SuspendLayout();
            panelRight.SuspendLayout();
            panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numProgress).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numEstimatedHours).BeginInit();
            this.SuspendLayout();

            // ════════════════════════════════════════════════════
            // panelLeft — cột trái (thông tin chính)
            // ════════════════════════════════════════════════════
            panelLeft.Location = new Point(12, 12);
            panelLeft.Size     = new Size(480, 500);

            // ── lblTitle ──────────────────────────────────────
            lblTitle.Location = new Point(0, 0);
            lblTitle.Size     = new Size(200, 20);
            lblTitle.Text     = "Tiêu đề công việc *";
            lblTitle.Font     = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            // ── txtTitle ──────────────────────────────────────
            txtTitle.Location    = new Point(0, 22);
            txtTitle.Size        = new Size(476, 28);
            txtTitle.Font        = new Font("Segoe UI", 10F);
            txtTitle.MaxLength   = 200;
            txtTitle.PlaceholderText = "Nhập tiêu đề công việc...";

            // ── lblProject ────────────────────────────────────
            lblProject.Location = new Point(0, 62);
            lblProject.Size     = new Size(200, 20);
            lblProject.Text     = "Dự án *";
            lblProject.Font     = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            // ── cboProject ────────────────────────────────────
            cboProject.Location      = new Point(0, 84);
            cboProject.Size          = new Size(476, 28);
            cboProject.Font          = new Font("Segoe UI", 10F);
            cboProject.DropDownStyle = ComboBoxStyle.DropDownList;

            // ── lblAssignee ───────────────────────────────────
            lblAssignee.Location = new Point(0, 124);
            lblAssignee.Size     = new Size(200, 20);
            lblAssignee.Text     = "Người thực hiện";
            lblAssignee.Font     = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            // ── cboAssignee ───────────────────────────────────
            cboAssignee.Location      = new Point(0, 146);
            cboAssignee.Size          = new Size(476, 28);
            cboAssignee.Font          = new Font("Segoe UI", 10F);
            cboAssignee.DropDownStyle = ComboBoxStyle.DropDownList;

            // ── lblDescription ────────────────────────────────
            lblDescription.Location = new Point(0, 186);
            lblDescription.Size     = new Size(200, 20);
            lblDescription.Text     = "Mô tả chi tiết";
            lblDescription.Font     = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            // ── txtDescription ────────────────────────────────
            txtDescription.Location   = new Point(0, 208);
            txtDescription.Size       = new Size(476, 140);
            txtDescription.Font       = new Font("Segoe UI", 9.5F);
            txtDescription.ScrollBars = RichTextBoxScrollBars.Vertical;

            // ── lblEstimated ──────────────────────────────────
            lblEstimated.Location = new Point(0, 362);
            lblEstimated.Size     = new Size(240, 20);
            lblEstimated.Text     = "Số giờ ước tính (giờ)";
            lblEstimated.Font     = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            // ── numEstimatedHours ─────────────────────────────
            numEstimatedHours.Location      = new Point(0, 384);
            numEstimatedHours.Size          = new Size(120, 28);
            numEstimatedHours.Font          = new Font("Segoe UI", 10F);
            numEstimatedHours.Minimum       = 0;
            numEstimatedHours.Maximum       = 9999;
            numEstimatedHours.DecimalPlaces = 1;
            numEstimatedHours.Value         = 0;

            // Đưa tất cả control vào panelLeft
            panelLeft.Controls.AddRange(new Control[]
            {
                lblTitle, txtTitle,
                lblProject, cboProject,
                lblAssignee, cboAssignee,
                lblDescription, txtDescription,
                lblEstimated, numEstimatedHours,
            });

            // ════════════════════════════════════════════════════
            // panelRight — cột phải (thuộc tính task)
            // ════════════════════════════════════════════════════
            panelRight.Location = new Point(510, 12);
            panelRight.Size     = new Size(260, 500);

            // ── lblPriority ───────────────────────────────────
            lblPriority.Location = new Point(0, 0);
            lblPriority.Size     = new Size(200, 20);
            lblPriority.Text     = "Mức ưu tiên *";
            lblPriority.Font     = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            // ── cboPriority ───────────────────────────────────
            cboPriority.Location      = new Point(0, 22);
            cboPriority.Size          = new Size(256, 28);
            cboPriority.Font          = new Font("Segoe UI", 10F);
            cboPriority.DropDownStyle = ComboBoxStyle.DropDownList;

            // ── lblStatus ─────────────────────────────────────
            lblStatus.Location = new Point(0, 62);
            lblStatus.Size     = new Size(200, 20);
            lblStatus.Text     = "Trạng thái *";
            lblStatus.Font     = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            // ── cboStatus ─────────────────────────────────────
            cboStatus.Location      = new Point(0, 84);
            cboStatus.Size          = new Size(256, 28);
            cboStatus.Font          = new Font("Segoe UI", 10F);
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            // ── lblCategory ───────────────────────────────────
            lblCategory.Location = new Point(0, 124);
            lblCategory.Size     = new Size(200, 20);
            lblCategory.Text     = "Phân loại (Category)";
            lblCategory.Font     = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            // ── cboCategory ───────────────────────────────────
            cboCategory.Location      = new Point(0, 146);
            cboCategory.Size          = new Size(256, 28);
            cboCategory.Font          = new Font("Segoe UI", 10F);
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            // ── lblProgress ───────────────────────────────────
            lblProgress.Location = new Point(0, 186);
            lblProgress.Size     = new Size(200, 20);
            lblProgress.Text     = "Tiến độ (%)";
            lblProgress.Font     = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            // ── numProgress ───────────────────────────────────
            numProgress.Location = new Point(0, 208);
            numProgress.Size     = new Size(100, 28);
            numProgress.Font     = new Font("Segoe UI", 10F);
            numProgress.Minimum  = 0;
            numProgress.Maximum  = 100;
            numProgress.Value    = 0;
            numProgress.ValueChanged += numProgress_ValueChanged;

            // ── chkIsCompleted ────────────────────────────────
            chkIsCompleted.Location = new Point(110, 210);
            chkIsCompleted.Size     = new Size(146, 24);
            chkIsCompleted.Text     = "Đã hoàn thành";
            chkIsCompleted.Font     = new Font("Segoe UI", 9.5F);

            // ── lblDueDate ────────────────────────────────────
            lblDueDate.Location = new Point(0, 252);
            lblDueDate.Size     = new Size(200, 20);
            lblDueDate.Text     = "Ngày hạn chót";
            lblDueDate.Font     = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            // ── chkHasDueDate ─────────────────────────────────
            chkHasDueDate.Location        = new Point(0, 274);
            chkHasDueDate.Size            = new Size(140, 24);
            chkHasDueDate.Text            = "Có ngày hạn chót";
            chkHasDueDate.Font            = new Font("Segoe UI", 9.5F);
            chkHasDueDate.CheckedChanged += chkHasDueDate_CheckedChanged;

            // ── dtpDueDate ────────────────────────────────────
            dtpDueDate.Location = new Point(0, 300);
            dtpDueDate.Size     = new Size(256, 28);
            dtpDueDate.Font     = new Font("Segoe UI", 10F);
            dtpDueDate.Format   = DateTimePickerFormat.Short;
            dtpDueDate.Enabled  = false; // Chỉ bật khi tick chkHasDueDate

            // Đưa tất cả control vào panelRight
            panelRight.Controls.AddRange(new Control[]
            {
                lblPriority, cboPriority,
                lblStatus, cboStatus,
                lblCategory, cboCategory,
                lblProgress, numProgress, chkIsCompleted,
                lblDueDate, chkHasDueDate, dtpDueDate,
            });

            // ════════════════════════════════════════════════════
            // panelButtons — thanh nút phía dưới
            // ════════════════════════════════════════════════════
            panelButtons.Dock      = DockStyle.Bottom;
            panelButtons.Height    = 52;
            panelButtons.Padding   = new Padding(10, 10, 10, 6);

            // ── btnSave ───────────────────────────────────────
            btnSave.Location  = new Point(600, 11);
            btnSave.Size      = new Size(100, 32);
            btnSave.Text      = "💾 Lưu";
            btnSave.Font      = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.BackColor = Color.FromArgb(37, 99, 235);   // Xanh dương
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click    += btnSave_Click;

            // ── btnCancel ─────────────────────────────────────
            btnCancel.Location     = new Point(710, 11);
            btnCancel.Size         = new Size(80, 32);
            btnCancel.Text         = "✖ Hủy";
            btnCancel.Font         = new Font("Segoe UI", 10F);
            btnCancel.BackColor    = Color.FromArgb(239, 68, 68); // Đỏ
            btnCancel.ForeColor    = Color.White;
            btnCancel.FlatStyle    = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click       += btnCancel_Click;

            panelButtons.Controls.AddRange(new Control[] { btnSave, btnCancel });

            // ════════════════════════════════════════════════════
            // Form
            // ════════════════════════════════════════════════════
            this.Text            = "Công việc";
            this.Size            = new Size(810, 620);
            this.MinimumSize     = new Size(810, 580);
            this.MaximizeBox     = false;
            this.StartPosition   = FormStartPosition.CenterParent;
            this.Font            = new Font("Segoe UI", 9.5F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Không resize được

            this.Controls.Add(panelLeft);
            this.Controls.Add(panelRight);
            this.Controls.Add(panelButtons);

            panelLeft.ResumeLayout(false);
            panelRight.ResumeLayout(false);
            panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numProgress).EndInit();
            ((System.ComponentModel.ISupportInitialize)numEstimatedHours).EndInit();
            this.ResumeLayout(false);
        }

        // ── Khai báo biến — theo quy ước Designer để cuối file ──
        private Panel            panelLeft;
        private Panel            panelRight;
        private Panel            panelButtons;

        private Label            lblTitle;
        private TextBox          txtTitle;

        private Label            lblDescription;
        private RichTextBox      txtDescription;

        private Label            lblProject;
        private ComboBox         cboProject;

        private Label            lblAssignee;
        private ComboBox         cboAssignee;

        private Label            lblPriority;
        private ComboBox         cboPriority;

        private Label            lblStatus;
        private ComboBox         cboStatus;

        private Label            lblCategory;
        private ComboBox         cboCategory;

        private Label            lblProgress;
        private NumericUpDown    numProgress;
        private CheckBox         chkIsCompleted;

        private Label            lblEstimated;
        private NumericUpDown    numEstimatedHours;

        private Label            lblDueDate;
        private CheckBox         chkHasDueDate;
        private DateTimePicker   dtpDueDate;

        private Button           btnSave;
        private Button           btnCancel;
    }
}
