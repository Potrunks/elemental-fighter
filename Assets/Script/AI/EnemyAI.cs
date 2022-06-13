using UnityEngine;
using Pathfinding;
using System.Collections.Generic;
using System;

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
    private MovePlayer movePlayer;
    private System.Random probability = new System.Random();

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        movePlayer = GetComponent<MovePlayer>();
        movePlayer.jumpForce = 600;
        movePlayer.normalMoveSpeed = 225;
        MovePlayer[] allPlayers = FindObjectsOfType<MovePlayer>();
        List<Transform> allPlayersList = new List<Transform>();
        foreach (MovePlayer player in allPlayers)
        {
            if (player.playerIndex != this.movePlayer.playerIndex)
            {
                allPlayersList.Add(player.transform);
            }
        }
        targets = allPlayersList.ToArray();
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
        if (currentTarget != null)
        {
            FightStrategy(CalculateDistancePlayerEnemy(currentTarget));
        }
        if (path == null)
        {
            return;
        }

        // Reached end of path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        // Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        // Vector2 force = direction * speed * Time.deltaTime;
        movePlayer.horizontalMovementV2 = direction;

        // Movement
        //rb.AddForce(force);

        // Jump
        if (direction.y > jumpNodeHeightRequirement)
        {
            movePlayer.currentState.PerformingInput("Jumping");
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
                movePlayer.currentState.PerformingInput("MediumATK");
                if (probability.NextDouble() * 100 <= 25)
                {
                    movePlayer.currentState.PerformingInput("HeavyATK");
                }
            }
        }
        if (distanceWithEnemy > shortRangeDistanceRequirement && distanceWithEnemy < longRangeDistanceRequirement)
        {
            Debug.Log("AI is mid range mode");
            if (probability.NextDouble() * 100 <= 1)
            {
                movePlayer.currentState.PerformingInput("MediumATK");
            }
            if (probability.NextDouble() * 100 <= 1)
            {
                movePlayer.currentState.PerformingInput("Dash");
            }
        }
        if (distanceWithEnemy <= shortRangeDistanceRequirement)
        {
            Debug.Log("AI is short range mode");
            if (probability.NextDouble() * 100 <= 1)
            {
                movePlayer.currentState.PerformingInput("LightATK");
                if (probability.NextDouble() * 100 <= 25)
                {
                    movePlayer.currentState.PerformingInput("MediumATK");
                    if (probability.NextDouble() * 100 <= 10)
                    {
                        movePlayer.currentState.PerformingInput("HeavyATK");
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
