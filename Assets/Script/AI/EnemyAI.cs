using UnityEngine;
using Pathfinding;
using System.Collections.Generic;
using System;
using Assets.Script.Data.Reference;

public class EnemyAI : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform currentTarget;
    private Transform nextTarget;
    [SerializeField]
    private Transform[] targets;
    private float? closestDistance = 0f;
    public float pathUpdateSeconds;

    [Header("Physics")]
    public float nextWaypointDistance;
    public float jumpNodeHeightRequirement;
    public float longRangeDistanceRequirement;
    public float shortRangeDistanceRequirement;

    [Header("Custom Behavior")]
    public bool followEnabled = true;

    private Path path;
    private int currentWaypoint = 0;
    private Seeker seeker;
    private Rigidbody2D rb;
    private PlayableCharacterController _playableCharacterController;
    private System.Random probability = new System.Random();

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        _playableCharacterController = GetComponent<PlayableCharacterController>();
        PlayableCharacterController[] allPlayers = FindObjectsOfType<PlayableCharacterController>();
        List<Transform> allTransformPlayers = new List<Transform>();
        foreach (PlayableCharacterController player in allPlayers)
        {
            if (player._playerIndex != _playableCharacterController._playerIndex)
            {
                allTransformPlayers.Add(player.transform);
            }
        }
        targets = allTransformPlayers.ToArray();
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void Update()
    {
        if (followEnabled)
        {
            PathFollow();
        }
        UpdateTarget();
    }

    /// <summary>
    /// Update the target enemy of the AI
    /// </summary>
    private void UpdateTarget()
    {
        CalculateDistanceBetweenAllPlayersEnemies();
        VerifyNewTargetIsPossible();
    }

    /// <summary>
    /// Calculate the distance between all the players enemies on the map
    /// </summary>
    private void CalculateDistanceBetweenAllPlayersEnemies()
    {
        foreach (Transform t in targets)
        {
            float? distance = CalculateDistancePlayerEnemy(t);
            if (distance <= closestDistance || closestDistance == 0f)
            {
                closestDistance = distance;
                nextTarget = t;
            }
        }
    }

    /// <summary>
    /// Verify if update target its necessary
    /// </summary>
    private void VerifyNewTargetIsPossible()
    {
        if (nextTarget != currentTarget)
        {
            currentTarget = nextTarget;
        }
    }

    private void UpdatePath()
    {
        if (followEnabled && seeker.IsDone())
        {
            seeker.StartPath(rb.position, currentTarget.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        _playableCharacterController.isDeviceUsed = true;
        if (currentTarget != null)
        {
            FightStrategy(CalculateDistancePlayerEnemy(currentTarget));
        }
        if (path == null)
        {
            _playableCharacterController.isDeviceUsed = false;
            return;
        }

        // Reached end of path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            _playableCharacterController.isDeviceUsed = false;
            return;
        }

        // Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        // Vector2 force = direction * speed * Time.deltaTime;
        _playableCharacterController.inputMoveValue = direction;

        // Movement
        //rb.AddForce(force);

        // Jump
        if (direction.y > jumpNodeHeightRequirement)
        {
            _playableCharacterController.currentState.PerformingInput(PlayableCharacterActionReference.Jump);
        }

        // Next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    private void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            this.path = path;
            currentWaypoint = 0;
        }
    }

    private void FightStrategy(float? distanceWithEnemy)
    {
        if (distanceWithEnemy >= longRangeDistanceRequirement)
        {
            Debug.Log("AI is long range mode");
            if (probability.NextDouble() * 100 <= 1)
            {
                _playableCharacterController.currentState.PerformingInput(PlayableCharacterActionReference.MediumAtk);
                if (probability.NextDouble() * 100 <= 25)
                {
                    _playableCharacterController.currentState.PerformingInput(PlayableCharacterActionReference.HeavyAtk);
                }
            }
        }
        if (distanceWithEnemy > shortRangeDistanceRequirement && distanceWithEnemy < longRangeDistanceRequirement)
        {
            Debug.Log("AI is mid range mode");
            if (probability.NextDouble() * 100 <= 1)
            {
                _playableCharacterController.currentState.PerformingInput(PlayableCharacterActionReference.MediumAtk);
            }
            if (probability.NextDouble() * 100 <= 1)
            {
                _playableCharacterController.currentState.PerformingInput(PlayableCharacterActionReference.SpecialAtk);
            }
        }
        if (distanceWithEnemy <= shortRangeDistanceRequirement)
        {
            Debug.Log("AI is short range mode");
            if (probability.NextDouble() * 100 <= 1)
            {
                _playableCharacterController.currentState.PerformingInput(PlayableCharacterActionReference.LightAtk);
                if (probability.NextDouble() * 100 <= 25)
                {
                    _playableCharacterController.currentState.PerformingInput(PlayableCharacterActionReference.MediumAtk);
                    if (probability.NextDouble() * 100 <= 10)
                    {
                        _playableCharacterController.currentState.PerformingInput(PlayableCharacterActionReference.HeavyAtk);
                    }
                }
            }
        }
    }

    public float? CalculateDistancePlayerEnemy(Transform transformOfEnemy)
    {
        float distance = Vector3.Distance(this.gameObject.transform.position, transformOfEnemy.position);
        return distance;
    }
}
