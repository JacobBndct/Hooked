using UnityEngine;
using UnityEngine.UI;

public class MapSelectionManager : MonoBehaviour
{
    public GameObject mapPanel;
    public Button deepOceanButton;
    public Button nightTimeButton;
    public Button caveButton;
    public Button defaultLevelButton;

    public GameObject deepOceanLock; // Lock icon for Deep Ocean level
    public GameObject nightTimeLock; // Lock icon for Night Time level
    public GameObject caveLock;      // Lock icon for Cave level

    public string deepOceanLevelName = "Ocean";
    public string nightTimeLevelName = "Dark";
    public string caveLevelName = "Cave";
    public string lake = "BoatPrototype"; // Default level name

    private string selectedLevel;

    private bool hasEngineUpgrade = false;
    private bool hasLightsUpgrade = false;
    private bool hasHullUpgrade = false;

    public MapUIController mapUIController;

    private void Start()
    {
        // Set all level buttons to locked state initially
        deepOceanButton.interactable = false;
        nightTimeButton.interactable = false;
        caveButton.interactable = false;

        selectedLevel = lake;

        // Add listeners for each level button
        deepOceanButton.onClick.AddListener(() => SelectLevel(deepOceanLevelName));
        nightTimeButton.onClick.AddListener(() => SelectLevel(nightTimeLevelName));
        caveButton.onClick.AddListener(() => SelectLevel(caveLevelName));
        defaultLevelButton.onClick.AddListener(() => SelectLevel(lake));

        UpdateLevelAvailability();
    }

    public void OpenMap()
    {
        mapPanel.SetActive(true);
    }

    public void CloseMap()
    {
        mapPanel.SetActive(false);
    }

    public void UpdateLevelAvailability()
    {
        // Update Deep Ocean level lock and interactability
        deepOceanButton.interactable = hasEngineUpgrade;
        deepOceanLock.SetActive(!hasEngineUpgrade); // Show lock if not unlocked

        // Update Night Time level lock and interactability
        nightTimeButton.interactable = hasLightsUpgrade;
        nightTimeLock.SetActive(!hasLightsUpgrade); // Show lock if not unlocked

        // Update Cave level lock and interactability
        caveButton.interactable = hasHullUpgrade;
        caveLock.SetActive(!hasHullUpgrade); // Show lock if not unlocked
    }

    private void SelectLevel(string levelName)
    {
        selectedLevel = levelName;
        Debug.Log($"Selected Level: {levelName}");

        if (mapUIController != null)
        {
            mapUIController.CloseMap();
        }
    }

    public string GetSelectedLevel()
    {
        return selectedLevel;
    }

    // Methods to unlock levels (called from shop upgrades)
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
