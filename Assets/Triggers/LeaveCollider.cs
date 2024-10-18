using UnityEngine;
using Managers.EventManager;

namespace Triggers
{
    public class LeaveCollider : MonoBehaviour
	{
        [SerializeField] string m_objectTag;
        [SerializeField] GameEvent m_GameEvent;

        // if an object with a given tag exits the trigger on this object, trigger a given event
        void OnTriggerExit(Collider other)
		{
			if (other.gameObject.tag == m_objectTag)
			{
				m_GameEvent.TriggerEvent();
			}
		}
	}
}
