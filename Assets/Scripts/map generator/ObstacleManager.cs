using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obstacle;
    public float coolDown = 3f;

   void Update()
    {
        Cooldown(); 
    }

    void Cooldown()
    {
        if (coolDown <= 0f)
        {
            SpawnObstacle();
            coolDown = 3f;
        }
        else
        {
            coolDown -= Time.deltaTime;
        }
    }
void SpawnObstacle()
{
    GameObject clone = Instantiate(obstacle, new Vector3(5, Random.Range(-3, 3), 0), Quaternion.identity);
    Destroy(clone, 5f);
}


}
