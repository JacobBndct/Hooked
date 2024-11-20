using UnityEngine;

public class ShopUpgrade : MonoBehaviour
{
    public MapSelectionManager mapSelectionManager;

    public void PurchaseEngineUpgrade()
    {
        mapSelectionManager.UnlockEngineUpgrade();
        Debug.Log("Engine Upgrade Purchased!");
    }

    public void PurchaseLightsUpgrade()
    {
        mapSelectionManager.UnlockLightsUpgrade();
        Debug.Log("Lights Upgrade Purchased!");
    }

    public void PurchaseHullUpgrade()
    {
        mapSelectionManager.UnlockHullUpgrade();
        Debug.Log("Hull Upgrade Purchased!");
    }
    public void PurchaseWormsTier1()
    {
        mapSelectionManager.UnlockHullUpgrade();
        Debug.Log("You've got 5 worms!");
    }
    
}