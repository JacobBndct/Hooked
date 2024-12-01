using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Fish/Actions/StartTimer")]
public class StartTimer : EntityStateAction
{
    public string minPropertyName = "";
    public string maxPropertyName = "";

    public override void PerformAction(Entity entityReference)
    {
        FishController fish = ((FishController)entityReference);

        if (fish != null)
        {
            fish.Timer.EndTimer();

            FishData fishData = fish.GetFishData();
            float min = (float)fishData.GetPropValue(minPropertyName);
            float max = (float)fishData.GetPropValue(maxPropertyName);

            if (min == default && max == default) return;

            float hookedTime = Random.Range(min, max);

            fish.Timer.SetEndTime(hookedTime);
            fish.Timer.StartTimer();
        }
    }
}
