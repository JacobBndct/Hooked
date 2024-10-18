using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Managers.EventManager
{
	public class GameEventListener : MonoBehaviour
	{
		// game event listener which keeps track of a game event and unity events to be called when game event is triggered
		public GameEvent GameEvent;
		public UnityEvent EventAction;

		// on enable add the listener to the game event's list of listeners
		void OnEnable()
		{
			GameEvent.AddListener(this);
		}

        // on disable remove the listener to the game event's list of listeners
        void OnDisable()
		{
			GameEvent.RemoveListener(this);
		}

		// call unity events when the event occurs
		public void OnEventTriggered()
		{
			EventAction.Invoke();
		}
	}
}
