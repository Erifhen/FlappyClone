using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
 private float baseSpeed = 2f;
    private float speed;
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreManager>();
    }

    void Update()
    {
        speed = baseSpeed + (scoreManager.score / 10f);
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
