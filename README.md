# Komut Satırı Baslatıcı
Genel Amaçlı Komut Satırından Belirli Bir İze Sahip Uygulamayı Çalıştırma Uygulaması ArgeMup@yandex.com

### Girdiler
     P1 : E=Hemen kapansin H=Bitmesini beklesin
     P2 : E=Önyüzü göster  H=Önyüzü gizle
     P3 : * ile ayrılmış çalıştırılacak dosya
     P4 ve digerleri : * ile ayrılmış çalıştırılacak dosyaya aktarılacak parametreler

     P3 ve sonrası için geçerli olarak
     *KLASOR* ile başlayan satırların ilgili dosyasnın klasörünü getirir
     *DOSYAADI* ile başlayan satırların ilgili dosyasnın sadece dosya adını ve soyadını getirir

### Örnekler
    $REPO = Calistirilacak dosyanın bulunma ihtimalinin oldugu klasörlerden biri olabilir
    Baslatici.exe E E "$REPO*.PrjPcb"
    Baslatici.exe E E "$REPO*.kicad_pro"
    Baslatici.exe E E "$REPO*.sln"
    Baslatici.exe E E "C:\\Program Files\\Microsoft VS Code\\Code.exe" "*KLASOR*$REPO*platformio.ini"
