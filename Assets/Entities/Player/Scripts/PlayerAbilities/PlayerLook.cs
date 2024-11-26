using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : PlayerAbility
{
    [Header("Camera Movement Parameters")]
    [SerializeField] float horizontalSensitivity, verticalSensitivity;
    [SerializeField] bool invertHorizontal, invertVertical;
    [SerializeField] int cameraRange;

    [Header("Camera Obeject Parameters")]
    [SerializeField] Transform rotatableObjects;

    protected override void Awake()
    {
        // call base player ability awake
        base.Awake();

        // hide and lock the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // action to be called when the mouse moves
    protected override void Action(InputAction.CallbackContext context)
    {
        Vector2 delta = context.ReadValue<Vector2>();

        HorizontalRotation(delta.x);
        VerticalRotation(delta.y);
    }

    // updates the x rotation of player
    void HorizontalRotation(float deltaX)
    {
        // calculate the horizontal rotation amount and account for a potential inversion
        float horizontalRotation = deltaX * horizontalSensitivity * Invert(invertHorizontal);

        // find the quaterion rotation
        Quaternion horizontalRotationQuaternion = Quaternion.AngleAxis(horizontalRotation, transform.up) * _player.rb.rotation;

        // apply quaterion rotation
        _player.rb.MoveRotation(horizontalRotationQuaternion);
    }

    // updates the y rotation of camera and other rotatable objects
    void VerticalRotation(float deltaY)
    {
        // calculate the vertical rotation and account for a potential inversion
        float verticalRotation = -(deltaY * verticalSensitivity) * Invert(invertVertical);

        // find the current vertical rotation
        float currentVerticalRotation = RelativeToForward(rotatableObjects.localEulerAngles.x);

        // find the min and max vertical rotations given the camera rotation range
        float min = -cameraRange - currentVerticalRotation;
        float max = cameraRange - currentVerticalRotation;

        // clamp the verticle rotation between the min and max
        verticalRotation = Mathf.Clamp(verticalRotation, min, max);

        // find the rotation vector 
        Vector3 verticalRotationVector = new Vector3(verticalRotation, 0f, 0f);

        // apply the rotation vector
        rotatableObjects.Rotate(verticalRotationVector);
    }

    // Changes the range of a euler angle from 0 - 360 degrees to -180 - 180. This works so that 0 is in the direction of the local forward of the transform.
    float RelativeToForward(float eulerRotation)
    {
        return (eulerRotation >= 180) ? eulerRotation - 360 : eulerRotation;
    }

    // Inverts equation on true and leaves the same on false
    float Invert(bool isInverted)
    {
        return Mathf.Pow(-1, isInverted ? 1 : 0);
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}