using UnityEngine;

public class ReturnCamera : MonoBehaviour
{
    public ShopTransition transitionManager;

    private void OnMouseDown()
    {
        transitionManager.ReturnToMainView(); // Centralized return to main view
    }
}
