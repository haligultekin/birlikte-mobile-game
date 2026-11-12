# Birlikte Mobil Oyun Geliştirme — Proje İncelemesi

**Oyun:** Project Astra: Nexus (çalışma adı)
**Kaynak:** https://share.gemini.google/lfdGL0v99yPc — "Otomatik" Gemini hesabıyla
yapılmış tasarım konuşması (11–12 Kasım 2026)
**Durum:** 🟢 Tasarım aşaması tamamlandı — kodlama başlamadı
**İlgili dosya:** [design-notes.md](design-notes.md) — konuşmadan damıtılmış tam tasarım

---

## Kısa Özet

StarCraft görselliğinde, Clash of Clans oynanışında mobil sci-fi strateji. 4 asimetrik
faction (Supplier / Warrior / Scientist / Engineer) + 5. rol (Auctioneer) birbirine
kilitlenmiş bir üretim zinciriyle; 3 ana kaynak (Kristal, Kromit, Gaz); HoMM tarzı günlük
6 turluk, 21 aylık harita seferi; Tower Defense katmanı; idle kazı; raid/filo savaşları;
adil (P2W'siz) bir ekonomi hedefi.

## Güçlü Yönler (samimi olarak)

1. **Üretim zinciri = asıl değer.** "Scientist blueprint çizer → Engineer alet yapar →
   Supplier hammadde çıkarır → Warrior zırh/filo yapar → eşsiz item birleşimi" kurgusu
   gerçekten iyi. Bu, Whiteout Survival'da ya da CoC'de olmayan şey — projenin tek
   özgün satış noktası (USP) burası ve doğru yerde duruyor.
2. **Anti-P2W disiplini tutarlı.** Aylık çekiliş limiti, catch-up paketinin sadece zaman
   kazandırması, Level 60 sonrası paket satılmaması — mobilde nadir görülen, pazarlanabilir
   bir duruş ("adil strateji oyunu" vaadi).
3. **Scarcity içgüdüsü doğru.** Blueprint sadece TD/boss'tan, %5 pazar vergisi, aylık
   limitler, gold sink'ler — kağıt üzerinde sağlıklı bir kapalı devre ekonomi.
4. **Retention iskeleti düzgün.** 6 tur/gün + 3 saat idle kazı + aylık harita + günlük
   craft drop + harita finali epik'leri = ders kitabına uygun oturum mimarisi.

## Riskler ve Açıklar (bunlar önemli)

1. **Kapsam patlaması — en büyük tehlike.** 4 asimetrik faction + auction house + staking +
   hero sistemi + 21 haritalık sezon + TD + raid + guild war + filo + idle kazı +
   Auctioneer kariyeri + 3 evren... Bu, 15-30 kişilik stüdyonun 2-4 yıllık işi. Henüz
   tek bir ekran bile yok. Tasarım güzel; ama bu haliyle **yapılamaz**.
2. **Ekonomi rakamları varsayım.** 5x/20x kaynak oranları, %5 vergi, %0.08 şans — hepsi
   sohbet anında uyduruldu. 5 rol × 3 kaynak × pazar × sink kombinasyonu denge
   simülasyonu gerektirir. Kod yazılmadan önce basit bir tablo simülasyonu (30 gün × 100
   oyuncu) şart; aksi halde bir musluk bozulur ve tüm zincir çöker.
3. **Kodlar oyun değil.** Sohbetteki C# parçaları derlenmiyor bile — gerçek dizim
   hataları var (`uretics void`, `phas FactionType.Supply`, bozuk string interpolasyonu)
   ve `UpdateMiningProgress()` gibi Unity'nin asla çağırmayacağı metotlar zamanlayıcıları
   fiilen çalıştırmaz. Bunlar **mantık taslakları**; UI, sunucu, kalıcılık, ağ katmanı yok.
   Değerli olan GDD'dir, kod değil — bu ayrım net yapılmalı.
4. **Loot box yasal riski.** Satın alınabilen Space Crystals + %0.08 efsanevi zar =
   paralı şans mekaniği. Belçika/Hollanda'da yasak, Türkiye'de de düzenlemeler
   sıkılaşıyor. Ya zar sadece ücretsiz olsun, ya hukuki gözden geçirme yapılsın.
5. **21 ay çok uzun.** Aylık haritalar tek başına yeterli motivasyon değil; 3 aylık
   mini-finaller (kendi unique drop'uyla) konulmalı.
6. **Yeni oyuncu hunisi ağır.** 5 bağımlı rol + karmaşık ekonomi ilk 10 dakikada
   anlatılamaz. Catch-up paketi ilerlemeyi çözer ama **anlamayı** çözmez; onboarding
   tek bir döngüyü (Supplier) öğretmeli.
7. **İtem matematiği netleşmedi (yeni).** 350 toplamı kategorileri toplamıyor: Gemini'nin
   kendi dökümü (100+162+60+11+27) **360 ediyor**; imbued = epic+taş mı (ayrı satır mı?),
   raid = combine çıktısı — hangisi 350'ye dahil belirsiz. Ayrıca "27 imbued" hem eşzamanlı
   slot sınırı hem faction-başı ustalık hedefi; toplam havuz 27 mi, 4×27 mi? CSV tabloları
   yazılmadan bu sayılar kitlenemez.
8. **God-roll olasılığı ters çalışıyor (yeni, hesapla doğrulandı).** Tasarım "Impossible
   çok nadir" diyor ama `base × U(1.0, 2.0)` + clamp matematiğinde: taban %1.6 → god-roll
   **%3.1**, taban %2.0 → **%42.5**, taban %3.0 → **%95**. Yani tabanı güçlü item neredeyse
   kesin "impossible" çıkıyor — niyetin tam tersi. Düzeltme: sabit dar şans bandı
   (ör. her item'da %1-2) veya tabana göre normalize edilmiş ayrı bir kalite zarı.
9. **İki kazı sistemi üst üste biniyor (yeni).** Eski 3 saatlik idle sefer + yeni günde
   12 kazı (2 saatte bir) aynı işi, aynı ödüllerle yapıyor. Hangisinin geçerli olduğu
   seçilmeli; ikisi birden = kaynak musluğu iki katı.
10. **Kod hâlâ derlenmiyor (yeni hatalarla).** `tdSuccessroll`/`tdSuccessRoll` büyük-küçük
    harf uyuşmazlığı, `'{item.itemName'` bozuk interpolasyon, trait'ler sabit (enabled ise
    her item aynı pasifi alıyor — varyans sadece ana stat'te, "build çeşitliliği" iddiası
    zayıf).

## Tavsiye (arkadaşa söylenecek özet)

1. **MVP'ye kesin:** tek faction (Supplier) + üs + işçi + Kromit + 6 turluk keşif +
   basit pazar. 1 çalışan döngü, 5 dokümante edilmiş döngüden değerlidir. Gerisi
   sonradan veri katmanı olarak eklenir.
2. **Önce tablo, sonra kod:** 30 günlük ekonomi simülasyonunu Excel/Sheets'te yap
   (bir gün sürer). Fiyatlar ayakta kalmıyorsa kod yazmak anlamsız.
3. **Motor seçimi ikinci mesele.** Asıl soru: kod kim yazacak? Üç yol var —
   (a) öğrenmek (6-12 ay), (b) freelancer ($2.5–5K, konuşmadaki kendi senaryoları),
   (c) **AI destekli geliştirme** (Claude Code/Codex ile prototip) — 2026'da en ucuz yol
   ve arkadaş zaten Gemini'yle çalışıyor.
4. **Bu GDD'yi koru.** Faction bağımlılık zinciri gerçekten iyi bir fikir; ortak/yayıncı
   görüşmelerinde bu doküman + design-notes.md sunum olarak kullanılır.
5. Hukuki not: kristal satışı + zar mekaniği bir arada olacaksa profesyonel görüş alınmalı.

## Klasör İçeriği

- `README.md` — bu inceleme
- `design-notes.md` — konuşmadan damıtılmış tam tasarım dokümanı
- `code/AstraImbuedCraftingEngine_Fixed.cs` — arkadaşın gönderdiği imbued crafting
  motorunun düzeltilmiş hâli (3 hata: god-roll olasılık tersliği, base doğrulama eksikliği,
  ID çakışması + sabit trait'ler; her fix kod içinde [FIX-1/2/3] olarak işaretli)
- (Gemini'deki PDF'ler: GDD ×2 + UI/UX mimarisi — indirilip buraya eklenebilir)

## Notlar

- İlk tarama (12 Kasım): paylaşım linki Google giriş duvarı arkasındaydı; içerik
  kullanıcı tarafından tam metin olarak sağlandı ve bu inceleme ona dayanıyor.
- İkinci paylaşım linki (sohbet içinde): https://share.gemini.google/lfdGL0v99yPc
