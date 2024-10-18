using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : PlayerAbility
{
    [Header("Player Movement Parameters")]
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;

    Vector2 playerDirection = Vector2.zero;
    Vector3 currentVelocity => _player.rb.velocity;

    protected override void Awake()
    {
        // call the base player ability class
        base.Awake();
    }

    // call the player movement fuction on fixed update
    void FixedUpdate()
    {
        MovePlayer();
    }

    // update the current movement direction based on player input
    protected override void Action(InputAction.CallbackContext context)
    {
        _player.isMoving = true;
        playerDirection = context.ReadValue<Vector2>();
    }


    // update the player's velocity according to the recorded input of the user
    public void MovePlayer()
    {
        Accelerate(transform.TransformDirection(new Vector3(playerDirection.x, 0.0f, playerDirection.y)), acceleration, maxSpeed);
    }

    // accelerate the player based on the current movement direction
    private void Accelerate(Vector3 direction, float acceleration, float maxSpeed)
    {
        // if no player direction is being given don't update the player's velocity
        if (direction == Vector3.zero)
        {
            _player.isMoving = false;
            return;
        }

        // finds the desired acceleration to apply to the player
        Vector3 desiredAcceleration = direction.normalized * acceleration * Time.deltaTime;
        
        // if the current velocity plus the desired acceleration is over the max speed return
        if ((currentVelocity + desiredAcceleration).magnitude > maxSpeed)
            return;

        // apply the acceleration
        Vector3 newVelocity = currentVelocity + desiredAcceleration;
        _player.rb.velocity = newVelocity;
    }
}
