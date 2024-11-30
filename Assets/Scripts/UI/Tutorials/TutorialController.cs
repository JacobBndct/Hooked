using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject tutorialContainer;
    private bool tutorialShown = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            ToggleTutorial();
        }
    }

    public void ToggleTutorial()
    {
        if (tutorialShown)
        {
	        tutorialContainer.SetActive(false);
            tutorialShown = false;
        }
        else
        {
	        tutorialContainer.SetActive(true);
            tutorialShown = true;
        }
    }
}
