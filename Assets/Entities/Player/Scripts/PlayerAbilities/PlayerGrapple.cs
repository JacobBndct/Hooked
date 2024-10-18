using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrapple : PlayerAbility
{
    [Header("References")]
    [SerializeField] private Transform cam;
    [SerializeField] private Transform launcherTip;
    [SerializeField] private LayerMask IsGrappleableLayer;
    [SerializeField] private LineRenderer grappleRope;
    
    [Header("Grappling")]
    [SerializeField] private float maxGrappleDist;
    [SerializeField] private float ropeSpeed = 8f;

    [SerializeField] private float overshootYAxis;
    [SerializeField] private float pullPower;
    [SerializeField] private float pullDelay;

    private Vector3 grapplePoint;
    private Vector3 currentPoint;
    private Vector3 initialGrapplePosition;
    private Vector3 initialGrappleNormal;
    private Vector3 velocityToSet;
    private float initialDistance;

    [SerializeField] private float grappleCD;
    private float grappleCDTimer;

    private bool isGrappleHit;

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
            _player.isGrappling = true;
            StartGrapple();
        }
        // when the input is release pull the player in
        else if (context.canceled && isGrappleHit)
        {
            ExecuteGrapple(pullDelay);
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
        if (grappleCDTimer > 0)
        {
            grappleCDTimer -= Time.deltaTime;
        }
    }

    // draw the rope
    private void LateUpdate()
    {
        DrawRope();
    }

    // start the grappling process by racasting to see if there are any colliders that can be grappled in front of the player
    private void StartGrapple()
    {
        // if the grapple launcher is on cooldown return
        if (grappleCDTimer > 0) return;

        // set the grappling status
        _player.isGrappling = true;
        isGrappleHit = true;

        // raycast forward from the camera and set grappling values if there is a hit
        RaycastHit hit;
        if(Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDist, IsGrappleableLayer))
        {
            grapplePoint = hit.point;
            initialDistance = Vector3.Distance(launcherTip.position, grapplePoint);
            initialGrapplePosition = launcherTip.position;
            initialGrappleNormal = _player.rb.transform.forward;
        }
        else
        {
            isGrappleHit = false;
            grapplePoint = cam.position + cam.forward * maxGrappleDist;
            initialDistance = Vector3.Distance(launcherTip.position, grapplePoint);

            // cancel the grappling if the raycast missed
            StartCoroutine(StopGrapple(RopeWaitTime()));
        }

        // set some more values for drawing the rope
        currentPoint = launcherTip.position;
        grappleRope.enabled = true;
    }

    // perform the pulling action of the grappling hook
    private void ExecuteGrapple(float waitTime)
    {
        // find the lowest point of the player
        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        // calculate the change in position between the original grappling point and the release points
        Vector3 pullGrappleDirection = -ProjectMovementOntoPlane(launcherTip.position) * pullPower;

        // find the relative grappling point
        float grapplePointRelativeYPos = grapplePoint.y + pullGrappleDirection.y - lowestPoint.y;

        // calculate the highest point the arc of the trajectory
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        // if the grapple point is lower than the player set the highest point to the overshoot value
        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        // start the jump coroutine
        StartCoroutine(JumpToPosition(grapplePoint + pullGrappleDirection, highestPointOnArc, waitTime));
        
        // start the stop grappling coroutine
        StartCoroutine(StopGrapple(waitTime));
    }

    // a coroutine that stops the grappling and puts it on cooldown
    private IEnumerator StopGrapple(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        _player.isGrappling = false;

        grappleCDTimer = grappleCD;

        grappleRope.enabled = false;
    }

    // applies the jumping velocity to the player
    private IEnumerator JumpToPosition(Vector3 targetPosition, float trajectoryHeight, float waitTime)
    {
        // wait for a given amount of time
        yield return new WaitForSeconds(waitTime);

        // find the desired velocitys
        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);

        // set the velocity of the player
        _player.rb.velocity = velocityToSet;
    }

    // calculate the required velocity in order to reach a given trajectory height to reach the endpoint
    public Vector3 CalculateJumpVelocity(Vector3 startingPoint, Vector3 endPoint, float trajectoryHeight)
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

    // projects the 3d rotation of the player's grappling launcher onto the original forward plane
    private Vector3 ProjectMovementOntoPlane(Vector3 point)
    {
        Vector3 directionOfMovement = point - initialGrapplePosition;
        Vector3 projection = Vector3.Project(directionOfMovement, initialGrappleNormal);
        Vector3 projectedPoint = point - projection;
        return projectedPoint - initialGrapplePosition;
    }
    
    // the wait time will be the speed over the distance
    private float RopeWaitTime()
    {
        return Vector3.Distance(launcherTip.position, grapplePoint) / ropeSpeed;
    }

    private void DrawRope()
    {
        // if not grappling reset the line render and spring component
        if (!_player.isGrappling)
        {
            currentPoint = launcherTip.position;
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

        Vector3 forceDirection = Quaternion.LookRotation((grapplePoint - launcherTip.position).normalized) * Vector3.up;
        currentPoint = Vector3.Lerp(currentPoint, grapplePoint, (Time.deltaTime * ropeSpeed) / initialDistance);
        
        for (int i = 0; i < quality + 1; i++)
        {
            // delta from 0.0 - 1.0
            float delta = i / (float) quality;
            Vector3 offset = forceDirection * waveHeight * Mathf.Sin(delta * waveCount * Mathf.PI) * spring.Value * affectCurve.Evaluate(delta);

            grappleRope.SetPosition(i, Vector3.Lerp(launcherTip.position, currentPoint, delta) + offset);
        }
    }
}
