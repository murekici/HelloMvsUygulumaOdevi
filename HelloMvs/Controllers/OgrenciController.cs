using HelloMvs.Data;
using HelloMvs.Extensions;
using HelloMvs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace HelloMvs.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OgrenciController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ViewResult Index()
        {
            return View("AnaSayfa");
        }

        public ViewResult OgrenciListe()
        {
            List<Ogrenci> ogrenciler = _context.Ogrenciler.ToList();
            return View(ogrenciler);
        }

        [HttpGet]
        public ViewResult OgrenciEkle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OgrenciEkle(Ogrenci ogr)
        {
            int sonuc = 0;
            if (ogr != null)
            {
                _context.Ogrenciler.Add(ogr);
                sonuc = _context.SaveChanges();
            }

            // Ajax isteği olup olmadığını kontrol et
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                if (sonuc > 0)
                {
                    return Json(new { success = true, message = "Öğrenci başarıyla eklendi", redirectUrl = "/Ogrenci/OgrenciListe" });
                }
                else
                {
                    return Json(new { success = false, message = "Öğrenci eklenirken bir hata oluştu" });
                }
            }
            else
            {
                // Normal form gönderiminde eski davranışı koru
                if (sonuc > 0)
                {
                    TempData["sonuc"] = true;
                }
                else
                {
                    TempData["sonuc"] = false;
                }
                return View();
            }
        }

        [HttpGet]
        public IActionResult OgrenciDetay(int id)
        {
            var ogr = _context.Ogrenciler.Find(id);

            // Ajax isteği kontrolü
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_OgrenciDetayModal", ogr);
            }

            return View(ogr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OgrenciDetay(Ogrenci ogr)
        {
            _context.Entry(ogr).State = EntityState.Modified;
            int sonuc = _context.SaveChanges();

            // Ajax isteği kontrolü
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                if (sonuc > 0)
                {
                    // Güncel tabloyu döndür
                    var ogrenciler = _context.Ogrenciler.ToList();
                    var tableHtml = await this.RenderViewToStringAsync("_OgrenciTabloPartial", ogrenciler, true);

                    return Json(new
                    {
                        success = true,
                        message = "Öğrenci başarıyla güncellendi",
                        updateTable = true,
                        tableHtml = tableHtml
                    });
                }
                else
                {
                    return Json(new { success = false, message = "Öğrenci güncellenirken bir hata oluştu" });
                }
            }

            return RedirectToAction("OgrenciListe");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Ogrenci/OgrenciSil/{id}")]
        public async Task<IActionResult> OgrenciSil(int id)
        {
            var ogr = _context.Ogrenciler.Find(id);
            if (ogr != null)
            {
                _context.Ogrenciler.Remove(ogr);
                int sonuc = _context.SaveChanges();

                // Ajax isteği kontrolü
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    if (sonuc > 0)
                    {
                        // Güncel tabloyu döndür
                        var ogrenciler = _context.Ogrenciler.ToList();
                        var tableHtml = await this.RenderViewToStringAsync("_OgrenciTabloPartial", ogrenciler, true);

                        return Json(new
                        {
                            success = true,
                            message = "Öğrenci başarıyla silindi",
                            updateTable = true,
                            tableHtml = tableHtml
                        });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Öğrenci silinirken bir hata oluştu" });
                    }
                }
            }

            return RedirectToAction("OgrenciListe");
        }

        // Helper metodu artık Extensions içinde
    }
}