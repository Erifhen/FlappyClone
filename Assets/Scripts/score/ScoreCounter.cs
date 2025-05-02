using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private ScoreManager scoreManager;

    void Start() {
        scoreManager = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreManager>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            scoreManager.score++;
        }
    }
}
