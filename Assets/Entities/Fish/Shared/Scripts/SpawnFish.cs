using Managers.Time;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class SpawnFish : MonoBehaviour
{
    [SerializeField] private NavMeshSurface NavSurface;
    [SerializeField] private TimeManager Timer;
    [SerializeField] private List<GameObject> FishTypes = new List<GameObject>{ };

    [SerializeField] private int initialSpawnNumber = 10;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < initialSpawnNumber; i++)
        {
            SpawnRandomFish();
        }
    }

    public void SpawnRandomFish()
    {
        int randIndex = 0;

        while (true)
        {
            float sum = 0f;
            foreach (var fish in FishTypes)
            {
                sum += fish.GetComponent<FishController>().GetFishData().SpawnChance;
            }


            float scaledIndex = Mathf.FloorToInt(Random.Range(0.0f, sum));
            float lowerBound = 0f;

            for (int i = 0; i < FishTypes.Count; i++)
            {
                FishData fishData = FishTypes[i].GetComponent<FishController>().GetFishData();

                float upperBound = lowerBound + fishData.SpawnChance;
                if (lowerBound < scaledIndex && scaledIndex < upperBound)
                {
                    randIndex = i;
                    break;
                }

                lowerBound = upperBound;
            }

            FishData spawnFishData = FishTypes[randIndex].GetComponent<FishController>().GetFishData();
            float time = Timer.CurrentTime / Timer.EndTime;

            if (spawnFishData.SpawnStartTime <= time && time <= spawnFishData.SpawnEndTime)
            {
                break;
            }
        }


        Vector3 randPos = new Vector3(
            Random.Range(-1.0f, 1.0f) * NavSurface.size.x / 2f,
            Random.Range(-1.0f, 1.0f) * NavSurface.size.y / 2f,
            Random.Range(-1.0f, 1.0f) * NavSurface.size.z / 2f
        );

        Instantiate(FishTypes[randIndex], randPos + NavSurface.center + NavSurface.transform.position, Quaternion.identity);
    }
}
