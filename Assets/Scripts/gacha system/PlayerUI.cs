using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text pointsText;
    public Text coinsText;
    public Text pointsTextTwo;
    public Text coinsTextTwo;

    void Start()
    {
        UpdatePointsUI();
        UpdateCoinsUI();

        PlayerData.OnPointsChanged += UpdatePointsUI;
        PlayerData.OnCoinsChanged += UpdateCoinsUI;
    }

    void OnDestroy()
    {
        PlayerData.OnPointsChanged -= UpdatePointsUI;
        PlayerData.OnCoinsChanged -= UpdateCoinsUI;
    }

    void UpdatePointsUI()
    {
        pointsText.text = PlayerData.totalPoints.ToString("00");
        pointsTextTwo.text = PlayerData.totalPoints.ToString("00");
    }

    void UpdateCoinsUI()
    {
        coinsText.text = PlayerData.totalCoins.ToString("00");
        coinsTextTwo.text = PlayerData.totalCoins.ToString("00");
    }
}
