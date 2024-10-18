using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // base entity class holding an entity state machine
    [SerializeField] protected EntityState _startState;
    protected EntityStateMachine _stateMachine;

    // base entity variables
    public bool isGrounded = true;
    public bool isMoving = false;
}
