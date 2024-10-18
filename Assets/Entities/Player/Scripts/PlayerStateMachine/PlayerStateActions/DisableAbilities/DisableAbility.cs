using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Player/Actions/DisableAbility")]
public class DisableAbility : EntityStateAction
{
    public string AbilityName;
    public override void PerformAction(Entity entity)
    {
        // a player action that disables a given ability based on a given ability name string
        PlayerAbility playerAbility = ((PlayerCharacter)entity).Controller.FindPlayerAbility(AbilityName);
        if (playerAbility != null)
        {
            playerAbility.DisableAbility();
        }
    }
}
