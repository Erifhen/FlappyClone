using UnityEngine;
public class GameSession : MonoBehaviour
{
    public static GameSession Instance;
    public CharData selectedChar;
    public int selectedSkinIndex;
    public CharDatabase charDatabase;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if(charDatabase == null)
        {
            charDatabase = FindObjectOfType<CharDatabase>();
        }

        EnsureValidSelection();
    }
    public void EnsureValidSelection()
    {
        var characters = charDatabase.characters;

        if(!characters.Exists(c => c.selected))
        {
            var firstUnlocked = characters.Find(c => c.unlocked);
            if(firstUnlocked != null)
            {
                firstUnlocked.selected = true;
                firstUnlocked.selectedSkinIndex = 0;
            }
        }

        selectedChar = characters.Find(c => c.selected);
        selectedSkinIndex = selectedChar != null ? selectedChar.selectedSkinIndex : 0;
    }
}
