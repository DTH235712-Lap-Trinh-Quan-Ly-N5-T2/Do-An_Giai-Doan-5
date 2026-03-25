namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmKanban
    {
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

        private void InitializeComponent()
        {
            this.tlpBoard = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTodo = new System.Windows.Forms.Panel();
            this.flpTodo = new TaskFlowManagement.WinForms.Forms.DoubleBufferedFlowLayoutPanel();
            this.lblTodo = new System.Windows.Forms.Label();
            this.pnlInProgress = new System.Windows.Forms.Panel();
            this.flpInProgress = new TaskFlowManagement.WinForms.Forms.DoubleBufferedFlowLayoutPanel();
            this.lblInProgress = new System.Windows.Forms.Label();
            this.pnlReview = new System.Windows.Forms.Panel();
            this.flpReview = new TaskFlowManagement.WinForms.Forms.DoubleBufferedFlowLayoutPanel();
            this.lblReview = new System.Windows.Forms.Label();
            this.pnlTesting = new System.Windows.Forms.Panel();
            this.flpTesting = new TaskFlowManagement.WinForms.Forms.DoubleBufferedFlowLayoutPanel();
            this.lblTesting = new System.Windows.Forms.Label();
            this.pnlFailed = new System.Windows.Forms.Panel();
            this.flpFailed = new TaskFlowManagement.WinForms.Forms.DoubleBufferedFlowLayoutPanel();
            this.lblFailed = new System.Windows.Forms.Label();
            this.pnlDone = new System.Windows.Forms.Panel();
            this.flpDone = new TaskFlowManagement.WinForms.Forms.DoubleBufferedFlowLayoutPanel();
            this.lblDone = new System.Windows.Forms.Label();
            this.tlpBoard.SuspendLayout();
            this.pnlTodo.SuspendLayout();
            this.pnlInProgress.SuspendLayout();
            this.pnlReview.SuspendLayout();
            this.pnlTesting.SuspendLayout();
            this.pnlFailed.SuspendLayout();
            this.pnlDone.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpBoard
            // 
            this.tlpBoard.ColumnCount = 6;
            this.tlpBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpBoard.Controls.Add(this.pnlTodo, 0, 0);
            this.tlpBoard.Controls.Add(this.pnlInProgress, 1, 0);
            this.tlpBoard.Controls.Add(this.pnlReview, 2, 0);
            this.tlpBoard.Controls.Add(this.pnlTesting, 3, 0);
            this.tlpBoard.Controls.Add(this.pnlFailed, 4, 0);
            this.tlpBoard.Controls.Add(this.pnlDone, 5, 0);
            this.tlpBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBoard.Location = new System.Drawing.Point(0, 0);
            this.tlpBoard.Name = "tlpBoard";
            this.tlpBoard.Padding = new System.Windows.Forms.Padding(12);
            this.tlpBoard.RowCount = 1;
            this.tlpBoard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBoard.Size = new System.Drawing.Size(1184, 761);
            this.tlpBoard.TabIndex = 0;
            // 
            // pnlTodo
            // 
            this.pnlTodo.Controls.Add(this.flpTodo);
            this.pnlTodo.Controls.Add(this.lblTodo);
            this.pnlTodo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTodo.Location = new System.Drawing.Point(15, 15);
            this.pnlTodo.Name = "pnlTodo";
            this.pnlTodo.Padding = new System.Windows.Forms.Padding(8);
            this.pnlTodo.Size = new System.Drawing.Size(187, 731);
            this.pnlTodo.TabIndex = 0;
            // 
            // flpTodo
            // 
            this.flpTodo.AutoScroll = true;
            this.flpTodo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flpTodo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTodo.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpTodo.Location = new System.Drawing.Point(8, 56);
            this.flpTodo.Name = "flpTodo";
            this.flpTodo.Padding = new System.Windows.Forms.Padding(8);
            this.flpTodo.Size = new System.Drawing.Size(171, 667);
            this.flpTodo.TabIndex = 1;
            this.flpTodo.WrapContents = false;
            // 
            // lblTodo
            // 
            this.lblTodo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTodo.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTodo.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblTodo.Location = new System.Drawing.Point(8, 8);
            this.lblTodo.Name = "lblTodo";
            this.lblTodo.Size = new System.Drawing.Size(171, 48);
            this.lblTodo.TabIndex = 0;
            this.lblTodo.Text = "To Do";
            this.lblTodo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlInProgress
            // 
            this.pnlInProgress.Controls.Add(this.flpInProgress);
            this.pnlInProgress.Controls.Add(this.lblInProgress);
            this.pnlInProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInProgress.Location = new System.Drawing.Point(205, 15);
            this.pnlInProgress.Name = "pnlInProgress";
            this.pnlInProgress.Padding = new System.Windows.Forms.Padding(8);
            this.pnlInProgress.Size = new System.Drawing.Size(187, 731);
            this.pnlInProgress.TabIndex = 1;
            // 
            // flpInProgress
            // 
            this.flpInProgress.AutoScroll = true;
            this.flpInProgress.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flpInProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpInProgress.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpInProgress.Location = new System.Drawing.Point(8, 56);
            this.flpInProgress.Name = "flpInProgress";
            this.flpInProgress.Padding = new System.Windows.Forms.Padding(8);
            this.flpInProgress.Size = new System.Drawing.Size(171, 667);
            this.flpInProgress.TabIndex = 2;
            this.flpInProgress.WrapContents = false;
            // 
            // lblInProgress
            // 
            this.lblInProgress.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInProgress.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblInProgress.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblInProgress.Location = new System.Drawing.Point(8, 8);
            this.lblInProgress.Name = "lblInProgress";
            this.lblInProgress.Size = new System.Drawing.Size(171, 48);
            this.lblInProgress.TabIndex = 1;
            this.lblInProgress.Text = "In Progress";
            this.lblInProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlReview
            // 
            this.pnlReview.Controls.Add(this.flpReview);
            this.pnlReview.Controls.Add(this.lblReview);
            this.pnlReview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReview.Location = new System.Drawing.Point(395, 15);
            this.pnlReview.Name = "pnlReview";
            this.pnlReview.Padding = new System.Windows.Forms.Padding(8);
            this.pnlReview.Size = new System.Drawing.Size(187, 731);
            this.pnlReview.TabIndex = 2;
            // 
            // flpReview
            // 
            this.flpReview.AutoScroll = true;
            this.flpReview.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flpReview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpReview.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpReview.Location = new System.Drawing.Point(8, 56);
            this.flpReview.Name = "flpReview";
            this.flpReview.Padding = new System.Windows.Forms.Padding(8);
            this.flpReview.Size = new System.Drawing.Size(171, 667);
            this.flpReview.TabIndex = 2;
            this.flpReview.WrapContents = false;
            // 
            // lblReview
            // 
            this.lblReview.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReview.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblReview.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblReview.Location = new System.Drawing.Point(8, 8);
            this.lblReview.Name = "lblReview";
            this.lblReview.Size = new System.Drawing.Size(171, 48);
            this.lblReview.TabIndex = 1;
            this.lblReview.Text = "Review";
            this.lblReview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTesting
            // 
            this.pnlTesting.Controls.Add(this.flpTesting);
            this.pnlTesting.Controls.Add(this.lblTesting);
            this.pnlTesting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTesting.Location = new System.Drawing.Point(585, 15);
            this.pnlTesting.Name = "pnlTesting";
            this.pnlTesting.Padding = new System.Windows.Forms.Padding(8);
            this.pnlTesting.Size = new System.Drawing.Size(187, 731);
            this.pnlTesting.TabIndex = 3;
            // 
            // flpTesting
            // 
            this.flpTesting.AutoScroll = true;
            this.flpTesting.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flpTesting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTesting.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpTesting.Location = new System.Drawing.Point(8, 56);
            this.flpTesting.Name = "flpTesting";
            this.flpTesting.Padding = new System.Windows.Forms.Padding(8);
            this.flpTesting.Size = new System.Drawing.Size(171, 667);
            this.flpTesting.TabIndex = 2;
            this.flpTesting.WrapContents = false;
            // 
            // lblTesting
            // 
            this.lblTesting.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTesting.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTesting.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblTesting.Location = new System.Drawing.Point(8, 8);
            this.lblTesting.Name = "lblTesting";
            this.lblTesting.Size = new System.Drawing.Size(171, 48);
            this.lblTesting.TabIndex = 1;
            this.lblTesting.Text = "Testing";
            this.lblTesting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlFailed
            // 
            this.pnlFailed.Controls.Add(this.flpFailed);
            this.pnlFailed.Controls.Add(this.lblFailed);
            this.pnlFailed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFailed.Location = new System.Drawing.Point(775, 15);
            this.pnlFailed.Name = "pnlFailed";
            this.pnlFailed.Padding = new System.Windows.Forms.Padding(8);
            this.pnlFailed.Size = new System.Drawing.Size(187, 731);
            this.pnlFailed.TabIndex = 4;
            // 
            // flpFailed
            // 
            this.flpFailed.AutoScroll = true;
            this.flpFailed.BackColor = System.Drawing.Color.FromArgb(254, 242, 242);
            this.flpFailed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpFailed.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpFailed.Location = new System.Drawing.Point(8, 56);
            this.flpFailed.Name = "flpFailed";
            this.flpFailed.Padding = new System.Windows.Forms.Padding(8);
            this.flpFailed.Size = new System.Drawing.Size(171, 667);
            this.flpFailed.TabIndex = 2;
            this.flpFailed.WrapContents = false;
            // 
            // lblFailed
            // 
            this.lblFailed.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFailed.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblFailed.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            this.lblFailed.Location = new System.Drawing.Point(8, 8);
            this.lblFailed.Name = "lblFailed";
            this.lblFailed.Size = new System.Drawing.Size(171, 48);
            this.lblFailed.TabIndex = 1;
            this.lblFailed.Text = "Failed";
            this.lblFailed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlDone
            // 
            this.pnlDone.Controls.Add(this.flpDone);
            this.pnlDone.Controls.Add(this.lblDone);
            this.pnlDone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDone.Location = new System.Drawing.Point(965, 15);
            this.pnlDone.Name = "pnlDone";
            this.pnlDone.Padding = new System.Windows.Forms.Padding(8);
            this.pnlDone.Size = new System.Drawing.Size(204, 731);
            this.pnlDone.TabIndex = 5;
            // 
            // flpDone
            // 
            this.flpDone.AutoScroll = true;
            this.flpDone.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flpDone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpDone.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpDone.Location = new System.Drawing.Point(8, 56);
            this.flpDone.Name = "flpDone";
            this.flpDone.Padding = new System.Windows.Forms.Padding(8);
            this.flpDone.Size = new System.Drawing.Size(188, 667);
            this.flpDone.TabIndex = 2;
            this.flpDone.WrapContents = false;
            // 
            // lblDone
            // 
            this.lblDone.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDone.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblDone.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblDone.Location = new System.Drawing.Point(8, 8);
            this.lblDone.Name = "lblDone";
            this.lblDone.Size = new System.Drawing.Size(188, 48);
            this.lblDone.TabIndex = 1;
            this.lblDone.Text = "Done";
            this.lblDone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmKanban
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.tlpBoard);
            this.Name = "frmKanban";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kanban Board";
            this.tlpBoard.ResumeLayout(false);
            this.pnlTodo.ResumeLayout(false);
            this.pnlInProgress.ResumeLayout(false);
            this.pnlReview.ResumeLayout(false);
            this.pnlTesting.ResumeLayout(false);
            this.pnlFailed.ResumeLayout(false);
            this.pnlDone.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpBoard;
        private System.Windows.Forms.Panel pnlTodo;
        private System.Windows.Forms.Label lblTodo;
        private System.Windows.Forms.Panel pnlInProgress;
        private System.Windows.Forms.Label lblInProgress;
        private System.Windows.Forms.Panel pnlReview;
        private System.Windows.Forms.Label lblReview;
        private System.Windows.Forms.Panel pnlTesting;
        private System.Windows.Forms.Label lblTesting;
        private System.Windows.Forms.Panel pnlFailed;
        private System.Windows.Forms.Label lblFailed;
        private System.Windows.Forms.Panel pnlDone;
        private System.Windows.Forms.Label lblDone;
        private DoubleBufferedFlowLayoutPanel flpTodo;
        private DoubleBufferedFlowLayoutPanel flpInProgress;
        private DoubleBufferedFlowLayoutPanel flpReview;
        private DoubleBufferedFlowLayoutPanel flpTesting;
        private DoubleBufferedFlowLayoutPanel flpFailed;
        private DoubleBufferedFlowLayoutPanel flpDone;
    }
}