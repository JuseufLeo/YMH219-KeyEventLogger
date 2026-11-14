namespace KeyEventLogger
{
    /// <summary>
    /// Kayıt durumunu (açık/kapalı) ve kayıtların listesini yönetir, istatistik üretir.
    /// Form sadece olayları bu sınıfa iletir.
    /// </summary>
    public class KeyLogger
    {
        private readonly List<KeyLogEntry> entries = new List<KeyLogEntry>();

        public bool IsRecording { get; private set; }
        public IReadOnlyList<KeyLogEntry> Entries => entries;

        /// <summary>Yeni bir kayıt eklendiğinde tetiklenir (arayüzü güncellemek için).</summary>
        public event EventHandler<KeyLogEntry> EntryAdded;

        /// <summary>Kayıt açılıp kapandığında tetiklenir.</summary>
        public event EventHandler RecordingChanged;

        public void Start()
        {
            IsRecording = true;
            RecordingChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Stop()
        {
            IsRecording = false;
            RecordingChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Clear()
        {
            entries.Clear();
        }

        /// <summary>Kayıt açıksa olayı listeye ekler; kapalıysa hiçbir şey yapmaz.</summary>
        public void Record(Keys key, Keys modifiers, KeyEventType type)
        {
            if (!IsRecording)
                return;
            var entry = new KeyLogEntry(DateTime.Now, key, modifiers, type);
            entries.Add(entry);
            EntryAdded?.Invoke(this, entry);
        }

        /// <summary>Toplam tuş basma sayısı (sadece KeyDown olayları sayılır).</summary>
        public int TotalKeyPresses => CountKeyPresses(entries);

        /// <summary>Verilen kayıtlardaki tuş basma (KeyDown) sayısı.</summary>
        public static int CountKeyPresses(IEnumerable<KeyLogEntry> source) =>
            source.Count(e => e.EventType == KeyEventType.KeyDown);

        /// <summary>Tüm kayıtlarda en sık basılan tuşlar.</summary>
        public List<KeyValuePair<string, int>> MostUsedKeys(int count) => MostUsedKeys(entries, count);

        /// <summary>Verilen kayıtlarda en sık basılan tuşlar ve kaçar kez basıldıkları.</summary>
        public static List<KeyValuePair<string, int>> MostUsedKeys(IEnumerable<KeyLogEntry> source, int count)
        {
            var counts = new Dictionary<string, int>();
            foreach (KeyLogEntry e in source)
            {
                if (e.EventType != KeyEventType.KeyDown)
                    continue;
                string key = KeyLogEntry.FriendlyName(e.Key);
                counts[key] = counts.TryGetValue(key, out int c) ? c + 1 : 1;
            }
            return counts.OrderByDescending(p => p.Value).Take(count).ToList();
        }
    }
}
