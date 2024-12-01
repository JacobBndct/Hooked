using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickSkinSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        FishSkinsMenuController.SetSkin(gameObject);
    }

    public void OnPointerUp(PointerEventData eventData) {}
}
