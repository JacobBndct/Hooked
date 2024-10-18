using System.Collections;
using UnityEngine;

namespace Managers.CustomSceneManager
{
	[CreateAssetMenu(fileName = "Fade", menuName = "Scene Transitions/Fade")]
	public class FadeTransitionScriptableObject : AbstractSceneTransitionScriptableObject
	{
		// implementation of abstract enter function for transition
		public override IEnumerator Enter(Canvas parent)
		{
			// initialize time, start fade colour, and end fade colour
			float time = 0;
			Color startColour = Color.black;
			Color endColour = new Color(0, 0, 0, 0);

			// while transition not complete
			while (time < 1) 
			{ 
				// set the current colour by lerping between the start and end colours using the completion time
				AnimatedObject.color = Color.Lerp(startColour, endColour, time);
				yield return null;

				// add delta time over total animation time to the completion time
				time += Time.deltaTime / AnimationTime;
			}

			// destroy the image object once done
			Destroy(AnimatedObject.gameObject);
		}

        // implementation of abstract exit function for transition
        public override IEnumerator Exit(Canvas parent)
		{
			// create a new transition image to the transition canvas and set the size + anchor
			AnimatedObject = CreateImage(parent);
			AnimatedObject.rectTransform.anchorMin = Vector2.zero;
			AnimatedObject.rectTransform.anchorMax = Vector2.one;
			AnimatedObject.rectTransform.sizeDelta = Vector2.zero;

			// initialize time, start fade colour, and end fade colour
			float time = 0;
			Color startColour = new Color(0, 0, 0, 0);
			Color endColour = Color.black;

			// while transition not completes
			while (time < 1)
			{
				// set the current colour by lerping between the start and end colours using the completion time
				AnimatedObject.color = Color.Lerp(startColour, endColour, time);
				yield return null;

				// add delta time over total animation time to the completion time
				time += Time.deltaTime / AnimationTime;
			}
		}
	}
}
