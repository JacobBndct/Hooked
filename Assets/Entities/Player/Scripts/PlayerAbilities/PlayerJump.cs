using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : PlayerAbility
{
    [Header("Jump Settings")]
    [SerializeField] float jumpPower;
    [SerializeField] float jumpLength;
    [SerializeField] AnimationCurve jumpCurve;
    private float jumpTime = 0f;

    protected override void Awake()
    {
        // call base player ability's awake
        base.Awake();
    }

    private void Update()
    {
        // if the player is jumping the apply the jumping force
        if (_player.isJumping)
        {
            // apply the jump for at a specific given time
            Jump(jumpTime);

            // update the jump time
            jumpTime += Time.deltaTime /jumpLength;
        }
    }

    // input action called when the jump key is pressed
    protected override void Action(InputAction.CallbackContext context)
    {
        // if the jump key is pressed then setup and start the jump coroutine 
        if (context.started)
        {
            _player.isJumping = true;
            jumpTime = 0;
            StartCoroutine(TimeoutJump());
        }
        // when the jump key is released cancel the jump
        else if (context.canceled)
        {
            CancelJump();
            StopCoroutine(TimeoutJump());
        }
    }

    // add a jumping force to the player based on the given jumping sample time
    private void Jump(float jumpSample)
    {
        float jumpIncrement = jumpPower * jumpCurve.Evaluate(jumpSample) * Time.deltaTime;
        Vector3 newVelocity = new Vector3(0f,jumpIncrement, 0f);
        _player.rb.velocity += newVelocity;
    }

    // cancel the current jump
    private void CancelJump()
    {
        _player.isJumping = false;
    }

    // a coroutine to cancel the current jump if the jump exceeds the max jump length
    private IEnumerator TimeoutJump()
    {
        yield return new WaitForSeconds(jumpLength);
        CancelJump();
    }
}
