using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fish", menuName = "Data/Fish")]
public class FishData : ScriptableObject
{
    [Header("Fish Capture")]
    [SerializeField] private int sellingValue = 10;
    [SerializeField] private float captureDistance = 1f;
    [SerializeField] private float minHookedTimer = 2f;
    [SerializeField] private float maxHookedTimer = 5f;
    [SerializeField] private float minNibbleTimer = 2f;
    [SerializeField] private float maxNibbleTimer = 5f;

    [Header("Fish Spawn")]
    [SerializeField] private float spawnStartTime = 0f;
    [SerializeField] private float spawnEndTime = 1f;

    [Header("Fish Movement")]
    [SerializeField] private float wanderRadius = 30;
    [SerializeField] private float baseSpeed = 7f;
    [SerializeField] private float minWanderTimer = 0.3f;
    [SerializeField] private float maxWanderTimer = 2f;

    [Header("Fish Avoidance")]
    [SerializeField] private float avoidanceSpeed = 10f;
    [SerializeField] private float avoidanceRadius = 3f;

    [Header("Fish Attraction")]
    [SerializeField] private float attractionRadius = 5f;
    [SerializeField] private float attractionTickChance = 0.1f;
    [SerializeField] private int[] preferredBaitTypes = { 1 };

    public int SellingValue => sellingValue;
    public float CaptureDistance => captureDistance;
    public float MinHookedTimer => minHookedTimer;
    public float MaxHookedTimer => maxHookedTimer;
    public float MinNibbleTimer => minNibbleTimer;
    public float MaxNibbleTimer => maxNibbleTimer;

    public float SpawnStartTime => spawnStartTime;
    public float SpawnEndTime => spawnEndTime;

    public float WanderRadius => wanderRadius;
    public float BaseSpeed => baseSpeed;
    public float MinWanderTimer => minWanderTimer;
    public float MaxWanderTimer => maxWanderTimer;

    public float AvoidanceSpeed => avoidanceSpeed;
    public float AvoidanceRadius => avoidanceRadius;

    public float AttractionRadius => attractionRadius;
    public float AttractionTickChance => attractionTickChance;
    public int[] PreferredBaitTypes => preferredBaitTypes;

    public object GetPropValue(string propName)
    {
        return GetType().GetProperty(propName).GetValue(this, null);
    }
}
