/**
 * Custom Scroll Rect class with no scroll on drag.
 * From https://www.reddit.com/r/Unity3D/comments/qnk6di/disabling_mouse_scrolling_on_scroll_view/?rdt=35497.
 * 
 * @author Marina (Mars) Semenova
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoDragScrollRect : ScrollRect
{
    public override void OnBeginDrag(PointerEventData eventData) {}
    public override void OnDrag(PointerEventData eventData) {}
    public override void OnEndDrag(PointerEventData eventData) {}
}
