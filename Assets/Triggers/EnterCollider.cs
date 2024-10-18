using Managers.EventManager;
using UnityEngine;

namespace Triggers
{
    public class EnterCollider : MonoBehaviour
    {
        [SerializeField] string m_objectTag;
        [SerializeField] GameEvent m_GameEvent;

        // if an object with a given tag enters the trigger on this object, trigger a given event
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == m_objectTag)
            {
                m_GameEvent.TriggerEvent();
            }
        }
    }
}
