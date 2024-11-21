using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public MapSelectionManager mapSelectionManager;

    // Costs for items and upgrades
    public int engineUpgradeCost = 100;
    public int lightsUpgradeCost = 75;
    public int hullUpgradeCost = 50;
    public int wormCost = 10;

    public void PurchaseEngineUpgrade()
    {
        if (!PlayerManager.Instance.playerData.engineUpgrade &&
            PlayerManager.Instance.playerData.money >= engineUpgradeCost)
        {
            PlayerManager.Instance.playerData.money -= engineUpgradeCost;
            PlayerManager.Instance.playerData.engineUpgrade = true;

            // Unlock the map area for ocean fishing
            mapSelectionManager.UnlockEngineUpgrade();

            Debug.Log("Engine Upgrade Purchased! Ocean fishing unlocked.");
        }
        else
        {
            Debug.LogWarning("Not enough money or Engine Upgrade already purchased!");
        }
    }

    public void PurchaseLightsUpgrade()
    {
        if (!PlayerManager.Instance.playerData.lightsUpgrade &&
            PlayerManager.Instance.playerData.money >= lightsUpgradeCost)
        {
            PlayerManager.Instance.playerData.money -= lightsUpgradeCost;
            PlayerManager.Instance.playerData.lightsUpgrade = true;

            // Unlock the Night Time level
            if (mapSelectionManager != null)
            {
                mapSelectionManager.UnlockLightsUpgrade();
            }
            else
            {
                Debug.LogWarning("MapSelectionManager is not assigned in Shop.");
            }

            Debug.Log("Lights Upgrade Purchased! Nighttime fishing unlocked.");
        }
        else
        {
            Debug.LogWarning("Not enough money or Lights Upgrade already purchased!");
        }
    }


    public void PurchaseHullUpgrade()
    {
        if (!PlayerManager.Instance.playerData.hullUpgrade &&
            PlayerManager.Instance.playerData.money >= hullUpgradeCost)
        {
            PlayerManager.Instance.playerData.money -= hullUpgradeCost;
            PlayerManager.Instance.playerData.hullUpgrade = true;

            // Unlock the map area for cave fishing
            mapSelectionManager.UnlockHullUpgrade();

            Debug.Log("Hull Upgrade Purchased! Cave fishing unlocked.");
        }
        else
        {
            Debug.LogWarning("Not enough money or Hull Upgrade already purchased!");
        }
    }

    public void BuyWorms()
    {
        if (PlayerManager.Instance.playerData.money >= wormCost)
        {
            PlayerManager.Instance.playerData.money -= wormCost;
            PlayerManager.Instance.playerData.worms += 5;
            Debug.Log("You've purchased 5 worms!");
        }
        else
        {
            Debug.LogWarning("Not enough money to buy worms!");
        }
    }


    public void SellFish()
    {
        var fishInventory = PlayerManager.Instance.playerData.fishInventory;

        if (fishInventory.Count > 0)
        {
            int totalValue = fishInventory.Sum(fish => fish.value);
            PlayerManager.Instance.playerData.money += totalValue;
            fishInventory.Clear();
            Debug.Log($"Sold all fish for ${totalValue}!");
        }
        else
        {
            Debug.LogWarning("No fish to sell!");
        }
    }
}
