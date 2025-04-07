using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public float minDistanceJump = 3f, maxDistanceJump = 5f;
    public float minHeight = -4.5f, maxHeight = 4.5f;
    public float spawnDelay = 1.5f;
    
    private float spawnX, endX;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnPlatforms());
    }

    private void Update()
    {
        // Define os limites de spawn e destruição com base na posição da câmera
        spawnX = mainCamera.ViewportToWorldPoint(new Vector3(1.2f, 0, 0)).x;
        endX = mainCamera.ViewportToWorldPoint(new Vector3(-0.2f, 0, 0)).x;
    }

    private IEnumerator SpawnPlatforms()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            ControlPlatform();
        }
    }

    private void ControlPlatform()
    {
        int platformCount = Random.Range(1, 4);
        float distanceOffset = Random.Range(minDistanceJump, maxDistanceJump);
        float lastHeight = 0f; // Armazena a última altura para evitar sobreposição

        for (int i = 0; i < platformCount; i++)
        {
            float randomHeight;
            do
            {
                randomHeight = Random.Range(minHeight, maxHeight);
            } while (Mathf.Abs(randomHeight - lastHeight) < 1.5f); // Garante espaçamento mínimo entre plataformas

            lastHeight = randomHeight;
            Vector3 spawnPos = new Vector3(spawnX + distanceOffset * i, randomHeight, 0);
            GameObject platform = Instantiate(platformPrefab, spawnPos, Quaternion.identity);
            
            // Adiciona componente Rigidbody2D para garantir que as plataformas sejam tangíveis
            Rigidbody2D rb = platform.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = platform.AddComponent<Rigidbody2D>();
                rb.isKinematic = true; // Para evitar que a gravidade afete as plataformas
            }

            StartCoroutine(DestroyPlatform(platform));
        }
    }

    private IEnumerator DestroyPlatform(GameObject platform)
    {
        while (platform != null)
        {
            if (platform.transform.position.x < endX)
            {
                Destroy(platform);
                yield break;
            }
            yield return null;
        }
    }
}
