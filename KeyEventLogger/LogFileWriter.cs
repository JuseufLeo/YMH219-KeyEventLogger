namespace KeyEventLogger
{
    /// <summary>
    /// Kayıtları yerel bir .txt dosyasına yazar. Kayıtlar yalnızca kullanıcının seçtiği dosyaya yazılır,
    /// hiçbir yere gönderilmez.
    /// </summary>
    public static class LogFileWriter
    {
        public static void Save(string path, IEnumerable<KeyLogEntry> entries)
        {
            using var writer = new StreamWriter(path, false, System.Text.Encoding.UTF8);
            writer.WriteLine("Key Event Logger - YMH219");
            writer.WriteLine($"Kaydedilme zamanı: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
            writer.WriteLine(new string('-', 50));
            foreach (KeyLogEntry entry in entries)
                writer.WriteLine(entry.ToString());
        }
    }
}
