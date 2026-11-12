// AstraImbuedCraftingEngine_Fixed.cs
// AstraImbuedAdvancedEngine'in düzeltilmiş hâli — 3 gerçek hata giderildi:
//   [FIX-1] God-roll olasılığı ters çalışıyordu: taban stat büyüdükçe "Impossible"
//           garantiye dönüyordu (taban 1.5 → %0, taban 3.2+ → %100). Artık sabit %1.5
//           bağımsız zar; her item'da aynı nadirlikte.
//   [FIX-2] baseBonus doğrulanmıyordu (>3.20 girilirse her ürün god-roll çıkıyordu).
//           0..cap aralığına kırpılıyor.
//   [FIX-3] itemId rastgeleydi: 300 üründe %39 çakışma ihtimali. Artık sayaç tabanlı
//           benzersiz ID. Ayrıca trait'ler sabitti (enabled ise her item aynıydı);
//           şimdi her trait kendi zarıyla minimal varyasyonla geliyor.
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ImbuedItem
{
    public string itemId;
    public string itemName;
    public string factionOwner;
    public float baseStatBonus;
    public float actualStatBonus;        // Zar sonrası nihai stat (Max %3.20 Cap)
    public bool isImpossibleGodRoll;     // Bağımsız %1.5 zarla belirlenir

    // Pasif Özellikler (Traits) — her biri kendi zarıyla
    public bool hasDurabilityProtection; // "Repair istemez"
    public float raidBonusChance;        // Raidlerde ekstra şans (0 - %2 arası)
    public float miningProductivityBonus;// Maden verimliliği (%3 - %6 arası)
}

public class AstraImbuedCraftingEngineFixed : MonoBehaviour
{
    [Header("--- OYUNCU & İMBUED ENVANTERİ ---")]
    public string playerName = "Cengo";
    public List<ImbuedItem> craftedImbuedItems = new List<ImbuedItem>();

    private const float AbsoluteMaxCap = 3.20f;
    private const float GodRollChancePercent = 1.5f; // [FIX-1] sabit nadirlik

    private static int idCounter = 0;    // [FIX-3] benzersiz ID sayacı

    public void CraftAdvancedImbuedItem(string itemName, string faction, float baseBonus, bool enableSpecialTraits)
    {
        // [FIX-2] Girdi doğrulama
        baseBonus = Mathf.Clamp(baseBonus, 0f, AbsoluteMaxCap);

        // 1. Stat zarı: taban ile 2 katı arasında varyans, cap'te kırpılır
        float randomRollFactor = Random.Range(1.0f, 2.0f);
        float calculatedBonus = Mathf.Min(baseBonus * randomRollFactor, AbsoluteMaxCap);
        calculatedBonus = Mathf.Round(calculatedBonus * 100f) / 100f;

        // [FIX-1] God-roll artık çarpana değil, bağımsız sabit şansa bağlı
        bool isGodRoll = Random.Range(0f, 100f) <= GodRollChancePercent;

        // 2. Trait'ler — her biri bağımsız zar (önceki: sabit değerlerdi)
        bool noRepairNeeded = enableSpecialTraits && Random.Range(0f, 100f) <= 25f;
        float raidBonus = enableSpecialTraits ? Mathf.Round(Random.Range(0.5f, 2.0f) * 100f) / 100f : 0f;
        float miningBonus = enableSpecialTraits ? Random.Range(3f, 6f) : 0f;

        // [FIX-3] Benzersiz ID
        idCounter++;
        string newItemId = $"IMB_{idCounter:D6}_{System.Guid.NewGuid().ToString("N").Substring(0, 6)}";

        ImbuedItem newItem = new ImbuedItem
        {
            itemId = newItemId,
            itemName = itemName,
            factionOwner = faction,
            baseStatBonus = baseBonus,
            actualStatBonus = calculatedBonus,
            isImpossibleGodRoll = isGodRoll,
            hasDurabilityProtection = noRepairNeeded,
            raidBonusChance = raidBonus,
            miningProductivityBonus = miningBonus
        };

        craftedImbuedItems.Add(newItem);

        string traitInfo = "";
        if (newItem.hasDurabilityProtection) traitInfo += " | [PASİF: Asla Tamir İstemez]";
        if (newItem.miningProductivityBonus > 0) traitInfo += $" | [MADEN: +%{newItem.miningProductivityBonus:F1} Prodüktivite]";
        if (newItem.raidBonusChance > 0) traitInfo += $" | [RAİD: +%{newItem.raidBonusChance:F2} Şans]";

        if (isGodRoll)
        {
            Debug.Log($"[DEVASA BAŞARI!] IMPOSSIBLE (God-Roll) Üretildi: {newItem.itemName} | Stat: %{newItem.actualStatBonus:F2}{traitInfo}");
        }
        else
        {
            Debug.Log($"[Üretim Tamamlandı] {newItem.itemName} ({faction}) | Stat: %{newItem.actualStatBonus:F2}{traitInfo}");
        }
    }
}
