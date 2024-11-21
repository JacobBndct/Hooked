using UnityEngine;
using UnityEngine.UI;

public class MapSelectionManager : MonoBehaviour
{
    public GameObject mapPanel;
    public Button deepOceanButton;
    public Button nightTimeButton;
    public Button caveButton;
    public Button lakeButton;

    public GameObject deepOceanLock; // Lock icon for Deep Ocean level
    public GameObject nightTimeLock; // Lock icon for Night Time level
    public GameObject caveLock;      // Lock icon for Cave level

    public string deepOceanLevelName = "Ocean";
    public string nightTimeLevelName = "Dark";
    public string caveLevelName = "Cave";
    public string lakeLevelName = "Lake";

    private string selectedLevel;

    private bool hasEngineUpgrade = false;
    private bool hasLightsUpgrade = false;
    private bool hasHullUpgrade = false;

    public MapUIController mapUIController;

    private void Start()
    {
        //Initialize upgrade states from PlayerData
        if (PlayerManager.Instance != null && PlayerManager.Instance.playerData != null)
        {
            hasEngineUpgrade = PlayerManager.Instance.playerData.engineUpgrade;
            hasLightsUpgrade = PlayerManager.Instance.playerData.lightsUpgrade;
            hasHullUpgrade = PlayerManager.Instance.playerData.hullUpgrade;
        }
        else
        {
            Debug.LogWarning("PlayerManager or PlayerData is not initialized.");
        }

        deepOceanButton.interactable = false;
        nightTimeButton.interactable = false;
        caveButton.interactable = false;

        deepOceanButton.onClick.AddListener(() => SelectLevel(deepOceanLevelName));
        nightTimeButton.onClick.AddListener(() => SelectLevel(nightTimeLevelName));
        caveButton.onClick.AddListener(() => SelectLevel(caveLevelName));
        lakeButton.onClick.AddListener(() => SelectLevel(lakeLevelName));

        //Update map
        UpdateLevelAvailability();
    }
    //UI management
    public void OpenMap()
    {
        mapPanel.SetActive(true);
    }

    public void CloseMap()
    {
        mapPanel.SetActive(false);
    }
    //UI level management
    public void UpdateLevelAvailability()
    {
        deepOceanButton.interactable = hasEngineUpgrade;
        deepOceanLock.SetActive(!hasEngineUpgrade);

        nightTimeButton.interactable = hasLightsUpgrade;
        nightTimeLock.SetActive(!hasLightsUpgrade);

        caveButton.interactable = hasHullUpgrade;
        caveLock.SetActive(!hasHullUpgrade);
    }
    //level selection
    private void SelectLevel(string levelName)
    {
        selectedLevel = levelName;
        Debug.Log($"Selected Level: {levelName}");

        if (mapUIController != null)
        {
            mapUIController.CloseMap();
        }
    }
    //getter
    public string GetSelectedLevel()
    {
        return selectedLevel;
    }

    //upgrade unlock calls
    public void UnlockEngineUpgrade()
    {
        hasEngineUpgrade = true;
        UpdateLevelAvailability();
    }

    public void UnlockLightsUpgrade()
    {
        hasLightsUpgrade = true;
        UpdateLevelAvailability();
    }

    public void UnlockHullUpgrade()
    {
        hasHullUpgrade = true;
        UpdateLevelAvailability();
    }
}
