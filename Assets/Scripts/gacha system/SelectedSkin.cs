using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SelectedSkins : MonoBehaviour
{
    public CharDatabase charDatabase;
    public GameObject petSelectPrefab;
    public Transform skinDisplayParent;

    private CharData currentChar;
    private int currentSkinIndex = 0;

    private Text nameTxt;
    private Image skinImage;
    private Text priceTxt;
    private GameObject folderLock;

    private void Start()
{
    CharData selected = charDatabase.characters.Find(c => c.selected);
    if (selected == null)
    {
        selected = charDatabase.characters[0];
        selected.selected = true;
    }
    LoadCharacter(selected);
}

   public void LoadCharacter(CharData character)
    {
        foreach (Transform child in skinDisplayParent)
        {
        Destroy(child.gameObject);
        }

        currentChar = character;
        currentSkinIndex = Mathf.Clamp(currentChar.selectedSkinIndex, 0, currentChar.skins.Length - 1);

        GameObject currentDisplayGO = Instantiate(petSelectPrefab, skinDisplayParent);

        nameTxt = currentDisplayGO.transform.Find("Name").GetComponent<Text>();
        skinImage = currentDisplayGO.transform.Find("Image").GetComponent<Image>();
        priceTxt = currentDisplayGO.transform.Find("Price").GetComponent<Text>();
        folderLock = currentDisplayGO.transform.Find("Folder").gameObject;

        UpdateCharDisplay();
        AddBuyTrigger();
    }

    void AddBuyTrigger()
    {
        var trigger = folderLock.GetComponent<EventTrigger>();
        if (trigger == null) trigger = folderLock.AddComponent<EventTrigger>();

        trigger.triggers.Clear();
        var entry = new EventTrigger.Entry { eventID = EventTriggerType.PointerClick };
        entry.callback.AddListener((eventData) => TryBuy());
        trigger.triggers.Add(entry);
    }

    void UpdateCharDisplay()
    {
        if (currentSkinIndex >= currentChar.skins.Length)
            currentSkinIndex = 0;

            nameTxt.text = currentChar.displayNames[currentSkinIndex];
            bool unlocked = currentChar.skinsUnlocked[currentSkinIndex];

        if (unlocked)
        {
            skinImage.sprite = currentChar.skins[currentSkinIndex];
            folderLock.SetActive(false);
            priceTxt.gameObject.SetActive(false);
        }
        else
        {
            skinImage.sprite = currentChar.skins[currentSkinIndex];
            folderLock.SetActive(true);
            priceTxt.text = currentChar.prices[currentSkinIndex].ToString();
            priceTxt.gameObject.SetActive(true);
        }
    }

    public void NextSkin()
    {
        currentSkinIndex = (currentSkinIndex + 1) % currentChar.skins.Length;
        UpdateCharDisplay();
    }

    public void PrevSkin()
    {
        currentSkinIndex = (currentSkinIndex - 1 + currentChar.skins.Length) % currentChar.skins.Length;
        UpdateCharDisplay();
    }

    public void OnPlayButton()
    {
        Sprite chosenSkin = currentChar.skinsUnlocked[currentSkinIndex]
            ? currentChar.skins[currentSkinIndex]
            : currentChar.skins[0];

        GameManager.Instance.SetCurrentChar(currentChar, chosenSkin);
        SceneManager.LoadScene("GameScene");
    }

    void TryBuy()
    {
        if (currentChar.skinsUnlocked[currentSkinIndex]) return;

        int price = currentChar.prices[currentSkinIndex];
        bool canAfford = PlayerData.totalCoins >= price;

        if (canAfford)
        {
            //usar um popup real com confirmação
            Debug.Log($"Confirmar compra da skin por {price} coins?");
            BuySkin(price);
        }
        else
        {
            Debug.Log("Coins insuficientes para comprar essa skin.");
        }
    }

    void BuySkin(int price)
    {
        bool success = PlayerData.SpendCoins(price);

        if (success)
        {
            currentChar.skinsUnlocked[currentSkinIndex] = true;
            Debug.Log("Skin comprada com sucesso!");
            UpdateCharDisplay();
        }
        else
        {
            Debug.Log("Erro inesperado ao tentar comprar skin.");
        }
    }
}

