namespace KeyEventLogger
{
    /// <summary>
    /// Tek bir klavye olayının kaydı: ne zaman, hangi tuş, hangi olay ve hangi yardımcı tuşlar (Ctrl/Shift/Alt).
    /// </summary>
    public class KeyLogEntry
    {
        public DateTime Time { get; }
        public Keys Key { get; }
        public Keys Modifiers { get; }
        public KeyEventType EventType { get; }

        public KeyLogEntry(DateTime time, Keys key, Keys modifiers, KeyEventType eventType)
        {
            Time = time;
            Key = key;
            Modifiers = modifiers;
            EventType = eventType;
        }

        /// <summary>Tuşun okunabilir adı, örn. "Ctrl + Shift + A".</summary>
        public string KeyName
        {
            get
            {
                string name = FriendlyName(Key);
                // Yardımcı tuşun kendisine basıldıysa (ör. sadece Shift) tekrar "Shift +" yazma
                if (Modifiers.HasFlag(Keys.Shift) && !IsModifierKey(Key)) name = "Shift + " + name;
                if (Modifiers.HasFlag(Keys.Alt) && !IsModifierKey(Key)) name = "Alt + " + name;
                if (Modifiers.HasFlag(Keys.Control) && !IsModifierKey(Key)) name = "Ctrl + " + name;
                return name;
            }
        }

        /// <summary>Bazı tuş adlarını daha okunur hâle getirir (ör. D2 -> 2, ShiftKey -> Shift).</summary>
        public static string FriendlyName(Keys key)
        {
            if (key >= Keys.D0 && key <= Keys.D9)
                return ((int)(key - Keys.D0)).ToString();
            if (key >= Keys.NumPad0 && key <= Keys.NumPad9)
                return "Num " + (int)(key - Keys.NumPad0);
            return key switch
            {
                Keys.ShiftKey or Keys.LShiftKey or Keys.RShiftKey => "Shift",
                Keys.ControlKey or Keys.LControlKey or Keys.RControlKey => "Ctrl",
                Keys.Menu or Keys.LMenu or Keys.RMenu => "Alt",
                Keys.Back => "Backspace",
                Keys.Capital => "CapsLock",
                Keys.Next => "PageDown",
                _ => key.ToString()
            };
        }

        private static bool IsModifierKey(Keys key) =>
            key is Keys.ShiftKey or Keys.LShiftKey or Keys.RShiftKey
                or Keys.ControlKey or Keys.LControlKey or Keys.RControlKey
                or Keys.Menu or Keys.LMenu or Keys.RMenu;

        /// <summary>Ekranda ve dosyada kullanılan kayıt satırı.</summary>
        public override string ToString() =>
            $"[{Time:dd.MM.yyyy HH:mm:ss.fff}]  {EventType,-7}  {KeyName}";
    }
}
