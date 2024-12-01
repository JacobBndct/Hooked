using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "StateMachine/Fish/Actions/Wander")]
public class Wander : EntityStateAction
{
    public override void PerformAction(Entity entityReference)
    {
        FishController fish = ((FishController)entityReference);

        if (fish != null && fish.Timer.Ended)
        {
            var target = GetWanderDestination(fish);
            fish.SetTargetLocation(target);
            fish.SetSpeed(fish.GetFishData().BaseSpeed);

            FishData fishData = fish.GetFishData();
            float moveTime = Random.Range(fishData.MinWanderTimer, fishData.MaxWanderTimer);

            fish.Timer.SetEndTime(moveTime);
            fish.Timer.StartTimer();
        }
    }

    Vector3 GetWanderDestination(FishController fish)
    {
        Vector3 dir = Random.insideUnitSphere * fish.GetFishData().WanderRadius + fish.transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(dir, out navHit, 1000, NavMesh.AllAreas);
        return navHit.position;
    }
}
