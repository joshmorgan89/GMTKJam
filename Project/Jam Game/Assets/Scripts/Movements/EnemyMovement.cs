using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float stopRange = 5f;
    public float tooCloseRange = 3f;

    private Transform target;

    private void Update()
    {
        FindClosestTarget();

        if (target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            if (distanceToTarget > stopRange)
            {
                MoveTowardsTarget();
            }
            else if (distanceToTarget < tooCloseRange)
            {
                MoveAwayFromTarget();
            }
        }
    }

    /// <summary>
    /// Find the closest player or room
    /// </summary>
    void FindClosestTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");

        GameObject closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < closestDistance)
            {
                closestDistance = distanceToPlayer;
                closest = player;
            }
        }

        foreach (GameObject room in rooms)
        {
            float distanceToRoom = Vector2.Distance(transform.position, room.transform.position);
            if (distanceToRoom < closestDistance)
            {
                closestDistance = distanceToRoom;
                closest = room;
            }
        }

        if (closest != null)
        {
            target = closest.transform;
        }
    }

    /// <summary>
    /// move towards to closest target
    /// </summary>
    void MoveTowardsTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// move away from target too close
    /// </summary>
    void MoveAwayFromTarget()
    {
        Vector2 direction = (transform.position - target.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)direction, moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// check if the target is in range
    /// </summary>
    /// <returns>Is the target in range</returns>
    public bool IsInRange()
    {
        if (target == null) return false;

        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        return distanceToTarget <= stopRange && distanceToTarget >= tooCloseRange;
    }


    public Transform GetTarget()
    {
        return target;
    }
}
