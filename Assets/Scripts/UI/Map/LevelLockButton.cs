using UnityEngine;

public class LevelLockButton : MonoBehaviour
{
    public GameObject lockImage; // Reference to the lock image

    public void SetLocked(bool isLocked)
    {
        // Enable or disable the lock image based on the lock state
        lockImage.SetActive(isLocked);
    }
}