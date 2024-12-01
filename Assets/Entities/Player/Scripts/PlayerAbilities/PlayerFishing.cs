using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerFishing : PlayerAbility
{
    [Header("References")]
    [SerializeField] private Transform _fishingRodTip;
    [SerializeField] private LineRenderer grappleRope;
    
    [Header("Grappling")]
    [SerializeField] private float maxFishingDist;
    [SerializeField] private float chargeSpeed;
    [SerializeField] private float ropeSpeed = 8f;

    [SerializeField] private float overshootYAxis;

    private Vector3 _fishingTarget;

    [SerializeField] private Rigidbody _bobberPoint;

    private Vector3 velocityToSet;

    [SerializeField] private float fishingCD;
    private float fishingCDTimer;

    private bool isFishingHeld = false;
    private float _fishingDistance = 0.1f;

    [Header("Spring")]
    private Spring spring;
    [SerializeField] private int quality;
    [SerializeField] private float damper;
    [SerializeField] private float strength;
    [SerializeField] private float waveCount;
    [SerializeField] private float waveHeight;
    [SerializeField] private AnimationCurve affectCurve;

    protected override void Awake()
    {
        // call base player ability's awakes
        base.Awake();
    }

    // preforms the grapple action based on the player's input
    protected override void Action(InputAction.CallbackContext context)
    {
        // launch grappling rope when the player presses the input
        if (context.started)
        {
            if (_player.IsFishing)
            {
                // pull rod back and check if any fish was caught
                PullbackFishingRod();
            }
            else if (!isFishingHeld)
            {
                isFishingHeld = true;
            }
        }
        else if (context.canceled && isFishingHeld)
        {
            _player.IsFishing = true;
            isFishingHeld = false;
            CastFishingRod(0.1f);
        }
    }
    
    // set up the spring at the game start
    private void Start()
    {
        spring = new Spring();
        spring.SetTarget(0);
        spring.SetDamper(damper);
        spring.SetStrength(strength);
    }

    // increments the grappling launcher's cooldown down if it has been used
    private void Update()
    {
        if (fishingCDTimer > 0)
        {
            fishingCDTimer -= Time.deltaTime;
        }
        
        if (isFishingHeld && (_fishingDistance < maxFishingDist))
        {
            _fishingDistance += Time.deltaTime * chargeSpeed;
        }
    }

    // draw the rope
    private void LateUpdate()
    {
        UpdateBobberPosition();
        DrawRope();
        _bobberPoint.gameObject.GetComponent<Renderer>().material.color = PlayerCharacter.Instance.BobberColour;
    }

    // start the grappling process by racasting to see if there are any colliders that can be grappled in front of the player
    private void CastFishingRod(float waitTime)
    {
        // if the grapple launcher is on cooldown return
        if (fishingCDTimer > 0) return;

        // set the grappling status
        _player.IsFishing = true;
        _bobberPoint.useGravity = true;

        _bobberPoint.transform.parent = null;

        _fishingTarget = _player.transform.position + _player.rb.transform.forward * _fishingDistance;

        // set some more values for drawing the rope
        _bobberPoint.position = _fishingRodTip.position;
        grappleRope.enabled = true;

        // find the lowest point of the player
        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        // find the relative grappling point
        float grapplePointRelativeYPos = _fishingTarget.y - lowestPoint.y;

        // calculate the highest point the arc of the trajectory
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        // if the grapple point is lower than the player set the highest point to the overshoot value
        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        // start the jump coroutine
        StartCoroutine(CastToPosition(transform.position, _fishingTarget, highestPointOnArc, waitTime));
    }

    private void PullbackFishingRod()
    {
        StartCoroutine(PullFish(RopeWaitTime()));
    }

    // a coroutine that stops the grappling and puts it on cooldown
    private IEnumerator PullFish(float waitTime)
    {
        _player.IsPullingFish = true;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        // find the relative grappling point
        float grapplePointRelativeYPos = _fishingTarget.y - lowestPoint.y;

        // calculate the highest point the arc of the trajectory
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        // if the grapple point is lower than the player set the highest point to the overshoot value
        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        // start the jump coroutine
        StartCoroutine(CastToPosition(_fishingTarget, transform.position, highestPointOnArc / 2f, 0.1f));

        yield return new WaitForSeconds(waitTime);

        _player.IsFishing = false;
        _fishingDistance = 0.1f;

        _bobberPoint.useGravity = false;
        _bobberPoint.velocity = Vector3.zero;
        _bobberPoint.transform.parent = _fishingRodTip.parent.parent;

        fishingCDTimer = fishingCD;

        grappleRope.enabled = false;

        _player.IsPullingFish = false;
    }

    // applies the jumping velocity to the player
    private IEnumerator CastToPosition(Vector3 startPosition, Vector3 targetPosition, float trajectoryHeight, float waitTime)
    {
        // wait for a given amount of time
        yield return new WaitForSeconds(waitTime);

        // find the desired velocitys
        velocityToSet = CalculateCastVelocity(startPosition, targetPosition, trajectoryHeight);

        // set the velocity of the player
        _bobberPoint.velocity = velocityToSet;
    }


    // calculate the required velocity in order to reach a given trajectory height to reach the endpoint
    public Vector3 CalculateCastVelocity(Vector3 startingPoint, Vector3 endPoint, float trajectoryHeight)
    {
        // find the required y and zx displacements
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startingPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startingPoint.x, 0f, endPoint.z - startingPoint.z);

        // find the velocity required for the displacement
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity) + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        // add the velocities together
        return velocityY + velocityXZ;
    }
    
    // the wait time will be the speed over the distance
    private float RopeWaitTime()
    {
        return Vector3.Distance(_fishingRodTip.position, _fishingTarget) / ropeSpeed;
    }

    private void UpdateBobberPosition()
    {
        if (_player.IsFishing)
        {
            _player.HookPosition = _bobberPoint.transform.position;
        }
        else
        {
            _player.HookPosition = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        }
    }

    private void DrawRope()
    {
        // if not grappling reset the line render and spring component
        if (!_player.IsFishing)
        {
            _bobberPoint.position = _fishingRodTip.position;
            spring.Reset();
            grappleRope.positionCount = 0;
            return;
        }

        // reinitialize line render and spring component
        if (grappleRope.positionCount == 0)
        {
            spring.SetVelocity(ropeSpeed);
            grappleRope.positionCount = quality + 1;
        }

        // set the deltaTime of the spring componenet
        spring.Update(Time.deltaTime);

        Vector3 forceDirection = Quaternion.LookRotation((_fishingTarget - _fishingRodTip.position).normalized) * Vector3.up;
        //_bobberPoint.position = Vector3.Lerp(_bobberPoint.position, _fishingTarget, (Time.deltaTime * ropeSpeed) / _fishingDistance);
        
        for (int i = 0; i < quality + 1; i++)
        {
            // delta from 0.0 - 1.0
            float delta = i / (float) quality;
            Vector3 offset = forceDirection * waveHeight * Mathf.Sin(delta * waveCount * Mathf.PI) * spring.Value * affectCurve.Evaluate(delta);

            grappleRope.SetPosition(i, Vector3.Lerp(_fishingRodTip.position, _bobberPoint.position, delta) + offset);
        }
    }
}
