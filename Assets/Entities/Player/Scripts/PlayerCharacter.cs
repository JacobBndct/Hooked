using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Entity
{
    // base player character entity references
    public PlayerController Controller = new PlayerController();
    public Rigidbody rb;

    // boolean conditional player character entity variables
    public bool isGrappling = false;
    public bool isJumping = false;

    public void Awake()
    {
        // set up player character
        InitializePlayerstateMachine();
        rb = GetComponent<Rigidbody>();
    }

    // create an instance of the entity state machine
    private void InitializePlayerstateMachine()
    {
        _stateMachine = new EntityStateMachine(_startState, this);
    }

    // call the update function of the statemachine
    public void Update()
    {
        _stateMachine?.Update();
    }

    // call the fixed update function of the statemachine
    public void FixedUpdate()
    {
        _stateMachine?.FixedUpdate();
    }

    // call the late update function of the statemachine
    public void LateUpdate()
    {
        _stateMachine?.LateUpdate();
    }
}
