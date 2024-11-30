using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private PlayerCharacter _player;

    // provide a mask to try for ground raycast
    [SerializeField] LayerMask mask;

    // provide a transform from where to start the raycast
    [SerializeField] Transform feetTransform;
    
    // the max distance that the raycast can travel to find
    [SerializeField] float maxDistance;

    // find the player component on this object
    private void Awake()
    {
        _player = GetComponent<PlayerCharacter>();
    }

    // check on update if the player is grounded
    private void Update()
    {
        _player.IsGrounded = IsOnGround();
    }

    // determine if the player is ground or not by raycast down from the player's feet 
    private bool IsOnGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(feetTransform.position, -feetTransform.up, out hit, maxDistance, mask))
        {
            return hit.distance < maxDistance;
        }

        return false;
    }
}
