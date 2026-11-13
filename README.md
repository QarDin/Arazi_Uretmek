# Arazi Üretmek

Sadece C# ve Raylib ile rastgele sayılardan dağlı bir yeryüzü üretme denemesi. YouTube videosunun kodu.

Adımlar (videodaki halkalar):
1. Izgaradaki her noktaya rastgele yükseklik ver
2. Komşu noktaların farkını sınırla
3. Her 5 noktada bir rastgele yükseklik seç, aralarını S eğrisiyle doldur
4. Büyük şekillerin üstüne küçük katman ekle
5. Mesh ile çiz
6. Yüksekliğe göre renk ver

## Çalıştırma
```
dotnet run
```
Gereksinim: .NET 10 SDK. Raylib-cs paketi otomatik iner.
