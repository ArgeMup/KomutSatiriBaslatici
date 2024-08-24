using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;

namespace Baslatici // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static int Main(string[] BaşlangicParametreleri)
        {
            W32_Konsol.AyrıPenceredeGöster(false); //gizler

            List<string> l = BaşlangicParametreleri == null ? new List<string>() : BaşlangicParametreleri.ToList();
            
            if (l.Count < 1) goto Hata;
            bool HemenKapansın = l.First() == "E";
            l.RemoveAt(0);

            if (l.Count < 1) goto Hata;
            bool ÖnyüzüGöster = l.First() == "E";
            l.RemoveAt(0);

            if (l.Count < 1) goto Hata;
            string ÇalıştırılacakDosya = YazıyıAyıkla(l.First());
            if (ÇalıştırılacakDosya.BoşMu(true)) goto Hata;
            l.RemoveAt(0);

            for (int i = 0; i < l.Count(); i++)
            {
                l[i] = YazıyıAyıkla(l[i]);
            }

            Çalıştır_ Çalıştır = new Çalıştır_();
            Process uygulama = null;
            if (ÇalıştırılacakDosya.ToLower().EndsWith(".exe")) uygulama = Çalıştır.UygulamayıDoğrudanÇalıştır(ÇalıştırılacakDosya, l.ToArray(), !ÖnyüzüGöster, !HemenKapansın);
            else uygulama = Çalıştır.UygulamayaİşletimSistemiKararVersin(ÇalıştırılacakDosya, l.ToArray(), !ÖnyüzüGöster, !HemenKapansın);

            if (HemenKapansın) return 0;

            if (uygulama == null)
            {
                Günlük.Ekle("Uygulama oluşturulamadı " + ÇalıştırılacakDosya);
                goto Hata;
            }

            System.Threading.Thread.Sleep(15);
            if (!uygulama.HasExited) uygulama.WaitForExit();
            return uygulama.ExitCode;

            Hata:
            Günlük.Ekle(
                "Parametreleri kontrol ediniz " + Environment.NewLine + Environment.NewLine +
                "P1 : E=Hemen kapansin H=Bitmesini beklesin" + Environment.NewLine +
                "P2 : E=Önyüzü göster  H=Önyüzü gizle" + Environment.NewLine +
                "P3 : * ile ayrılmış çalıştırılacak dosya" + Environment.NewLine +
                "P4 ve digerleri : * ile ayrılmış çalıştırılacak dosyaya aktarılacak parametreler" + Environment.NewLine + Environment.NewLine +
                "P3 ve sonrası için geçerli olarak" + Environment.NewLine +
                "*KLASOR* ile başlayan satırların ilgili dosyasnın klasörünü getirir" + Environment.NewLine +
                "*DOSYAADI* ile başlayan satırların ilgili dosyasnın sadece dosya adını ve soyadını getirir");
            return -1;
        }

        static string YazıyıAyıkla(string Girdi)
        {
            if (Girdi.BoşMu(true)) return null;
            bool KLASOR = Girdi.StartsWith("*KLASOR*"); Girdi = Girdi.Replace("*KLASOR*", null);
            bool DOSYAADI = Girdi.StartsWith("*DOSYAADI*"); Girdi = Girdi.Replace("*DOSYAADI*", null);

            Girdi = Girdi.Trim();

            int konum = Girdi.IndexOf('*');
            if (konum >= 0)
            {
                string kls = Girdi.Substring(0, konum);
                string[] dsy_lar = Temkinli.Klasör.Listele_Dosya(kls);

                foreach (string dsy in dsy_lar)
                {
                    if (dsy.BenzerMi(Girdi, false))
                    {
                        Girdi = dsy;
                        goto Devam;
                    }
                };

                Girdi = null; //bulunamadı
            }

            Devam:
            if (Girdi.DoluMu())
            {
                if (KLASOR) Girdi = Dosya.Klasörü(Girdi);
                else if (DOSYAADI) Girdi = Path.GetFileName(Girdi);
            }

            return Girdi;
        }
    }
}