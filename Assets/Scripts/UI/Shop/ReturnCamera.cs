using UnityEngine;

public class ReturnCamera : MonoBehaviour
{
    public AreaTransitionManager transitionManager;

    private void OnMouseDown()
    {
        transitionManager.ReturnToMainView(); // Centralized return to main view
    }
}
