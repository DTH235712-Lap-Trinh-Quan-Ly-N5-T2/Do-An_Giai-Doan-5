namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmHome
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelHeader      = new System.Windows.Forms.Panel();
            this.lblGreeting      = new System.Windows.Forms.Label();
            this.lblRole          = new System.Windows.Forms.Label();
            this.lblLastLogin     = new System.Windows.Forms.Label();
            this.panelAccentLine  = new System.Windows.Forms.Panel();
            this.panelStats       = new System.Windows.Forms.Panel();

            // Card 1 – Dự án đang chạy
            this.panelCard1       = new System.Windows.Forms.Panel();
            this.panelCard1Top    = new System.Windows.Forms.Panel();
            this.lblCard1Icon     = new System.Windows.Forms.Label();
            this.lblCard1Title    = new System.Windows.Forms.Label();
            this.lblStatProjects  = new System.Windows.Forms.Label();
            this.lblCard1Sub      = new System.Windows.Forms.Label();

            // Card 2 – Công việc của tôi
            this.panelCard2       = new System.Windows.Forms.Panel();
            this.panelCard2Top    = new System.Windows.Forms.Panel();
            this.lblCard2Icon     = new System.Windows.Forms.Label();
            this.lblCard2Title    = new System.Windows.Forms.Label();
            this.lblStatTasks     = new System.Windows.Forms.Label();
            this.lblCard2Sub      = new System.Windows.Forms.Label();

            // Card 3 – Quá hạn
            this.panelCard3       = new System.Windows.Forms.Panel();
            this.panelCard3Top    = new System.Windows.Forms.Panel();
            this.lblCard3Icon     = new System.Windows.Forms.Label();
            this.lblCard3Title    = new System.Windows.Forms.Label();
            this.lblStatOverdue   = new System.Windows.Forms.Label();
            this.lblCard3Sub      = new System.Windows.Forms.Label();

            // Card 4 – Hoàn thành tháng này
            this.panelCard4       = new System.Windows.Forms.Panel();
            this.panelCard4Top    = new System.Windows.Forms.Panel();
            this.lblCard4Icon     = new System.Windows.Forms.Label();
            this.lblCard4Title    = new System.Windows.Forms.Label();
            this.lblStatDone      = new System.Windows.Forms.Label();
            this.lblCard4Sub      = new System.Windows.Forms.Label();

            this.lblNote          = new System.Windows.Forms.Label();

            // FIX: FlowLayoutPanel để cards tự layout khi resize
            this.flowCards        = new System.Windows.Forms.FlowLayoutPanel();

            this.panelHeader.SuspendLayout();
            this.panelStats.SuspendLayout();
            this.SuspendLayout();

            // ── panelHeader ──────────────────────────────────────
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.panelHeader.Dock      = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Height    = 110;
            this.panelHeader.Name      = "panelHeader";
            this.panelHeader.Controls.AddRange(new System.Windows.Forms.Control[]
            { this.lblGreeting, this.lblRole, this.lblLastLogin, this.panelAccentLine });

            this.panelAccentLine.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.panelAccentLine.Dock      = System.Windows.Forms.DockStyle.Bottom;
            this.panelAccentLine.Height    = 3;
            this.panelAccentLine.Name      = "panelAccentLine";

            this.lblGreeting.AutoSize  = false;
            this.lblGreeting.Font      = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblGreeting.ForeColor = System.Drawing.Color.White;
            this.lblGreeting.Location  = new System.Drawing.Point(24, 12);
            this.lblGreeting.Name      = "lblGreeting";
            this.lblGreeting.Size      = new System.Drawing.Size(900, 40);
            this.lblGreeting.Text      = "Chào buổi sáng, ...! 👋";

            this.lblRole.AutoSize  = false;
            this.lblRole.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblRole.Location  = new System.Drawing.Point(26, 54);
            this.lblRole.Name      = "lblRole";
            this.lblRole.Size      = new System.Drawing.Size(400, 22);
            this.lblRole.Text      = "Vai trò: ...";

            this.lblLastLogin.AutoSize  = false;
            this.lblLastLogin.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLastLogin.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblLastLogin.Location  = new System.Drawing.Point(26, 78);
            this.lblLastLogin.Name      = "lblLastLogin";
            this.lblLastLogin.Size      = new System.Drawing.Size(500, 20);
            this.lblLastLogin.Text      = "";

            // ── panelStats (container chính) ─────────────────────
            this.panelStats.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.panelStats.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.panelStats.Name      = "panelStats";
            this.panelStats.Padding   = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.panelStats.Controls.Add(this.lblNote);
            this.panelStats.Controls.Add(this.flowCards);

            // ── FIX: FlowLayoutPanel để cards tự xếp khi resize ──
            this.flowCards.Dock      = System.Windows.Forms.DockStyle.Top;
            this.flowCards.AutoSize  = true;
            this.flowCards.BackColor = System.Drawing.Color.Transparent;
            this.flowCards.Name      = "flowCards";
            this.flowCards.Padding   = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.flowCards.WrapContents = true;
            this.flowCards.Controls.AddRange(new System.Windows.Forms.Control[]
            { this.panelCard1, this.panelCard2, this.panelCard3, this.panelCard4 });

            // ── Helper: kích thước card cố định ──────────────────
            var cardSize = new System.Drawing.Size(220, 150);
            var cardMargin = new System.Windows.Forms.Padding(10, 5, 10, 5);

            // ── Card 1: Dự án đang chạy ─────────────────────────
            this.panelCard1.BackColor   = System.Drawing.Color.White;
            this.panelCard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCard1.Name        = "panelCard1";
            this.panelCard1.Size        = cardSize;
            this.panelCard1.Margin      = cardMargin;
            this.panelCard1.Controls.AddRange(new System.Windows.Forms.Control[]
            { this.panelCard1Top, this.lblCard1Icon, this.lblCard1Title, this.lblStatProjects, this.lblCard1Sub });

            this.panelCard1Top.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.panelCard1Top.Dock      = System.Windows.Forms.DockStyle.Top;
            this.panelCard1Top.Height    = 5;
            this.panelCard1Top.Name      = "panelCard1Top";

            this.lblCard1Icon.Font      = new System.Drawing.Font("Segoe UI Emoji", 22F);
            this.lblCard1Icon.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblCard1Icon.Location  = new System.Drawing.Point(14, 14);
            this.lblCard1Icon.Name      = "lblCard1Icon";
            this.lblCard1Icon.Size      = new System.Drawing.Size(44, 40);
            this.lblCard1Icon.Text      = "📁";
            this.lblCard1Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblCard1Title.Font      = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCard1Title.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblCard1Title.Location  = new System.Drawing.Point(64, 20);
            this.lblCard1Title.Name      = "lblCard1Title";
            this.lblCard1Title.Size      = new System.Drawing.Size(150, 16);
            this.lblCard1Title.Text      = "DỰ ÁN ĐANG CHẠY";

            this.lblStatProjects.Font      = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            this.lblStatProjects.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblStatProjects.Location  = new System.Drawing.Point(14, 56);
            this.lblStatProjects.Name      = "lblStatProjects";
            this.lblStatProjects.Size      = new System.Drawing.Size(190, 56);
            this.lblStatProjects.Text      = "...";

            this.lblCard1Sub.Font      = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCard1Sub.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblCard1Sub.Location  = new System.Drawing.Point(14, 116);
            this.lblCard1Sub.Name      = "lblCard1Sub";
            this.lblCard1Sub.Size      = new System.Drawing.Size(190, 16);
            this.lblCard1Sub.Text      = "dự án InProgress";

            // ── Card 2: Công việc của tôi ────────────────────────
            this.panelCard2.BackColor   = System.Drawing.Color.White;
            this.panelCard2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCard2.Name        = "panelCard2";
            this.panelCard2.Size        = cardSize;
            this.panelCard2.Margin      = cardMargin;
            this.panelCard2.Controls.AddRange(new System.Windows.Forms.Control[]
            { this.panelCard2Top, this.lblCard2Icon, this.lblCard2Title, this.lblStatTasks, this.lblCard2Sub });

            this.panelCard2Top.BackColor = System.Drawing.Color.FromArgb(5, 150, 105);
            this.panelCard2Top.Dock      = System.Windows.Forms.DockStyle.Top;
            this.panelCard2Top.Height    = 5;
            this.panelCard2Top.Name      = "panelCard2Top";

            this.lblCard2Icon.Font      = new System.Drawing.Font("Segoe UI Emoji", 22F);
            this.lblCard2Icon.ForeColor = System.Drawing.Color.FromArgb(5, 150, 105);
            this.lblCard2Icon.Location  = new System.Drawing.Point(14, 14);
            this.lblCard2Icon.Name      = "lblCard2Icon";
            this.lblCard2Icon.Size      = new System.Drawing.Size(44, 40);
            this.lblCard2Icon.Text      = "✅";
            this.lblCard2Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblCard2Title.Font      = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCard2Title.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblCard2Title.Location  = new System.Drawing.Point(64, 20);
            this.lblCard2Title.Name      = "lblCard2Title";
            this.lblCard2Title.Size      = new System.Drawing.Size(150, 16);
            this.lblCard2Title.Text      = "CÔNG VIỆC CỦA TÔI";

            this.lblStatTasks.Font      = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            this.lblStatTasks.ForeColor = System.Drawing.Color.FromArgb(5, 150, 105);
            this.lblStatTasks.Location  = new System.Drawing.Point(14, 56);
            this.lblStatTasks.Name      = "lblStatTasks";
            this.lblStatTasks.Size      = new System.Drawing.Size(190, 56);
            this.lblStatTasks.Text      = "...";

            this.lblCard2Sub.Font      = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCard2Sub.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblCard2Sub.Location  = new System.Drawing.Point(14, 116);
            this.lblCard2Sub.Name      = "lblCard2Sub";
            this.lblCard2Sub.Size      = new System.Drawing.Size(190, 16);
            this.lblCard2Sub.Text      = "task được giao";

            // ── Card 3: Quá hạn ──────────────────────────────────
            this.panelCard3.BackColor   = System.Drawing.Color.White;
            this.panelCard3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCard3.Name        = "panelCard3";
            this.panelCard3.Size        = cardSize;
            this.panelCard3.Margin      = cardMargin;
            this.panelCard3.Controls.AddRange(new System.Windows.Forms.Control[]
            { this.panelCard3Top, this.lblCard3Icon, this.lblCard3Title, this.lblStatOverdue, this.lblCard3Sub });

            this.panelCard3Top.BackColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.panelCard3Top.Dock      = System.Windows.Forms.DockStyle.Top;
            this.panelCard3Top.Height    = 5;
            this.panelCard3Top.Name      = "panelCard3Top";

            this.lblCard3Icon.Font      = new System.Drawing.Font("Segoe UI Emoji", 22F);
            this.lblCard3Icon.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.lblCard3Icon.Location  = new System.Drawing.Point(14, 14);
            this.lblCard3Icon.Name      = "lblCard3Icon";
            this.lblCard3Icon.Size      = new System.Drawing.Size(44, 40);
            this.lblCard3Icon.Text      = "⚠";
            this.lblCard3Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblCard3Title.Font      = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCard3Title.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblCard3Title.Location  = new System.Drawing.Point(64, 20);
            this.lblCard3Title.Name      = "lblCard3Title";
            this.lblCard3Title.Size      = new System.Drawing.Size(150, 16);
            this.lblCard3Title.Text      = "QUÁ HẠN";

            this.lblStatOverdue.Font      = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            this.lblStatOverdue.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.lblStatOverdue.Location  = new System.Drawing.Point(14, 56);
            this.lblStatOverdue.Name      = "lblStatOverdue";
            this.lblStatOverdue.Size      = new System.Drawing.Size(190, 56);
            this.lblStatOverdue.Text      = "...";

            this.lblCard3Sub.Font      = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCard3Sub.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblCard3Sub.Location  = new System.Drawing.Point(14, 116);
            this.lblCard3Sub.Name      = "lblCard3Sub";
            this.lblCard3Sub.Size      = new System.Drawing.Size(190, 16);
            this.lblCard3Sub.Text      = "task đã qua deadline";

            // ── Card 4: Hoàn thành tháng này ─────────────────────
            this.panelCard4.BackColor   = System.Drawing.Color.White;
            this.panelCard4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCard4.Name        = "panelCard4";
            this.panelCard4.Size        = cardSize;
            this.panelCard4.Margin      = cardMargin;
            this.panelCard4.Controls.AddRange(new System.Windows.Forms.Control[]
            { this.panelCard4Top, this.lblCard4Icon, this.lblCard4Title, this.lblStatDone, this.lblCard4Sub });

            this.panelCard4Top.BackColor = System.Drawing.Color.FromArgb(124, 58, 237);
            this.panelCard4Top.Dock      = System.Windows.Forms.DockStyle.Top;
            this.panelCard4Top.Height    = 5;
            this.panelCard4Top.Name      = "panelCard4Top";

            this.lblCard4Icon.Font      = new System.Drawing.Font("Segoe UI Emoji", 22F);
            this.lblCard4Icon.ForeColor = System.Drawing.Color.FromArgb(124, 58, 237);
            this.lblCard4Icon.Location  = new System.Drawing.Point(14, 14);
            this.lblCard4Icon.Name      = "lblCard4Icon";
            this.lblCard4Icon.Size      = new System.Drawing.Size(44, 40);
            this.lblCard4Icon.Text      = "🎯";
            this.lblCard4Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblCard4Title.Font      = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCard4Title.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblCard4Title.Location  = new System.Drawing.Point(64, 20);
            this.lblCard4Title.Name      = "lblCard4Title";
            this.lblCard4Title.Size      = new System.Drawing.Size(150, 16);
            this.lblCard4Title.Text      = "XONG THÁNG NÀY";

            this.lblStatDone.Font      = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            this.lblStatDone.ForeColor = System.Drawing.Color.FromArgb(124, 58, 237);
            this.lblStatDone.Location  = new System.Drawing.Point(14, 56);
            this.lblStatDone.Name      = "lblStatDone";
            this.lblStatDone.Size      = new System.Drawing.Size(190, 56);
            this.lblStatDone.Text      = "...";

            this.lblCard4Sub.Font      = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCard4Sub.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblCard4Sub.Location  = new System.Drawing.Point(14, 116);
            this.lblCard4Sub.Name      = "lblCard4Sub";
            this.lblCard4Sub.Size      = new System.Drawing.Size(190, 16);
            this.lblCard4Sub.Text      = $"task hoàn thành T{DateTime.Now.Month}";

            // ── lblNote ──────────────────────────────────────────
            this.lblNote.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNote.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblNote.Dock      = System.Windows.Forms.DockStyle.Bottom;
            this.lblNote.Name      = "lblNote";
            this.lblNote.Size      = new System.Drawing.Size(900, 28);
            this.lblNote.Padding   = new System.Windows.Forms.Padding(30, 0, 0, 8);
            this.lblNote.Text      = "ℹ️  Đang tải số liệu...";

            // ── frmHome ──────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.FromArgb(241, 245, 249);
            this.ClientSize          = new System.Drawing.Size(960, 540);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.panelHeader);
            this.Font          = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name          = "frmHome";
            this.Text          = "Trang chủ";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;

            this.panelHeader.ResumeLayout(false);
            this.panelStats.ResumeLayout(false);
            this.panelStats.PerformLayout();
            this.ResumeLayout(false);
        }

        // ── Field declarations ────────────────────────────────────
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblGreeting;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Label lblLastLogin;
        private System.Windows.Forms.Panel panelAccentLine;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.FlowLayoutPanel flowCards; // FIX: responsive cards

        private System.Windows.Forms.Panel panelCard1;
        private System.Windows.Forms.Panel panelCard1Top;
        private System.Windows.Forms.Label lblCard1Icon;
        private System.Windows.Forms.Label lblCard1Title;
        private System.Windows.Forms.Label lblStatProjects;
        private System.Windows.Forms.Label lblCard1Sub;

        private System.Windows.Forms.Panel panelCard2;
        private System.Windows.Forms.Panel panelCard2Top;
        private System.Windows.Forms.Label lblCard2Icon;
        private System.Windows.Forms.Label lblCard2Title;
        private System.Windows.Forms.Label lblStatTasks;
        private System.Windows.Forms.Label lblCard2Sub;

        private System.Windows.Forms.Panel panelCard3;
        private System.Windows.Forms.Panel panelCard3Top;
        private System.Windows.Forms.Label lblCard3Icon;
        private System.Windows.Forms.Label lblCard3Title;
        private System.Windows.Forms.Label lblStatOverdue;
        private System.Windows.Forms.Label lblCard3Sub;

        private System.Windows.Forms.Panel panelCard4;
        private System.Windows.Forms.Panel panelCard4Top;
        private System.Windows.Forms.Label lblCard4Icon;
        private System.Windows.Forms.Label lblCard4Title;
        private System.Windows.Forms.Label lblStatDone;
        private System.Windows.Forms.Label lblCard4Sub;

        private System.Windows.Forms.Label lblNote;
    }
}
