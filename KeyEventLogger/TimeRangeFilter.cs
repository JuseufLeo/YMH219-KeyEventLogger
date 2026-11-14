namespace KeyEventLogger
{
    /// <summary>
    /// Kayıtları zamana göre süzmek için kullanılan aralık seçeneği (ör. "Son 1 dakika").
    /// Duration null ise filtre uygulanmaz, tüm kayıtlar gösterilir.
    /// </summary>
    public class TimeRangeFilter
    {
        public string Name { get; }
        public TimeSpan? Duration { get; }

        public TimeRangeFilter(string name, TimeSpan? duration)
        {
            Name = name;
            Duration = duration;
        }

        /// <summary>Formdaki açılır listede gösterilecek hazır seçenekler.</summary>
        public static readonly TimeRangeFilter[] Options =
        {
            new TimeRangeFilter("Tüm kayıtlar", null),
            new TimeRangeFilter("Son 10 saniye", TimeSpan.FromSeconds(10)),
            new TimeRangeFilter("Son 30 saniye", TimeSpan.FromSeconds(30)),
            new TimeRangeFilter("Son 1 dakika", TimeSpan.FromMinutes(1)),
            new TimeRangeFilter("Son 5 dakika", TimeSpan.FromMinutes(5)),
            new TimeRangeFilter("Son 15 dakika", TimeSpan.FromMinutes(15)),
        };

        public bool IsAll => Duration == null;

        /// <summary>Kayıt, şu andan geriye doğru seçili süre içinde mi?</summary>
        public bool Matches(KeyLogEntry entry, DateTime now) =>
            Duration == null || entry.Time >= now - Duration.Value;

        /// <summary>Listeden sadece aralığa uyan kayıtları döndürür.</summary>
        public List<KeyLogEntry> Apply(IEnumerable<KeyLogEntry> entries)
        {
            DateTime now = DateTime.Now;
            return entries.Where(e => Matches(e, now)).ToList();
        }

        // ComboBox bu metni gösterir
        public override string ToString() => Name;
    }
}
