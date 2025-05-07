using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreCount;
    public int score;
    public Text lifeCount;
    public int lifePoints;
    public Text cooldownCount;
    public float cooldown;

    private int lastScore;
    private int lastLife;
    private int lastCooldown;

    void Update()
    {
        cooldown = Mathf.Max(0, cooldown - Time.deltaTime);

        if (score != lastScore && scoreCount != null)
        {
            scoreCount.text = score.ToString();
            lastScore = score;
        }

        if (lifePoints != lastLife && lifeCount != null)
        {
            lifeCount.text = lifePoints.ToString();
            lastLife = lifePoints;
        }

        int currentCooldown = Mathf.CeilToInt(cooldown);
        if (currentCooldown != lastCooldown && cooldownCount != null)
        {
            cooldownCount.text = currentCooldown.ToString();
            lastCooldown = currentCooldown;
        }
    }
}
