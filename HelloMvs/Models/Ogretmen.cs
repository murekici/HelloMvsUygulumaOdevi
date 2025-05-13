namespace HelloMvs.Models
{
    /// <summary>
    /// Temel Öğretmen modeli.
    /// Öğrenci detaylarında ilişkilendirilebilir.
    /// </summary>
    public class Ogretmen
    {
        public int OgretmenID { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;
    }
}
