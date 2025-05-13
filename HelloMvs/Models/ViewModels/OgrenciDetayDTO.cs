namespace HelloMvs.Models.ViewModels
{
    /// <summary>
    /// Öğrenci ve ilgili Öğretmen bilgisini JSON olarak taşıyan DTO.
    /// AJAX tabanlı formlarda ve client-side işlemlerde kullanılır.
    /// </summary>
    public class OgrenciDetayDTO
    {
        // Öğrenci bilgileri
        public int OgrenciId { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;

        // Öğretmen bilgileri
        public int OgretmenID { get; set; }
        public string OgretmenAd { get; set; } = string.Empty;
        public string OgretmenSoyad { get; set; } = string.Empty;
    }
}