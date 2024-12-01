using Managers.CustomSceneManager;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : Entity
{
    // base player character entity references
    public PlayerController Controller = new PlayerController();
    public Rigidbody rb;

    // boolean conditional player character entity variables
    public bool IsFishing = false;
    public bool IsPullingFish = false;
    public float FishingDistance = 0f;
    public float MaxFishingDistance = 0f;
    public Color BobberColour = Color.white;
    public Vector3 HookPosition = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

    // private instance
    private static PlayerCharacter s_Instance;

    // public static singleton instance of scene transitioner
    public static PlayerCharacter Instance
    {
        get => s_Instance;
        private set => s_Instance = value;
    }

    public void Awake()
    {
        // if there is another instance destroy self and throw warning
        if (Instance != null)
        {
            Debug.LogWarning($"Invalid configuration. Duplicate instances found! First instance: {Instance.name}");
            Destroy(gameObject);
            return;
        }

        // set the singleton instance
        s_Instance = this;

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
