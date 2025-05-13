namespace HelloMvs.Models
{
    /// <summary>
    /// Temel Öğrenci modeli.
    /// AJAX çağrılarında JSON olarak transfer edilebilir.
    /// </summary>
    public class Ogrenci
    {
        public int OgrenciId { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;
    }
}