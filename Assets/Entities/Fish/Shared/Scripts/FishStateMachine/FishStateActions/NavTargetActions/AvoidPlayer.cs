using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "StateMachine/Fish/Actions/AvoidPlayer")]
public class AvoidPlayer : EntityStateAction
{
    public override void PerformAction(Entity entityReference)
    {
        FishController fish = ((FishController)entityReference);
        if (fish != null)
        {
            // set location to be someplace in opposite direction of player
            fish.SetSpeed(fish.GetFishData().AvoidanceSpeed);
            var target = GetAvoidanceDestination(fish);
            fish.SetTargetLocation(target);
        }
    }

    Vector3 GetAvoidanceDestination(FishController fish)
    {
        Vector3 position = fish.transform.position + (fish.transform.position - PlayerCharacter.Instance.transform.position).normalized * 10f;
        NavMeshHit navHit;
        NavMesh.SamplePosition(position, out navHit, 30, NavMesh.AllAreas);
        return navHit.position;
    }
}
