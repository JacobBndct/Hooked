using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fish", menuName = "Data/Fish")]
public class FishData : ScriptableObject
{
    [Header("Fish Spawn")]
    [SerializeField] private float spawnStartTime = 0f;
    [SerializeField] private float spawnEndTime = 1f;

    [Header("Fish Value")]
    [SerializeField] private int sellingValue = 10;

    [Header("Fish Movement")]
    [SerializeField] private float wanderRadius = 30;
    [SerializeField] private float baseSpeed = 7f;

    [Header("Fish Avoidance")]
    [SerializeField] private float avoidanceSpeed = 10f;
    [SerializeField] private float avoidanceRadius = 3f;

    [Header("Fish Attraction")]
    [SerializeField] private float attractionRadius = 5f;
    [SerializeField] private float attractionTickChance = 0.1f;
    [SerializeField] private int[] preferredBaitTypes = { 1 };

    [Header("Fish Timers")]
    [SerializeField] private float minWanderTimer = 0.3f;
    [SerializeField] private float maxWanderTimer = 2f;
    [SerializeField] private float minInterestTimer = 2f;
    [SerializeField] private float maxInterestTimer = 5f;

    public float SpawnStartTime => spawnStartTime;
    public float SpawnEndTime => spawnEndTime;

    public float SellingValue => sellingValue;

    public float WanderRadius => wanderRadius;
    public float BaseSpeed => baseSpeed;

    public float AvoidanceSpeed => avoidanceSpeed;
    public float AvoidanceRadius => avoidanceRadius;

    public float AttractionRadius => attractionRadius;
    public float AttractionTickChance => attractionTickChance;
    public int[] PreferredBaitTypes => preferredBaitTypes;

    public float MinWanderTimer => minWanderTimer;
    public float MaxWanderTimer => maxWanderTimer;
    public float MinInterestTimer => minInterestTimer;
    public float MaxInterestTimer => maxInterestTimer;
}
