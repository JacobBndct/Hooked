using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.EventManager
{
	[CreateAssetMenu(menuName = "Game Event")]
	public class GameEvent : ScriptableObject
	{
		// maintain a list of event listeners that listen to this event
		readonly List<GameEventListener> m_Listeners = new List<GameEventListener>();

		// trigger an event and call the OnEventTriggered for each listener 
		public void TriggerEvent()
		{
			for (int i = m_Listeners.Count - 1; i >= 0; i--)
			{
				m_Listeners[i].OnEventTriggered();
			}
		}

		// add listener to the event
		public void AddListener(GameEventListener listener)
		{
			m_Listeners.Add(listener);
		}

		// remove listener fron the listener list
		public void RemoveListener(GameEventListener listener)
		{
			m_Listeners.Remove(listener);
		}
	}
}
