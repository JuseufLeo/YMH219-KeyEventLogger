namespace KeyEventLogger
{
    /// <summary>
    /// Ana pencere. Formun KeyDown/KeyUp olaylarını KeyLogger sınıfına iletir ve
    /// kayıtları, kayıt durumunu ve istatistikleri ekranda gösterir.
    /// KeyPreview = true olduğu için form, içindeki hangi kontrol odakta olursa olsun tuşları önce kendisi alır.
    /// Bu olaylar sadece bu pencere aktifken gelir; başka programlardaki tuşlar yakalanmaz.
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly KeyLogger logger = new KeyLogger();

        public MainForm()
        {
            InitializeComponent();
            logger.EntryAdded += Logger_EntryAdded;
            logger.RecordingChanged += (s, e) => UpdateStatus();
            UpdateStatus();
            UpdateStats();
        }

        // ---------- Klavye olayları ----------

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            logger.Record(e.KeyCode, e.Modifiers, KeyEventType.KeyDown);
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            logger.Record(e.KeyCode, e.Modifiers, KeyEventType.KeyUp);
        }

        private void Logger_EntryAdded(object sender, KeyLogEntry entry)
        {
            lstLog.Items.Add(entry.ToString());
            lstLog.TopIndex = lstLog.Items.Count - 1;   // en son kayıt görünsün
            UpdateStats();
        }

        // ---------- Butonlar ----------

        private void BtnStart_Click(object sender, EventArgs e)
        {
            logger.Start();
            txtInput.Focus();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            logger.Stop();
            txtInput.Focus();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            logger.Clear();
            lstLog.Items.Clear();
            txtInput.Clear();
            UpdateStats();
            txtInput.Focus();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (logger.Entries.Count == 0)
            {
                MessageBox.Show("Kaydedilecek kayıt yok. Önce Başlat'a basıp birkaç tuşa basın.",
                    "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            saveFileDialog.FileName = $"tus_kayitlari_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                LogFileWriter.Save(saveFileDialog.FileName, logger.Entries);
                MessageBox.Show($"{logger.Entries.Count} kayıt dosyaya yazıldı:\n{saveFileDialog.FileName}",
                    "Kaydedildi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                MessageBox.Show("Dosya kaydedilemedi:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            txtInput.Focus();
        }

        // ---------- Arayüz güncelleme ----------

        /// <summary>Kayıt göstergesini ve butonların aktifliğini günceller.</summary>
        private void UpdateStatus()
        {
            if (logger.IsRecording)
            {
                lblStatus.Text = "● KAYIT AÇIK";
                lblStatus.BackColor = Color.FromArgb(46, 125, 50);
            }
            else
            {
                lblStatus.Text = "● Kayıt kapalı";
                lblStatus.BackColor = Color.FromArgb(158, 158, 158);
            }
            btnStart.Enabled = !logger.IsRecording;
            btnStop.Enabled = logger.IsRecording;
            // Basılamayan buton gri görünsün
            btnStart.BackColor = btnStart.Enabled ? Color.FromArgb(46, 125, 50) : Color.Silver;
            btnStop.BackColor = btnStop.Enabled ? Color.FromArgb(198, 40, 40) : Color.Silver;
        }

        private void UpdateStats()
        {
            lblTotal.Text = $"Toplam tuş basma: {logger.TotalKeyPresses}";
            lblEvents.Text = $"Toplam olay: {logger.Entries.Count}";

            lstTop.Items.Clear();
            int rank = 1;
            foreach (var pair in logger.MostUsedKeys(10))
                lstTop.Items.Add($"{rank++,2}. {pair.Key,-12} {pair.Value,4}");
        }
    }
}
