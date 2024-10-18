using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Managers.CustomSceneManager
{
	public abstract class AbstractSceneTransitionScriptableObject : ScriptableObject
	{
		// animation parameters for the transition
		public float AnimationTime = 0.25f;
		protected Image AnimatedObject;

        // abstract async scene transition functions using coroutines for entering and exiting scene transitions
        public abstract IEnumerator Enter(Canvas parent);
		public abstract IEnumerator Exit(Canvas parent);

		// create an image on a given canvas to handle transitions
		protected virtual Image CreateImage(Canvas parent)
		{
			GameObject child = new GameObject("Transition Image");
			child.transform.SetParent(parent.transform, false);

			return child.AddComponent<Image>();
		}
	}
}
