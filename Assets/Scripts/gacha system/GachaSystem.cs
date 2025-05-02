using UnityEngine;

public class GachaSystem : MonoBehaviour
{
    public CharDatabase database;

    public void Roll()
    {
        if (!PlayerData.SpendPoints(50)) return;
        
        Rarity rolledRarity = RollRarity();
        CharData selectedChar = GetRandCharByRarity(rolledRarity);

        if (selectedChar.unlocked)
        {
            PlayerData.AddCoins(50);
            Debug.Log("Tu ganhou essência aeee parabéns!");
        }
        else
        {
            selectedChar.fragmentsOwned++;
            if (selectedChar.fragmentsOwned >= selectedChar.fragmentsRequired)
            {
                selectedChar.unlocked = true;
                Debug.Log($"{selectedChar.displayNames[selectedChar.selectedSkinIndex]} desbloqueado!");
            }
            else
            {
                Debug.Log($"Ganhou 1 fragmento de {selectedChar.displayNames[selectedChar.selectedSkinIndex]}");
            }
        }
        GachaMenu menu = FindAnyObjectByType<GachaMenu>();
        if (menu != null)
        {
            menu.RefreshCharList();
        }
    }

    Rarity RollRarity()
    {
        float roll = Random.Range(0f, 1f);
        if (roll < 0.6f) return Rarity.Common;
        else if (roll < 0.85f) return Rarity.Rare;
        else if (roll < 0.95f) return Rarity.Epic;
        else return Rarity.Legendary;
    }

    CharData GetRandCharByRarity(Rarity rarity)
    {
        var filtered = database.characters.FindAll(c => c.rarity == rarity && !c.unlocked);
        if (filtered.Count == 0)
        {
            filtered = database.characters.FindAll(c => c.rarity == rarity);
        }
        return filtered[Random.Range(0, filtered.Count)];
    }
}
