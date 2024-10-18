using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    // controller holds a dictionary of player abilities which is populated at runtime
    // in this scheme each ability is responsible for handling player inputs themselves by connecting to the new player input system
    private readonly Dictionary<string, PlayerAbility> PlayerAbilities = new Dictionary<string, PlayerAbility>();

    // regist a player ability to the player controller
    public void RegisterAbility(PlayerAbility ability)
    {
        PlayerAbilities.Add(ability.GetType().Name, ability);
    }

    // clear the registered abilities list
    public void ClearRegisteredAbilities()
    {
        PlayerAbilities.Clear();
    }

    // search ability from the player abilities list
    public PlayerAbility FindPlayerAbility(string abilityName)
    {
        PlayerAbilities.TryGetValue(abilityName, out PlayerAbility playerAbility);
        return playerAbility;
    }
}
