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
            cmbTimeRange.Items.AddRange(TimeRangeFilter.Options);
            cmbTimeRange.SelectedIndex = 0;   // başlangıçta tüm kayıtlar
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
            // Yeni kayıt her zaman "şimdi" olduğu için her aralığa uyar
            lstLog.Items.Add(entry.ToString());
            lstLog.TopIndex = lstLog.Items.Count - 1;   // en son kayıt görünsün
            UpdateStats();
        }

        // ---------- Zaman aralığı filtresi ----------

        private TimeRangeFilter SelectedFilter =>
            cmbTimeRange.SelectedItem as TimeRangeFilter ?? TimeRangeFilter.Options[0];

        /// <summary>Seçili aralığa uyan kayıtlar.</summary>
        private List<KeyLogEntry> VisibleEntries() => SelectedFilter.Apply(logger.Entries);

        private void CmbTimeRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            // "Son X saniye" seçiliyken zaman ilerledikçe eski kayıtlar listeden düşsün diye
            // her saniye kontrol eden zamanlayıcı çalışır; "Tüm kayıtlar"da gerek yok.
            filterTimer.Enabled = !SelectedFilter.IsAll;
            RefreshLogList();
            txtInput.Focus();
        }

        private void FilterTimer_Tick(object sender, EventArgs e)
        {
            // Aralık dışına çıkan kayıt varsa listeyi yeniden oluştur
            if (VisibleEntries().Count != lstLog.Items.Count)
                RefreshLogList();
        }

        /// <summary>Kayıt listesini seçili aralığa göre baştan doldurur.</summary>
        private void RefreshLogList()
        {
            lstLog.BeginUpdate();
            lstLog.Items.Clear();
            foreach (KeyLogEntry entry in VisibleEntries())
                lstLog.Items.Add(entry.ToString());
            lstLog.EndUpdate();
            if (lstLog.Items.Count > 0)
                lstLog.TopIndex = lstLog.Items.Count - 1;
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
            List<KeyLogEntry> toSave = VisibleEntries();   // seçili aralıktaki kayıtlar kaydedilir
            if (toSave.Count == 0)
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
                LogFileWriter.Save(saveFileDialog.FileName, toSave);
                MessageBox.Show($"{toSave.Count} kayıt ({SelectedFilter.Name.ToLower()}) dosyaya yazıldı:\n{saveFileDialog.FileName}",
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
            // İstatistikler de seçili zaman aralığına göre hesaplanır
            List<KeyLogEntry> visible = VisibleEntries();
            lblTotal.Text = $"Toplam tuş basma: {KeyLogger.CountKeyPresses(visible)}";
            lblEvents.Text = SelectedFilter.IsAll
                ? $"Toplam olay: {visible.Count}"
                : $"Olay: {visible.Count} / {logger.Entries.Count}";

            lstTop.Items.Clear();
            int rank = 1;
            foreach (var pair in KeyLogger.MostUsedKeys(visible, 10))
                lstTop.Items.Add($"{rank++,2}. {pair.Key,-12} {pair.Value,4}");
        }
    }
}
