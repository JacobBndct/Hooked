using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance; //Singleton instance
    public PlayerData playerData;        //Reference to player data

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //Persist across scenes
        }
        else
        {
            Destroy(gameObject); //Prevent duplicates
        }

        playerData = new PlayerData();
        playerData.LoadPlayerData(); //Load saved data on start
    }

    private void OnApplicationQuit()
    {
        playerData.SavePlayerData(); //Save data on application quit
    }
}