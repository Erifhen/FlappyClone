using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GachaMenu : MonoBehaviour
{
    public CharDatabase database;
    public GameObject charSlotPrefab;
    public Transform charListParent;

    private List<GameObject> currentSlots = new List<GameObject>();

    void Start()
    {
        bool foundSelected = false;
        foreach (CharData c in database.characters)
        {
            if (!foundSelected && c.unlocked)
            {
                c.selected = true;
                foundSelected = true;
            }
            else
            {
                c.selected = false;
            }
        }
        PopulateCharList();
    }

    void PopulateCharList()
    {
        foreach (GameObject slot in currentSlots)
        {
            Destroy(slot);
        }
        currentSlots.Clear();

        foreach (CharData character in database.characters)
        {
            GameObject slot = Instantiate(charSlotPrefab, charListParent);

            int skinIndex = character.selectedSkinIndex;
            Sprite currentSprite = character.skins[skinIndex];
            string currentName = character.displayNames[skinIndex];

            slot.transform.Find("Image").GetComponent<Image>().sprite = currentSprite;
            slot.transform.Find("txtPetName").GetComponent<Text>().text = currentName;
            slot.transform.Find("txtFragments").GetComponent<Text>().text = character.fragmentsOwned + " / " + character.fragmentsRequired;

            bool isUnlocked = character.unlocked;
            slot.transform.Find("Folder").gameObject.SetActive(!isUnlocked);

            slot.transform.Find("Selected").gameObject.SetActive(character.selected);

            if (isUnlocked)
            {
                GameObject imageObj = slot.transform.Find("Image").gameObject;
                AddClickListener(imageObj, () => OnCharClicked(character));
            }
            currentSlots.Add(slot);
        }
    }

    void AddClickListener(GameObject imageObj, System.Action action)
    {
        EventTrigger trigger = imageObj.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = imageObj.AddComponent<EventTrigger>();

        trigger.triggers.Clear();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((_) => action.Invoke());
        trigger.triggers.Add(entry);
    }

    void OnCharClicked(CharData selectedChar)
    {
    foreach (CharData c in database.characters)
        c.selected = false;

    selectedChar.selected = true;
    
    SelectedSkins skinPanel = FindObjectOfType<SelectedSkins>();
    if (skinPanel != null)
    {
        skinPanel.LoadCharacter(selectedChar);
    }
    PopulateCharList();
    }


    public void RefreshCharList()
    {
        PopulateCharList();
    }

    public CharData GetSelectedChar()
    {
        return database.characters.Find(c => c.selected);
    }
}


