namespace KeyEventLogger
{
    partial class MainForm
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
            pnlTop = new Panel();
            btnStart = new Button();
            btnStop = new Button();
            btnClear = new Button();
            btnSave = new Button();
            lblStatus = new Label();
            lblInputTitle = new Label();
            txtInput = new TextBox();
            lblLogTitle = new Label();
            lstLog = new ListBox();
            grpStats = new GroupBox();
            lblTotal = new Label();
            lblEvents = new Label();
            lblTopTitle = new Label();
            lstTop = new ListBox();
            lblInfo = new Label();
            saveFileDialog = new SaveFileDialog();
            pnlTop.SuspendLayout();
            grpStats.SuspendLayout();
            SuspendLayout();
            //
            // pnlTop
            //
            pnlTop.BackColor = Color.FromArgb(240, 242, 245);
            pnlTop.Controls.Add(btnStart);
            pnlTop.Controls.Add(btnStop);
            pnlTop.Controls.Add(btnClear);
            pnlTop.Controls.Add(btnSave);
            pnlTop.Controls.Add(lblStatus);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(860, 60);
            //
            // btnStart
            //
            btnStart.BackColor = Color.FromArgb(46, 125, 50);
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStart.ForeColor = Color.White;
            btnStart.Location = new Point(12, 12);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(110, 36);
            btnStart.TabStop = false;
            btnStart.Text = "▶ Başlat";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += BtnStart_Click;
            //
            // btnStop
            //
            btnStop.BackColor = Color.FromArgb(198, 40, 40);
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStop.ForeColor = Color.White;
            btnStop.Location = new Point(132, 12);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(110, 36);
            btnStop.TabStop = false;
            btnStop.Text = "■ Durdur";
            btnStop.UseVisualStyleBackColor = false;
            btnStop.Click += BtnStop_Click;
            //
            // btnClear
            //
            btnClear.Font = new Font("Segoe UI", 10F);
            btnClear.Location = new Point(252, 12);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(110, 36);
            btnClear.TabStop = false;
            btnClear.Text = "Temizle";
            btnClear.Click += BtnClear_Click;
            //
            // btnSave
            //
            btnSave.Font = new Font("Segoe UI", 10F);
            btnSave.Location = new Point(372, 12);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(130, 36);
            btnSave.TabStop = false;
            btnSave.Text = "Kaydet (.txt)";
            btnSave.Click += BtnSave_Click;
            //
            // lblStatus
            //
            lblStatus.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblStatus.ForeColor = Color.White;
            lblStatus.Location = new Point(620, 12);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(225, 36);
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblInputTitle
            //
            lblInputTitle.AutoSize = true;
            lblInputTitle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblInputTitle.Location = new Point(12, 72);
            lblInputTitle.Name = "lblInputTitle";
            lblInputTitle.Text = "Deneme alanı (buraya yazabilirsiniz):";
            //
            // txtInput
            //
            txtInput.AcceptsReturn = true;
            txtInput.AcceptsTab = true;
            txtInput.Font = new Font("Segoe UI", 10F);
            txtInput.Location = new Point(12, 94);
            txtInput.Multiline = true;
            txtInput.Name = "txtInput";
            txtInput.ScrollBars = ScrollBars.Vertical;
            txtInput.Size = new Size(560, 80);
            txtInput.TabIndex = 0;
            //
            // lblLogTitle
            //
            lblLogTitle.AutoSize = true;
            lblLogTitle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblLogTitle.Location = new Point(12, 186);
            lblLogTitle.Name = "lblLogTitle";
            lblLogTitle.Text = "Kayıtlar (zaman · olay · tuş):";
            //
            // lstLog
            //
            lstLog.Font = new Font("Consolas", 10F);
            lstLog.HorizontalScrollbar = true;
            lstLog.IntegralHeight = false;
            lstLog.Location = new Point(12, 208);
            lstLog.Name = "lstLog";
            lstLog.Size = new Size(560, 300);
            lstLog.TabStop = false;
            //
            // grpStats
            //
            grpStats.Controls.Add(lblTotal);
            grpStats.Controls.Add(lblEvents);
            grpStats.Controls.Add(lblTopTitle);
            grpStats.Controls.Add(lstTop);
            grpStats.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            grpStats.Location = new Point(590, 72);
            grpStats.Name = "grpStats";
            grpStats.Size = new Size(255, 436);
            grpStats.Text = "İstatistik";
            //
            // lblTotal
            //
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 10F);
            lblTotal.Location = new Point(12, 30);
            lblTotal.Name = "lblTotal";
            //
            // lblEvents
            //
            lblEvents.AutoSize = true;
            lblEvents.Font = new Font("Segoe UI", 10F);
            lblEvents.Location = new Point(12, 56);
            lblEvents.Name = "lblEvents";
            //
            // lblTopTitle
            //
            lblTopTitle.AutoSize = true;
            lblTopTitle.Location = new Point(12, 92);
            lblTopTitle.Name = "lblTopTitle";
            lblTopTitle.Text = "En sık kullanılan tuşlar:";
            //
            // lstTop
            //
            lstTop.Font = new Font("Consolas", 10F);
            lstTop.IntegralHeight = false;
            lstTop.Location = new Point(12, 116);
            lstTop.Name = "lstTop";
            lstTop.Size = new Size(230, 305);
            lstTop.TabStop = false;
            //
            // lblInfo
            //
            lblInfo.ForeColor = Color.DimGray;
            lblInfo.Location = new Point(12, 518);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(833, 22);
            lblInfo.Text = "Bu uygulama yalnızca kendi penceresi aktifken basılan tuşları kaydeder. Arka planda çalışmaz, başka programları dinlemez, kayıtları hiçbir yere göndermez.";
            //
            // saveFileDialog
            //
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "Metin dosyası (*.txt)|*.txt";
            saveFileDialog.Title = "Kayıtları kaydet";
            //
            // MainForm
            //
            AutoScaleMode = AutoScaleMode.Font;
            AutoScaleDimensions = new SizeF(7F, 15F);
            ClientSize = new Size(860, 548);
            Controls.Add(pnlTop);
            Controls.Add(lblInputTitle);
            Controls.Add(txtInput);
            Controls.Add(lblLogTitle);
            Controls.Add(lstLog);
            Controls.Add(grpStats);
            Controls.Add(lblInfo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Key Event Logger - YMH219";
            KeyDown += MainForm_KeyDown;
            KeyUp += MainForm_KeyUp;
            pnlTop.ResumeLayout(false);
            grpStats.ResumeLayout(false);
            grpStats.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private Panel pnlTop;
        private Button btnStart;
        private Button btnStop;
        private Button btnClear;
        private Button btnSave;
        private Label lblStatus;
        private Label lblInputTitle;
        private TextBox txtInput;
        private Label lblLogTitle;
        private ListBox lstLog;
        private GroupBox grpStats;
        private Label lblTotal;
        private Label lblEvents;
        private Label lblTopTitle;
        private ListBox lstTop;
        private Label lblInfo;
        private SaveFileDialog saveFileDialog;
    }
}
