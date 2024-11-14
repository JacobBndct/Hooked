using UnityEngine;

public class ReturnToMainButtonHandler : MonoBehaviour
{
    public AreaTransitionManager transitionManager;  // Reference to the AreaTransitionManager

    public void OnButtonClick()
    {
        transitionManager.ReturnToMainView(); // Call the method to return to the main camera view
    }
}