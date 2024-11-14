using UnityEngine;

public class ReturnToMainButtonHandler : MonoBehaviour
{
    public ShopTransition shoptransition;  // Reference to the ShopTransition

    public void OnButtonClick()
    {
        shoptransition.ReturnToMainView(); // Call the method to return to the main camera view
    }
}