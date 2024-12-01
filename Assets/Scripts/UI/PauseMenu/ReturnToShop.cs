using Managers.EventManager;
using UnityEngine;

public class TriggerEventOnButton : MonoBehaviour
{
    public GameEvent Event;

    public void OnButtonTrigger()
    {
        Event.TriggerEvent();
    }
}