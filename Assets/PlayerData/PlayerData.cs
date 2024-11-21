using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int money = 100; // Starting money
    public int casinoTokens = 0; // Casino token currency
    public int worms = 0; // Worms in inventory

    // Boat Upgrades
    public bool engineUpgrade = false;
    public bool lightsUpgrade = false;
    public bool hullUpgrade = false;

    //Fish
    public List<Fish> fishInventory = new List<Fish>();

    // Save progress (upgrades, inventory, etc.)
    public void SavePlayerData()
    {
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("CasinoTokens", casinoTokens);
        PlayerPrefs.SetInt("Worms", worms);

        PlayerPrefs.SetInt("EngineUpgrade", engineUpgrade ? 1 : 0);
        PlayerPrefs.SetInt("LightsUpgrade", lightsUpgrade ? 1 : 0);
        PlayerPrefs.SetInt("HullUpgrade", hullUpgrade ? 1 : 0);
        
        PlayerPrefs.SetString("FishInventory", JsonUtility.ToJson(fishInventory));
    }

    // Load progress (upgrades, inventory, etc.)
    public void LoadPlayerData()
    {
        money = PlayerPrefs.GetInt("Money", 100); // Default 100 if no data saved
        casinoTokens = PlayerPrefs.GetInt("CasinoTokens", 0);
        worms = PlayerPrefs.GetInt("Worms", 0);
        
        engineUpgrade = PlayerPrefs.GetInt("EngineUpgrade", 0) == 1;
        lightsUpgrade = PlayerPrefs.GetInt("LightsUpgrade", 0) == 1;
        hullUpgrade = PlayerPrefs.GetInt("HullUpgrade", 0) == 1;
        
        string fishInventoryJson = PlayerPrefs.GetString("FishInventory", "[]");
        fishInventory = JsonUtility.FromJson<List<Fish>>(fishInventoryJson);
    }
}