using UnityEngine;

public enum Rarity { Common, Rare, Epic, Legendary }

[System.Serializable]
public class CharData
{
    public string id;
    public int lifePoints;
    public Rarity rarity;
    public int fragmentsOwned;
    public int fragmentsRequired;
    public bool unlocked;
    public bool selected;

    public string[] displayNames;
    public Sprite[] skins;
    public int[] prices;
    public bool[] skinsUnlocked;

    public int selectedSkinIndex = 0;
}


