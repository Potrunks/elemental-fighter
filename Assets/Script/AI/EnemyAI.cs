using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform target;
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
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void Update()
    {
        if (followEnabled)
        {
            PathFollow();
        }
        if (target == null && movePlayer.enemy != null)
        {
            target = movePlayer.enemy.transform;
        }
    }

    private void UpdatePath()
    {
        if (followEnabled && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (target != null)
        {
            FightStrategy(CalculateDistancePlayerEnemy());
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

    public float? CalculateDistancePlayerEnemy()
    {
        float distance = Vector3.Distance(this.gameObject.transform.position, target.transform.position);
        return distance;
    }
}
