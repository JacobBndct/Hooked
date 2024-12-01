/**
 * Class used in development.
 * 
 * @author Marina (Mars) Semenova
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarsSandboxScript : MonoBehaviour
{
    public GameObject slotMachine;
    public GameObject skinsMenu;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // spin slot machine test
        {
            SlotMachineController c = slotMachine.GetComponent<SlotMachineController>();
            c.Spin();
        }

        if (Input.GetKeyDown(KeyCode.F)) // open skin menu sim
        {
            skinsMenu.SetActive(!skinsMenu.activeSelf);
        }

		if (Input.GetKeyDown(KeyCode.R)) // test skin unlock
        {
            PlayerSkins.UnlockSkin("var1");
        }
    }
}
