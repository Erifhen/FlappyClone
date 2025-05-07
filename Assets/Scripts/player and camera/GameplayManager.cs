using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject playerPrefab;

    void Start()
    {
        var session = GameSession.Instance;
        if(session == null || session.selectedChar == null)
        {
            Debug.LogError("Deu erro no personagem");
            return;
        }

        CharData data = session.selectedChar;

        GameObject player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        var spriteSkin = player.GetComponentInChildren<SpriteRenderer>();
        if(spriteSkin != null && data.skins.Length > data.selectedSkinIndex)
        {
            spriteSkin.sprite = data.skins[data.selectedSkinIndex];
        }

        var status = player.GetComponent<PlayerStatus>();
        if( status != null)
        {
            status.lifePoints = data.lifePoints;
            status.rarity = data.rarity;
        }

        var rb = player.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            switch(data.rarity)
            {
                case Rarity.Common:
                rb.gravityScale = 1.3f;
                break;
                
                case Rarity.Rare:
                rb.gravityScale = 1.1f;
                break;

                case Rarity.Epic:
                rb.gravityScale = 0.9f;
                break;

                case Rarity.Legendary:
                rb.gravityScale = 0.7f;
                break;

            }
        }
    }
}
