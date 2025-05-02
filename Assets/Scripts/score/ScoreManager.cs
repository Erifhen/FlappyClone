using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text count;
    public int score;

    void Update()
    {
        count.text = score.ToString();
    }
}
