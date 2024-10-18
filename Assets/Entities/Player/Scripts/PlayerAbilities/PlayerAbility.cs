using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerAbility : MonoBehaviour
{
    protected PlayerCharacter _player;
    private bool isActionEnabled = true;
    
    // base player ability awake
    protected virtual void Awake()
    {
        // add self to the player's ability list
        _player = GetComponent<PlayerCharacter>();
        _player.Controller.RegisterAbility(this);
    }

    // a function that call an ability's specific action if that action is currently enabled
    public virtual void PreformAction(InputAction.CallbackContext context)
    {
        if (isActionEnabled)
        {
            Action(context);
        }
    }

    // an abstract function for an ability's specific action
    protected abstract void Action(InputAction.CallbackContext context);

    // enables the ability
    public void EnableAbility()
    {
        isActionEnabled = true;
    }

    // disables the ability
    public void DisableAbility()
    {
        isActionEnabled = false;
    }
}
