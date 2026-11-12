# Project Astra: Nexus — Tasarım Notları (Gemini Konuşmasından Damıtılmış)

Kaynak: https://share.gemini.google/lfdGL0v99yPc ("Birlikte Mobil Oyun Geliştirme", Otomatik hesabı, 11-12 Kasım 2026)
Gemini'nin ürettiği PDF'ler (konuşma içindeki ekler): `project_astra_nexus_gdd.pdf`, `Project_Astra_Nexus_GDD.pdf`, `Project_Astra_Nexus_UIUX_Architecture.pdf`

## Konsept

StarCraft estetiğinde ama Clash of Clans oynanışında mobil sci-fi strateji. Üs kurma →
zaman tabanlı inşaat → işçi/nüfus döngüsü → Heroes of Might and Magic tarzı harita keşfi →
küresel pazar. Son hedef: devasa uzay gemisi/filosu üretimi ve faction'lar arası savaşlar.

## Faction'lar ve Üretim Zinciri (asıl değer burada)

| Faction | Cash Crop | Bağımlılık |
|---|---|---|
| **Supplier** | Kromit madeni (avantajlı çıkarır) | Warrior'ların koruduğu ticaret yolları |
| **Warrior** | Biyomekanik çekirdek / zırh alaşımı | Engineer fabrikaları + Supplier ikmali |
| **Scientist** | Kuantum blueprint'leri | Gaz + Kromit hammadde |
| **Engineer** | Üretim aletleri / crafting tool | Kromit + Gaz + Kristal |
| **Auctioneer** (5. yol) | Ticaret komisyonu | Her faction'dan özel item + Warrior'dan güvenlik |

**Nihai birleşim:** Engineer'ın aleti + Scientist'in blueprint'i → eşsiz (unique) item →
Supplier üretimini katlar / Warrior unique filo üretir. Kimse tek başına zirveye çıkamaz.

## 3 Ana Kaynak

- **Kristal** = para birimi (altın gibi, akışkan)
- **Kromit** = gelişim/alaşım madeni (5x değer)
- **Gaz** = özel işler / ileri teknoloji (20x değer)

## Harita / Sezon Döngüsü

- Kahramanlar günlük **6 tur** harcar (HoMM tarzı)
- **21 devasa harita**; her harita = 30 gün × 6 tur = **180 adım** = 1 ay
- 21 ay sonunda kimseye muhtaç olmadan zirveye varılır (ama özel item'lar hep daha fazlası)
- Her harita finalinde **epik item** düşer (üretime yardımcı, diğer faction'a yarayan, bazen 2./3. hero açan)
- Her 5. node'da **Tower Defense** dalgası
- Günlük hedef sonunda craft item düşer

## Idle Sistemler

- **3 saatlik mağara/asteroit kazısı** (idle): ödül = para + alet + özel tool
- Savaşçı eskortu yoksa **%20 pusu riski** (çözüm 3: koruma zorunlu)

## Ekonomi Kuralları

- Auction House **%5 vergi** (gold sink / para yakma)
- Nadir blueprint'ler sadece TD ve boss'lardan (scarcity = pazarın can damarı)
- Aylık sınırlı şans çekilişi: Engineer %2, Supplier %3 ile yabancı faction malı spawn'lar;
  Space Crystals ile ayda 1 kez denenebilir → **P2W kapalı**
- Kromit tükenmezse piyasa çöker → dayanıklılık/yıpranma gerekli (konuşmada açık kalan madde)

## Monetizasyon

- **Catch-up paketi**: Level 40'a kadar taşır, sadece zaman kazandırır
- **Raid'ler Level 60**; Level 60 sonrası paket yok, sadece Auction House
- Space Crystal paketleri, Battle Pass ($3-5/ay), kozmetik, pazar komisyonu
- Raid loot zarı: herkes atar, **%0.08 efsanevi şans** (yeni oyuncular troop/tech/supply katkısıyla)
- Pazar araştırması (sohbette): Whiteout Survival 400M+ indirme, $70-110M/ay; CoC 95-100M MAU

## Askıya Alınanlar

- 5 evren/ırk (mutant, insan, cyborg, klingonvari, elemental) → 3'e indirildi ama
  **onaylanmadı**: "aklımızda bulunsun buna dönecez" — ortak yıldızda farm/savaş,
  evrenler arası ticaret yok, sadece savaş ganimeti. Görsel tema farkı.
- Multi-account tartışması: 4 hesap açma cazibesi = bot riski; alternatif tek hesapta
  uzmanlık ağacı. Karar verilmedi.

## Ek Sistemler (Konuşmanın 2. Yarısı, 12 Kasım)

- **Her turun sonunda TD:** 6 turluk günlük yürüyüşün her adım sonunda mini Tower Defense.
- **Checkpoint cezası:** TD yenilgisi → son güvenli adıma (her 5 adımda güncellenir) geri
  dönüş + %10 kaynak tamir masrafı. Savaşçı hero savunmada = %95 TD başarısı, yoksa %75.
- **World Map / Sektör Savaşları:** Daily Map'ten ayrı sekme; 9 hero buraya gönderilir,
  istilacı avında %8 rare kart/ödül şansı.
- **Kazı:** günde **12 kazı** (24 saatte ~2 saatte bir; ESKİ 3 saatlik idle seferle çakışıyor).
  Pusu riski %20 (savaşçı hero eskortu = sıfır); ödül kromit 40-90 + gaz 10-25; %0.5 hero
  şansı, %3'e kadar rare item.
- **Hero:** maksimum **9** hero, TD/keşif/görevlerle XP ve seviye atlar; hero atamaları
  sinerji verir (warrior → pusu 0, scientist → blueprint şansı ↑).
- **TD yenilgisi, checkpoint, hero sinerjisi, Bid/Buyout pazar** konuşmada "4 kritik eksik"
  olarak kapatıldı.

## İtem Piramidi (350 item hedefi)

| Katman | Adet | Not |
|---|---|---|
| Common | ~100+ | erken aşama craft temeli |
| Rare | ~100 | orta kademe |
| Epic | 60 | 27 hero + 33 craft |
| Raid | 11 | 33 craft epic combine (3→1) |
| Imbued | 27 | epic + özel taş socket; 9 hero × 3 imbue slot |

**Önemli netleşmeler:**
- "27 imbued" hem **eşzamanlı giyilebilir üst sınır** (9 hero × 3 slot) hem de **faction başına
  ustalık hedefi** ("Engineer'ı master edersen Engineer'ın en iyi 27 item'ına ulaşırsın").
  Toplam evrensel havuz bu yüzden 27 değil, faction × 27 ölçeğinde düşünülmeli.
- Imbued item'lar **unique**: üretim anında zar atılır, stat minimal farklarla değişir.
  Hedef: taban ~%1.5-2, **cap %3.20** ("Impossible / God-Roll").
- İkincil pasifler (trait): "asla tamir istemez", raid'de +%1 şans, madende +%5 prodüktivite.
- Pazar: WoW tarzı **Bid + Buyout**, %5 vergi, tooltip'te üretici imzası ("Crafted by: Cengo"),
  god-roll spekülasyonu.

## Prototip Kodu (Gemini çıktıları)

`AstraPrototypeCore`, `AstraMarketManager`, `AstraDailyQuestManager`,
`AstraAdventureManager`, `AstraBaseLoopManager`, `AstraRaidAndProgressionManager`,
`AstraIdleMiningManager`, `AstraNexusUltimateMaster(Updated)` — C# / Unity MonoBehaviour
iskeletleri. **Çalışan oyun değil, konsol log'lu mantık taslakları** (bkz. README'deki kod notu).
