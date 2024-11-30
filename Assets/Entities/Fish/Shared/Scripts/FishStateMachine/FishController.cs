 using UnityEngine;
using UnityEngine.AI;
using Managers.Time;

public class FishController : Entity
{
    private NavMeshAgent _navAgent;

    [SerializeField]
    private FishData _data;

    public bool IsInterested = false;
    public TimeManager Timer { get { return _timer; } private set { _timer = value; } }
    [SerializeField]
    private TimeManager _timer;

    private void OnEnable()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        InitializeFishStateMachine();
    }

    private void InitializeFishStateMachine()
    {
        _stateMachine = new EntityStateMachine(_startState, this);
    }

    public FishData GetFishData()
    {
        return _data;
    }

    public void SetTargetLocation(Vector3 target)
    {
        _navAgent.SetDestination(target);
    }

    public void SetSpeed(float speed)
    {
        _navAgent.speed = speed;
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
