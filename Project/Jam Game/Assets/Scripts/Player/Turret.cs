using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float attackingRange;
    public float attackingCooldown;

    private float timer;

    public GameObject bulletPrefab;
    public GameObject turret;

    public Transform ShootingPoint;

    private GameObject closestEnemy;

    private void FixedUpdate()
    {
        // Update the cooldown timer
        if (timer < attackingCooldown)
            timer += Time.fixedDeltaTime;

        // Find the closest enemy
        FindClosestEnemy();

        // If an enemy is within range, rotate and shoot
        if (closestEnemy != null)
        {
            RotateTurretTowardsEnemy();
            if (timer >= attackingCooldown)
            {
                ShootBullet();
                timer = 0f;
            }
        }
    }

    /// <summary>
    /// find the closest gameobject with the Enemy tag
    /// </summary>
    private void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestEnemyDistance = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            float currentEnemyDistance = Vector2.Distance(enemy.transform.position, transform.position);

            if (currentEnemyDistance < attackingRange && currentEnemyDistance < closestEnemyDistance)
            {
                closestEnemyDistance = currentEnemyDistance;
                closestEnemy = enemy;
            }
        }
    }

    /// <summary>
    /// rotate the turret towards the closest enemy
    /// </summary>
    private void RotateTurretTowardsEnemy()
    {
        Vector2 direction = (closestEnemy.transform.position - ShootingPoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        turret.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    /// <summary>
    /// shoot a bullet based on the turret's rotation
    /// </summary>
    private void ShootBullet()
    {
        float angle = turret.transform.rotation.eulerAngles.z;
        Instantiate(bulletPrefab, ShootingPoint.position, Quaternion.Euler(new Vector3(0, 0, angle + 90)));
    }
}
