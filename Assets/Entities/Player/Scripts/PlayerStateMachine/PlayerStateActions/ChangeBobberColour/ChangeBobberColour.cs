using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Player/Actions/ChangeBobberColour")]
public class ChangeBobberColour : EntityStateAction
{
    public Color colour = Color.white;

    public override void PerformAction(Entity entityReference)
    {
        PlayerCharacter.Instance.BobberColour = colour;
    }
}
