using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public CharData currentChar;
    public Sprite selectedSkin;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void SetCurrentChar(CharData charData, Sprite skin)
    {
        currentChar = charData;
        selectedSkin = skin;
    }
}

