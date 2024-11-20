using UnityEngine;

public class UpgradeSimulator : MonoBehaviour
{
    public MapSelectionManager mapSelectionManager;

    private void Update()
    {
        // Press 'E' to unlock the engine upgrade
        if (Input.GetKeyDown(KeyCode.E))
        {
            mapSelectionManager.UnlockEngineUpgrade();
            Debug.Log("Engine upgrade unlocked!");
        }
    }
}