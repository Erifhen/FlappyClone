using UnityEngine;
using System;

public class PlayerData : MonoBehaviour
{
    public static int totalPoints = 500;
    public static int totalCoins = 100;

    public static event Action OnPointsChanged;
    public static event Action OnCoinsChanged;

    public static void AddPoints(int amount)
    {
        totalPoints += amount;
        OnPointsChanged?.Invoke();
    }

    public static bool SpendPoints(int amount)
    {
        if(totalPoints>= amount)
        {
            totalPoints -= amount;
            OnPointsChanged?.Invoke();
            return true;
        }
        return false;
    }

        public static void AddCoins(int amount)
    {
        totalCoins += amount;
        OnCoinsChanged?.Invoke();
    }

    public static bool SpendCoins(int amount)
    {
        if(totalCoins>= amount)
        {
            totalCoins -= amount;
            OnCoinsChanged?.Invoke();
            return true;
        }
        return false;
    }
}
