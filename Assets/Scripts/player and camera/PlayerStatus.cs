using System.Collections;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private ScoreManager scoreManager;
    public int lifePoints = 3;
    public int maxLifePoint = 3;
    public bool invulnerability = false;
    public Rarity rarity = Rarity.Common;
    public System.Action OnDeath;

    private bool canUseSpecial = true;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        if(scoreManager == null)
        {
            Debug.LogError("ScoreManager não encontrado na cena!");
            return;
        }

        maxLifePoint = lifePoints;
        scoreManager.lifePoints = maxLifePoint;
        StartCoroutine(SpecialTimer());
    }

    public void TakeDamage(int amount)
    {
        if(!invulnerability)
        {
            lifePoints -= amount;
            scoreManager.lifePoints = lifePoints;
            StartCoroutine(IgnoreDamage(3));
        }

        if(lifePoints <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        lifePoints = Mathf.Min(lifePoints + amount, maxLifePoint);
    }

    private void Die()
    {
        Debug.Log("Morreu");
        OnDeath?.Invoke();
    }

    IEnumerator IgnoreDamage(int seconds)
    {
        invulnerability = true;
        yield return new WaitForSeconds(seconds);
        invulnerability = false;
    }

    void DestroyColumns()
    {
        // adicionar depois animação de colunas quebradas
    }

    void ExtraPoints(int value)
    {
        scoreManager.score += value;
    }

    IEnumerator SpecialTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(20f);
            SpecialHabilit();
        }
    }

    public void SpecialHabilit()
    {
        if(!canUseSpecial) return;

        canUseSpecial = false;
        StartCoroutine(ResetSpecialCooldown());

        switch(rarity)
        {
            case Rarity.Common:
                Heal(1);
                break;

            case Rarity.Rare:
                Heal(1);
                StartCoroutine(IgnoreDamage(5));
                break;

            case Rarity.Epic:
                Heal(2);
                DestroyColumns();
                ExtraPoints(10);
                break;

            case Rarity.Legendary:
                Heal(2);
                DestroyColumns();
                StartCoroutine(IgnoreDamage(8));
                ExtraPoints(30);
                break;
        }
    }

    IEnumerator ResetSpecialCooldown()
    {
        float cooldownTime = 20f;
        float currentCooldown = cooldownTime;

        while(currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
            scoreManager.cooldown = currentCooldown;
            yield return null;
        }

        scoreManager.cooldown = 0;
        canUseSpecial = true;
    }

}
