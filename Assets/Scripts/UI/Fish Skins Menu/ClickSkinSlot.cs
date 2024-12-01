/**
 * Custom clicking class which works with scrolling events. From
 * https://discussions.unity.com/t/solved-scroll-not-working-when-elements-inside-have-click-events/130859/2.
 * 
 * @author Marina (Mars) Semenova
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickSkinSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        FishSkinsMenuController.SetSkin(gameObject); // set fish skin to clicked one if unlocked
    }

    public void OnPointerUp(PointerEventData eventData) {}
}
